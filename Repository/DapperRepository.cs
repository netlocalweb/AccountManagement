using Contracts;
using Dapper;
using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly DapperContext _context;

        public DapperRepository(DapperContext context)
        {
            _context = context;
        }

        public IEnumerable<TestEntity> GetAll()
        {
            var query = "SELECT * FROM Test";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<TestEntity>(query);
                return result;
            }
        }

        public TestEntity GetById(int id)
        {
            var query = "SELECT * FROM Test WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<TestEntity>(query, new { id });
                return result;
            }
        }
    }
}
