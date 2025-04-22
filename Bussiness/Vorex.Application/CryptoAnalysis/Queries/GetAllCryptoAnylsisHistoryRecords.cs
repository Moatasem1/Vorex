using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Application.CryptoAnalysis.Contract;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Cryptos;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.CryptoAnalysis.Queries;

public class GetAllCryptoAnylsisHistoryRecords
{
    public sealed class Query : IRequest<Result<List<Data>, Error>>
    {
        public Guid UserId { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string? SearchQuery { get; private set; }
        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; init; }

        private Query(Guid userId, int pageNumber, int pageSize, string? searchQuery, DateOnly? startDate, DateOnly? endDate)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchQuery = searchQuery;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Query Create(Guid userId,int pageNumber, int pageSize, string? searchQuery = null, DateOnly? startDate=null, DateOnly? endDate = null) => new(userId,pageNumber, pageSize, searchQuery,startDate,endDate);
    }

    public sealed class Data
    {
        public Guid Id { get; set; }
        public required string CryptoName { get; set; }
        public required decimal Amount { get; set; }
        public int HoldingDays { get; set; }
        public decimal Risk { get; set; }
        public required DateTime SubmitDate { get; set; }
        public CryptoAnalysisHistoryDto ToCryptoAnalysisHistoryDto()
        {
            return new CryptoAnalysisHistoryDto
            {
                Id = Id,
                CryptoName = CryptoName,
                Amount = Amount,
                HoldingDays = HoldingDays,
                Risk = Risk,
                SubmitDate = SubmitDate
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<CryptoAnalysisHistory> _cryptoAnalysisHistoryRepository, IReadOnlyRepository<Crypto> _cryptoRepository) : IRequestHandler<Query, Result<List<Data>, Error>>
    {
        public Task<Result<List<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {

            var cryptoAnalysisHistoryQuery = from cryptoAnalysisHistory in _cryptoAnalysisHistoryRepository.GetAll()
                                               join crypto in _cryptoRepository.GetAll()
                                               on cryptoAnalysisHistory.CryptoId equals crypto.Id
                                               where cryptoAnalysisHistory.UserId == request.UserId
                                                  && (string.IsNullOrEmpty(request.SearchQuery) || crypto.Name.Contains(request.SearchQuery))
                                                  && (request.StartDate == null || DateOnly.FromDateTime(cryptoAnalysisHistory.CreatedAt) >= request.StartDate)
                                                  && (request.EndDate == null || DateOnly.FromDateTime(cryptoAnalysisHistory.CreatedAt) <= request.EndDate)
                                               select new Data
                                               {
                                                   Id = cryptoAnalysisHistory.Id,
                                                   CryptoName = crypto.Name,
                                                   Amount = cryptoAnalysisHistory.Amount,
                                                   HoldingDays = cryptoAnalysisHistory.HoldingDays,
                                                   Risk = cryptoAnalysisHistory.Risk,
                                                   SubmitDate = cryptoAnalysisHistory.CreatedAt
                                               };

            var paginatedRecords = cryptoAnalysisHistoryQuery
                .OrderByDescending(x => x.SubmitDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return Task.FromResult<Result<List<Data>, Error>>(paginatedRecords);
        }
    }
}
