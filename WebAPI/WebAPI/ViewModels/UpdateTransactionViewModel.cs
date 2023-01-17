using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class UpdateTransactionViewModel
    {
        public int TransactionID { get; set; }
        public string TransactionName { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? Date { get; set; }
    }
}
