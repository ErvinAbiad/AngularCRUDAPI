using FluentValidation;
using System;
using WebAPI.Interface;
using WebAPI.ViewModels;

namespace WebAPI.Validators
{
    public class DeleteTransactionValidator : AbstractValidator<DeleteTransactionViewModel>, IValidator<DeleteTransactionViewModel>
    {
        private readonly ITransactionRepository _transactionRepository;
        public DeleteTransactionValidator(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(_ => _.TransactionID)
             .NotEmpty()
                 .Must(_ => BeExistingTransaction(_))
                    .WithMessage("Transaction not found.");
        }

        private bool BeExistingTransaction(int id)
        {
            var transaction = _transactionRepository.GetTransactionByID(id);
            if (transaction != null)
            {
                return true;
            }
            return false;
        }
    }
}
