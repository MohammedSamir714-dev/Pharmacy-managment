using FluentValidation;

namespace Pharmacy_managment.Contract.Authentication
{
    public class LogginValidation : AbstractValidator<LogginRequest>
    {
        public LogginValidation()
        {
            RuleFor(x=>x.Email).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}
