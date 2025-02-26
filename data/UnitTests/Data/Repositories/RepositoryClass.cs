using Accelerate.Data.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Accelerate.Data.Repositories;

[ExcludeFromCodeCoverage]
internal class RepositoryClass : Repository<EntityClass, RepositoryClassOptions>
{
    public RepositoryClass(IOptions<RepositoryClassOptions> options) : base(options)
    {
    }

    public override Int32 Commit() => throw new NotImplementedException();
    public override void Delete(EntityClass entity) => throw new NotImplementedException();
    public override void Delete(IEnumerable<EntityClass> entities) => throw new NotImplementedException();
    public override void Delete(Expression<Func<EntityClass, Boolean>> expression) => throw new NotImplementedException();
    protected override void Dispose(Boolean disposing)
    {
        base.Dispose(disposing);
    }
    public override void Insert(EntityClass entity) => throw new NotImplementedException();
    public override void Insert(IEnumerable<EntityClass> entities) => throw new NotImplementedException();
    public override void Rollback() => throw new NotImplementedException();
    public override IEnumerable<EntityClass> Select() => throw new NotImplementedException();
    public override IEnumerable<EntityClass> Select(Expression<Func<EntityClass, Boolean>> expression) => throw new NotImplementedException();
    public override IEnumerable<EntityClass> Select(Expression<Func<EntityClass, Boolean>> expression, Int32 skip) => throw new NotImplementedException();
    public override IEnumerable<EntityClass> Select(Expression<Func<EntityClass, Boolean>> expression, Int32 skip, Int32 take) => throw new NotImplementedException();
    public override void Update(EntityClass entity) => throw new NotImplementedException();
    public override void Update(IEnumerable<EntityClass> entities) => throw new NotImplementedException();
}
