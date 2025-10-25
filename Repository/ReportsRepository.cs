using Contracts;
using Entities;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ReportsRepository : IDapperRepository
    {
        private readonly DapperContext _dapperContext;

        public ReportsRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public Task<IEnumerable<ClientReports>> GetClientReportsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
