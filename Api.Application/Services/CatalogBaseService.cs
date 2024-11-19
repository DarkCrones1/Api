using Api.Common.Entities;
using Api.Common.Interfaces.Entities;
using Api.Common.Interfaces.Repositories;
using Api.Common.Interfaces.Services;
using Api.Domain.Entities;

// using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Application.Services;

public class CatalogBaseService<T> : CrudService<T>, ICatalogBaseService<T> where T : CatalogBaseEntity
{
    protected new ICatalogBaseRepository<T> _repository;
    public CatalogBaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        this._repository = GetRepository();
    }

    protected override ICatalogBaseRepository<T> GetRepository()
    {
        var typeRep = typeof(T);

        if (typeRep == typeof(Moto))
            return (ICatalogBaseRepository<T>)this._unitOfWork.MotoRepository;

        return (ICatalogBaseRepository<T>)this._unitOfWork.PostRepository;
    }
}