﻿using FigoparaCaseStudyApi.Request;
using FluentValidation;

namespace FigoparaCaseStudyApi.Validations
{
    public class UserAddValidation : AbstractValidator<UserAddRequest>
    {
        public UserAddValidation()
        {
            RuleFor(f => f.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Hatalı mail adres");

            RuleFor(f => f.Phone)
                .NotEmpty();

            RuleFor(f => f.Name)
                .NotEmpty();

            RuleFor(f => f.Surname)
                .NotEmpty();

            RuleFor(f => f.Password)
                .NotEmpty();
         
            RuleFor(f => f.ConfirmPassword)
                .Equal(f => f.Password)
                .WithMessage("Hatalı şifre");
        }
    }
}