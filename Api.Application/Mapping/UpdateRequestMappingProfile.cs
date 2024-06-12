using Api.Domain.Dto.Request.Update;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Application.Mapping;

public class UpdateRequestMappingProfile : Profile
{
    public UpdateRequestMappingProfile()
    {
        CreateMap<UserInfoUpdateRequestDto, UserInfo>();

        CreateMap<CommentaryUpdateRequestDto, Commentary>();

        CreateMap<PostUpdateRequestDto, Post>();
    }
}