using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestOcbc.Models
{
    public class MasterTransaksi
    {
        public int MasterTransaksiId { get; set; }

        [Column(TypeName = "VARCHAR(2)")]
        [StringLength(2)]
        public string TransactionCode { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(100)]
        public string TransactionName { get; set; }
    }
}