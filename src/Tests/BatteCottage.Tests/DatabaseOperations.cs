using Microsoft.EntityFrameworkCore;
using BattleCottage.Data;

namespace BattleCottage.Tests
{
    public class DatabaseOperations : IDatabaseOperations
    {
        private readonly ApplicationDbContext _context;

        public DatabaseOperations(ApplicationDbContext context)
        {
            _context = context;
        }

        public void TruncateDatabase()
        {
            IList<string?> tableNames = _context.Model
                .GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToList();

            if (tableNames.Count > 0)
            {
                foreach (string? tableName in tableNames)
                {
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        _context.Database.ExecuteSqlRaw($"""TRUNCATE TABLE "{tableName}" CASCADE;""");
                    }
                }
            }
        }
    }
}
