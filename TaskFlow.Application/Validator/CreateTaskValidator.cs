using FluentValidation;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Validator;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
    }
}