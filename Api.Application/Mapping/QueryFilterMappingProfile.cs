
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Application.Mapping;

public class QueryFilterMappingProfile : Profile
{
    public QueryFilterMappingProfile()
    {
        CreateMap<Post, PostQueryFilter>();

        CreateMap<UserAccount, UserAccountQueryFilter>();


    }
}