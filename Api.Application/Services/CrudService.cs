using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

using Api.Common.Entities;
using Api.Common.Exceptions;
using Api.Common.Interfaces.Services;
using Api.Common.Interfaces.Repositories;

using Api.Common.Interfaces.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Entities;
// using Api.Domain.Entities;

namespace Api.Application.Services;

public class CrudService<T> : ICrudService<T> where T : BaseEntity
{
    protected ICrudRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    public CrudService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
        this._repository = GetRepository();
    }

    protected virtual ICrudRepository<T> GetRepository()
    {
        var typeRep = typeof(T);

        if (typeRep == typeof(Commentary))
            return (ICrudRepository<T>)this._unitOfWork.CommentaryRepository;

        if (typeRep == typeof(UserInfo))
            return (ICrudRepository<T>)this._unitOfWork.UserInfoRepository;

        return (ICrudRepository<T>)this._unitOfWork.UserAccountRepository;
    }

    public virtual async Task<int> Create(T entity)
    {
        var result = await _repository.Create(entity);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public virtual async Task CreateRange(IEnumerable<T> entities)
    {
        await _repository.CreateRange(entities);
        await _unitOfWork.SaveChangesAsync();
    }

    public virtual async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public virtual async Task<int> DeleteBy(Expression<Func<T, bool>> filter)
    {
        return await _repository.DeleteBy(filter);
    }

    public virtual async Task<int> DeleteRange(IEnumerable<int> idList)
    {
        return await _repository.DeleteRange(idList);
    }

    public virtual async Task<bool> Exist(Expression<Func<T, bool>> filters)
    {
        return await _repository.Exist(filters);
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        return _repository.Get(filters, orderBy, includeProperties);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll();
    }

    public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "")
    {
        return await _repository.GetBy(filters, includeProperties);
    }

    public virtual async Task<T> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public virtual async Task<PagedList<T>> GetPaged(T entity)
    {
        var pagedControl = (IPaginationQueryable)entity;
        var result = await _repository.GetPaged(entity);
        var pagedItems = PagedList<T>.Create(result, pagedControl.PageNumber, pagedControl.PageSize);
        return pagedItems;
    }

    public virtual async Task Update(T entity)
    {
        var currentEntity = await this.GetById(entity.Id) ?? throw new BusinessException("No se encontró el elemento que se desea modificar, verifique su información");
        var updateEntity = this.MapCurrentEntityToUpdate(entity, currentEntity);

        _repository.Update(updateEntity);

        await _unitOfWork.SaveChangesAsync();
    }

    public virtual T MapCurrentEntityToUpdate(T source, T target)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (!property.IsDefined(typeof(KeyAttribute)) && property.CanWrite) //&& !property.PropertyType.IsClass  && !typeof(ICollection).IsAssignableFrom(property.PropertyType)
            {
                if (property.Name.CompareTo("CreatedDate") != 0 && property.Name.CompareTo("CreatedBy") != 0)
                {
                    var value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }
        }
        return target;
    }
}