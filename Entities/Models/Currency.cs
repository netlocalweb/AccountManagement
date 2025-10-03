using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName ="decimal(18,4)")]
        public decimal ExchangeRate { get; set; }


        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }

    }
}
