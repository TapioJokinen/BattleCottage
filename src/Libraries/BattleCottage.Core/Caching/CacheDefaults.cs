using BattleCottage.Core.Entities;

namespace BattleCottage.Core.Caching;

public static class CacheDefaults<TEntity> where TEntity : BaseEntity
{
    private static readonly string EntityName = typeof(TEntity).Name.ToLowerInvariant();

    public static CacheKey ByIdCacheKey => new($"BC.{EntityName}.ById.{{0}}", ByIdPrefix, EntityPrefix);
    public static CacheKey AllValuesCacheKey => new($"BC.{EntityName}.AllValues", AllValuesPrefix, EntityPrefix);

    private static string EntityPrefix => $"BC.{EntityName}.";
    private static string ByIdPrefix => $"BC.{EntityName}.ById.";
    private static string AllValuesPrefix => $"BC.{EntityName}.AllValues.";
}