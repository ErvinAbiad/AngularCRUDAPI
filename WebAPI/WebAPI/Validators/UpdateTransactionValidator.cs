using FluentValidation;
using System;
using WebAPI.Interface;
using WebAPI.ViewModels;

namespace WebAPI.Validators
{
    public class UpdateTransactionValidator : AbstractValidator<UpdateTransactionViewModel>, IValidator<UpdateTransactionViewModel>
    {
        private readonly ITransactionRepository _transactionRepository;
        public UpdateTransactionValidator(ITransactionRepository transactionRepository)
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

            RuleFor(_ => _.TransactionName)
                .NotEmpty();

            RuleFor(_ => _.Cost)
                .NotNull();

            RuleFor(_ => _.Date)
                .NotEmpty();
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
