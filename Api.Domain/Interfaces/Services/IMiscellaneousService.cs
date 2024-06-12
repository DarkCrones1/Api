using Api.Common.Dtos.Response;

namespace Api.Domain.Interfaces.Services;

public interface IMiscellaneousService
{
    Task<IEnumerable<EnumValueResponseDto>> GetGender();
    // Task<IEnumerable<EnumValueResponseDto>> GetUserStatus();
}