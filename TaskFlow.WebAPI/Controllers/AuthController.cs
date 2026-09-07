using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.Features.Auth;

namespace TaskFlow.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<RegisterUserDto> _validator;

    public AuthController(IMediator mediator, IValidator<RegisterUserDto> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto dto)
    {
        var validatorResult = await _validator.ValidateAsync(dto);
        if (!validatorResult.IsValid)
        {
            var errors = validatorResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(errors);
        }

        try
        {
            var result = await _mediator.Send(new RegisterUserCommand(dto));
            return Ok(result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        try
        {
            var result = await _mediator.Send(new LoginUserQuery(dto));
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new { message = e.Message });
        }
    }
}