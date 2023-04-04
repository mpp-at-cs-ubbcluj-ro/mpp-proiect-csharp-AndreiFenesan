using System.Collections.Generic;
using lab5.models;

namespace lab5.repositories.interfaces;

public interface IRepository<TId, TE> where TE : Entity<TId>
{
    TE Save(TE entity);
    TE Delete(TId id);
    TE Update(TE entity);
    List<TE> FindAll();
    TE? findOneById(TId id);
}