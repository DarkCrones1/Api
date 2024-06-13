
using Api.Common.Helpers;
using Api.Domain.Dto.Response;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Application.Mapping;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<Commentary, CommentaryResponseDto>()
        .ForMember(
            dest => dest.Id,
            opt => opt.MapFrom(src => src.Id)
        ).ForMember(
            dest => dest.Description,
            opt => opt.MapFrom(src => src.Description)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        ).AfterMap(
            (src, dest) =>
            {
                var userAccount = src.UserAccount ?? new UserAccount();
                var userInfo = userAccount.UserInfo.FirstOrDefault() ?? new UserInfo();
                var post = src.Post ?? new Post();

                dest.UserInfoName = userInfo.FullName;
                dest.PostId = src.PostId;
                dest.PostName = post.Name;

            }
        );

        CreateMap<Post, PostResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        ).AfterMap(
            (src, dest) =>
            {
                var userAccount = src.UserAccount ?? new UserAccount();
                var userInfo = userAccount.UserInfo.FirstOrDefault() ?? new UserInfo();

                dest.UserInfoName = userInfo.FullName;
                dest.UserInfoProfilePictureUrl = userInfo.ProfilePictureUrl;
            }
        );

        CreateMap<Post, PostDetailResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        ).AfterMap(
            (src, dest) =>
            {
                var userAccount = src.UserAccount ?? new UserAccount();
                var userInfo = userAccount.UserInfo.FirstOrDefault() ?? new UserInfo();

                dest.UserInfoName = userInfo.FullName;
                dest.UserInfoProfilePictureUrl = userInfo.ProfilePictureUrl;
            }
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

        CreateMap<UserAccount, UserAccountDetailResponseDto>()
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

        CreateMap<UserInfo, UserInfoResponseDto>();
    }
}