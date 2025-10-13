using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankAccountForUpdateDto
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int CurrencyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
