using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestOcbc.Models
{
    public class Customers
    {
        public long CustomersId { get; set; }

        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string CustomerName { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}