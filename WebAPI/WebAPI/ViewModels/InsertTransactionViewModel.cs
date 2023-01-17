using System;

namespace WebAPI.ViewModels
{
    public class InsertTransactionViewModel
    {
        public string TransactionName { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? Date { get; set; }
    }
}
