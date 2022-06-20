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
        
        public IEnumerable<Clients> GetAll()
        {
            var query = "SELECT * FROM Test";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<Clients>(query);
                return result;
            }
        }

        public Clients GetById(int id)
        {
            var query = "SELECT * FROM Test WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<Clients>(query, new { id });
                return result;
            }
        }

        public IEnumerable<Clients> GetAllClients()
        {
            var query = "SELECT * FROM Clients";
            using (var connection = _context.CreateConnection())
            {
                var result = connection.Query<Clients>(query);
                return result;
            }
        }
        
    }
}
