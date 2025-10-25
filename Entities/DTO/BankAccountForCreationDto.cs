using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class BankAccountForCreationDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public decimal InitialBalance { get; set; }

        [JsonIgnore]
        public int ClientId { get; set; }

    }

}
