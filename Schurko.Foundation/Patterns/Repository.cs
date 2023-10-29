// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Patterns.Repository`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


#nullable enable
namespace Schurko.Foundation.Patterns
{
  public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    public Repository()
    {
    }

    public abstract void Add(TEntity entity);

    public abstract void Update(TEntity entity);

    public abstract void Delete(TEntity entity);

    public abstract IEnumerable<TEntity> GetAll();

    public abstract TEntity GetById(int id);

    public abstract bool SaveChanges();
 
  }
}
