using System.Linq.Expressions;
using Api.Common.Interfaces.Repositories;
using Api.Domain.Dto.QueryFilters;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface ICommentaryRepository : ICrudRepository<Commentary>, IQueryFilterPagedRepository<Commentary, CommentaryQueryFilter>
{
}