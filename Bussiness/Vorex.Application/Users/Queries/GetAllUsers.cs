using MediatR;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Users.Queries;

public class GetAllUsers
{
    public sealed class Query : IRequest<Result<List<Data>, Error>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        private Query(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static Query Create(int pageNumber, int pageSize) => new (pageNumber, pageSize);
    }

    public sealed class Data
    {
        public Guid Id { get; set; }
        public required string FirstName {get; set;}
        public required string LastName {get;  set;}
        public required string Email {get; set;}
        public string? ProfileImage {get; set;}
        public DateTime CreatedAt {get;  set;}

        public UserDto ToUsersListDto()
        {
            return new UserDto
            {
                Id = Id,
                Name = $"{FirstName} {LastName}",
                Email = Email,
                CreatedAt = CreatedAt
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.User.User> _userRepo) : IRequestHandler<Query, Result<List<Data>, Error>>
    {
        public Task<Result<List<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = _userRepo.GetAll()
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ProfileImage = x.ProfileImage,
                    CreatedAt = x.CreatedAt
                })
                .ToList();

            return Task.FromResult<Result<List<Data>, Error>>(users);
        }
    }
}