using Api.Common.Dtos.Response;
using Api.Common.Helpers;
using Api.Domain.Enumerations;
using Api.Domain.Interfaces.Services;

namespace Api.Application.Services;

public class MiscellaneousService : IMiscellaneousService
{
    public async Task<IEnumerable<EnumValueResponseDto>> GetGender()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<Gender>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }
}