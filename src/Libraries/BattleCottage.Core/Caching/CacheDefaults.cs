using BattleCottage.Core.Entities;

namespace BattleCottage.Core.Caching;

public static class CacheDefaults<TEntity> where TEntity : BaseEntity
{
    private static readonly string EntityName = typeof(TEntity).Name.ToLowerInvariant();

    public static CacheKey ByIdCacheKey => new($"BC.{EntityName}.ById.{{0}}", ByIdPrefix, Prefix);

    private static string Prefix => $"BC.{EntityName}.";
    private static string ByIdPrefix => $"BC.{EntityName}.ById.";
}