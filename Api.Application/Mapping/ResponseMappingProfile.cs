
using Api.Common.Helpers;
using Api.Domain.Dto.Response;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Application.Mapping;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {

        CreateMap<Post, PostResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        );

        CreateMap<UserAccount, UserAccountResponseDto>()
        .ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.UserName)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        ).AfterMap(
            (src, dest) => 
            {
                var userInfo = src.UserInfo.FirstOrDefault() ?? new UserInfo();
                dest.UserInfoId = userInfo.Id;
                dest.FullName = userInfo.FullName;
                dest.Phone = userInfo.Phone!;
                dest.CellPhone = userInfo.CellPhone;
            }
        );
    }
}