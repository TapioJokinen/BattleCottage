using BattleCottage.Core.Entities;

namespace BattleCottage.Core.Caching
{
    public class EntityCacheDefaults<TEntity>
        where TEntity : BaseEntity
    {
        public static string EntityTypeName => typeof(TEntity).Name.ToLowerInvariant();

        public static CacheKey ByIdCacheKey => new($"battlecottage.{EntityTypeName}.byid.{{0}}");
    }
}
