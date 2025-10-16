using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime DateCreated  { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
