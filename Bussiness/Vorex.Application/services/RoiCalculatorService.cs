using System.Net.Http;
using System.Net.Http.Json;
using Vorex.Application.services.Dtos;
using Vorex.Application.services.interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.services;

public class RoiCalculatorService(HttpClient httpClient) : IRoiCalculator
{
    public async Task<Result<RoiCalculatorResponseDto, Error>> CalculateRoi(RoiCalculatorInput input)
    {
        var response = await httpClient.PostAsJsonAsync<RoiCalculatorInput>("https://localhost:7067/roi", input);

        if (!response.IsSuccessStatusCode)
            return await Task.FromResult<Result<RoiCalculatorResponseDto, Error>>(Error.BadRequest(nameof(RoiCalculatorService),"Failed to connect with AI Model"));

        var result = await response.Content.ReadFromJsonAsync<RoiCalculatorResponseDto>();

        if (result is null)
            return await Task.FromResult<Result<RoiCalculatorResponseDto, Error>>(Error.BadRequest(nameof(RoiCalculatorService), "Something go wrong"));

        return result;
    }
}
