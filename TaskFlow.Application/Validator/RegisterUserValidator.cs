using FluentValidation;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Validator.Services;

namespace TaskFlow.Application.Validator;

public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100).WithMessage("Name is Required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(15).WithMessage("Password must have at least 6 characters.");
        RuleFor(x => x.Email).NotEmpty().Must(ValidationServices.IsValidEmail).WithMessage("Email is invalid.");
    }
}