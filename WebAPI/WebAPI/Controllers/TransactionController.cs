using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interface;
using WebAPI.Models;
using WebAPI.Validators;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IValidator<InsertTransactionViewModel> _insertTransactionValidator;
        private readonly IValidator<UpdateTransactionViewModel> _updateTransactionValidator;
        private readonly IValidator<DeleteTransactionViewModel> _deleteTransactionValidator;

        public TransactionController(
            ITransactionRepository transactionRepository,
            IValidator<InsertTransactionViewModel> insertTransactionValidator,
            IValidator<UpdateTransactionViewModel> updateTransactionValidator,
            IValidator<DeleteTransactionViewModel> deleteTransactionValidator
            )
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _insertTransactionValidator = insertTransactionValidator ?? throw new ArgumentNullException(nameof(insertTransactionValidator));
            _updateTransactionValidator = updateTransactionValidator ?? throw new ArgumentNullException(nameof(updateTransactionValidator));
            _deleteTransactionValidator = deleteTransactionValidator ?? throw new ArgumentNullException(nameof(deleteTransactionValidator));
        }

        [HttpGet]
        [Route("GetTransactions")]
        public async Task<IActionResult> Get()
        {
            var result = await _transactionRepository.GetTransactions();
            if (result == null || !result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTransactionByID/{id}")]
        public async Task<IActionResult> GetTransactionByID(int id)
        {
            var result = await _transactionRepository.GetTransactionByID(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("InsertTransaction")]
        public async Task<IActionResult> Post(Transaction transaction)
        {
            var validate = _insertTransactionValidator.Validate(new InsertTransactionViewModel
            {
                TransactionName = transaction.TransactionName,
                Cost = transaction.Cost,
                Date = transaction.Date
            });
            if (!validate.IsValid)            
                return BadRequest(validate.Errors);            
            
            var result = await _transactionRepository.InsertTransaction(transaction);
            if (result.TransactionID == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok(result);
        }


        [HttpPut]
        [Route("UpdateTransaction")]
        public async Task<IActionResult> Put(Transaction transaction)
        {
            var validate = _updateTransactionValidator.Validate(new UpdateTransactionViewModel
            {
                TransactionID = transaction.TransactionID,
                TransactionName = transaction.TransactionName,
                Cost = transaction.Cost,
                Date = transaction.Date
            });
            if (!validate.IsValid)
                return BadRequest(validate.Errors);

            var result = await _transactionRepository.UpdateTransaction(transaction);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteTransaction/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var validate = _deleteTransactionValidator.Validate(new DeleteTransactionViewModel
            {
                TransactionID = id
            });
            if (!validate.IsValid)
                return BadRequest(validate.Errors);

            await _transactionRepository.DeleteTransaction(id);
            return Ok(id);
        }
    }
}
