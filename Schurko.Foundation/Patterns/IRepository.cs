// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Patterns.IRepository`1
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System.Collections.Generic;


#nullable enable
namespace Schurko.Foundation.Patterns
{
  public interface IRepository<TEntity> where TEntity : class
  {
    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    IEnumerable<TEntity> GetAll();

    TEntity GetById(int id);

    bool SaveChanges();
  }
}
