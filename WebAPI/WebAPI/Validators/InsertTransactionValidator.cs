using FluentValidation;
using WebAPI.ViewModels;

namespace WebAPI.Validators
{
    public class InsertTransactionValidator : AbstractValidator<InsertTransactionViewModel>, IValidator<InsertTransactionViewModel>
    {

        public InsertTransactionValidator()
        {
            CreateRules();
        }

        private void CreateRules()
        {

            RuleFor(_ => _.TransactionName)
                .NotEmpty();

            RuleFor(_ => _.Cost)
                .NotNull();

            RuleFor(_ => _.Date)
                .NotEmpty();
        }
    }
}
