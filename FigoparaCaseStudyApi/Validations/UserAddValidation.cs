using FigoparaCaseStudyApi.Request;
using FluentValidation;

namespace FigoparaCaseStudyApi.Validations
{
    public class UserAddValidation : AbstractValidator<UserAddRequest>
    {
        public UserAddValidation()
        {
           RuleFor(f => f.Name)
             .NotEmpty()
             .NotNull();
            

            RuleFor(f => f.Surname)
               .NotEmpty()
               .NotNull();

            RuleFor(f => f.Password)
               .NotEmpty()
               .NotNull();

            RuleFor(f => f.ConfirmPassword)
               .Equal(f => f.Password)
               .WithMessage("Hatalı şifre");

            RuleFor(f => f.Email)
               .NotEmpty()
               .NotNull()
               .EmailAddress()
               .WithMessage("Hatalı mail adres");
           

            RuleFor(f => f.Phone)
               .NotEmpty()
               .NotNull();
           
        }

       
    }
}