using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repository
{

    public class TransactionRepository : ITransactionRepository
    {
        private readonly APIDbContext _appDBContext;

        public TransactionRepository(APIDbContext context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _appDBContext.Transactions.AsNoTracking().ToListAsync();
        }

        public async Task<Transaction> GetTransactionByID(int ID)
        {
            return await _appDBContext.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.TransactionID == ID);
        }

        public async Task<Transaction> InsertTransaction(Transaction objTransaction)
        {
            _appDBContext.Transactions.Add(objTransaction);
            await _appDBContext.SaveChangesAsync();
            return objTransaction;
        }

        public async Task<Transaction> UpdateTransaction(Transaction objTransaction)
        {
            _appDBContext.Entry(objTransaction).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objTransaction;
        }

        public async Task<bool> DeleteTransaction(int ID)
        {
            bool result = false;
            var department = _appDBContext.Transactions.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                await _appDBContext.SaveChangesAsync();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }


    }
}
