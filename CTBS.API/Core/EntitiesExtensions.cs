﻿using Microsoft.EntityFrameworkCore;

namespace CTBS.API.Core;

public static class EntitiesExtensions
{
    public static async ValueTask AddAndSave<T>(this DbContext dbContext, T entity, CancellationToken ct)
        where T : notnull
    {
        await dbContext.AddAsync(entity, ct);
        await dbContext.SaveChangesAsync(ct);
    }
    
    public static async ValueTask DeleteAndSave<T>(this DbContext dbContext, T entity, CancellationToken ct)
        where T : notnull
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync(ct);
    }
    
    public static async ValueTask UpdateAndSave<T>(this DbContext dbContext, T entity, CancellationToken ct)
        where T : notnull
    {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync(ct);
    }

    public static ValueTask<T?> Find<T, TId>(this DbContext dbContext, TId id, CancellationToken ct)
        where T : class where TId : notnull
        => dbContext.FindAsync<T>(new object[] {id}, ct);

    public static IServiceCollection AddQueryable<T, TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
        where T : class =>
        services.AddTransient(sp => sp.GetRequiredService<TDbContext>().Set<T>().AsNoTracking());
}