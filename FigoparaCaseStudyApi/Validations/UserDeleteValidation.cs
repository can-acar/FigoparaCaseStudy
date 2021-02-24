using FigoparaCaseStudyApi.Request;
using FluentValidation;

namespace FigoparaCaseStudyApi.Validations
{
    public class UserDeleteValidation : AbstractValidator<UserDeleteRequest>
    {
        public UserDeleteValidation()
        {
            RuleFor(f => f.Id)
                .NotEmpty();
        }
    }
}