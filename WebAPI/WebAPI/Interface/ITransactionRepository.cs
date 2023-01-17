using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<Transaction> GetTransactionByID(int ID);
        Task<Transaction> InsertTransaction(Transaction objTransaction);
        Task<Transaction> UpdateTransaction(Transaction objTransaction);
        Task<bool> DeleteTransaction(int ID);
    }
}
