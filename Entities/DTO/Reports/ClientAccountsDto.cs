using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Reports
{
    public class ClientAccountsDto
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Currency { get; set; }
        public decimal CurrentBalance { get; set; }

    }
}
