
using Api.Common.Helpers;
using Api.Domain.Dto.Request.Create;
using Api.Domain.Entities;
using Api.Domain.Enumerations;
using AutoMapper;

namespace Api.Application.Mapping;

public class CreateRequestMappingProfile : Profile
{
    public CreateRequestMappingProfile()
    {
        CreateMap<CommentaryCreateRequestDto, Commentary>()
        .ForMember(
            dest => dest.Description,
            opt => opt.MapFrom(src => src.Description)
        ).ForMember(
            dest => dest.PostId,
            opt => opt.MapFrom(src => src.PostId)
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        );

        CreateMap<PostCreateRequestDto, Post>()
        .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => src.Name)
        ).ForMember(
            dest => dest.Description,
            opt => opt.MapFrom(src => src.Description)
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).ForMember(
            dest => dest.PublicationDate,
            opt => opt.MapFrom(src => DateTime.Now)
        );

        CreateMap<UserAccountCreateRequestDto, UserAccount>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.IsAuthorized,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(src => src.Email)
        );

        CreateMap<MotoCreateRequestDto, Moto>()
        .ForMember(
            dest => dest.Code,
            opt => opt.MapFrom(src => Guid.NewGuid())
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.PublicationDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).ForMember(
            dest => dest.AvailableStatus,
            opt => opt.MapFrom(src => (short)MotoStatus.Available)
        ).ForMember(
            dest => dest.CreatedBy,
            opt => opt.MapFrom(src => "Admin")
        );

        CreateMap<UserAccountCreateRequestDto, UserInfo>()
        .AfterMap(
            (src, dest) =>
            {
                dest.FirstName = "Asignar";
                dest.LastName = "Asignar";
                dest.CellPhone = "Asignar";
                dest.Code = Guid.NewGuid();
                dest.Gender = (short)Gender.Other;
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;
            }
        );
    }
}