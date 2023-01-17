using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public string TransactionName { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? Date { get; set; }
    }
}
