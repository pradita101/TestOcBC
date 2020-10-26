using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestOcbc.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long CustomersId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "VARCHAR(2)")]
        [StringLength(2)]
        public string Status { get; set; }

        public double Amount { get; set; }
        public int point { get; set; }
        public DateTime TransDate { get; set; }
        public Customers Customers { get; set; }
    }
}