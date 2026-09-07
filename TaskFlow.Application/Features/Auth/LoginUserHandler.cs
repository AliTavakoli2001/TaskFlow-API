using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Auth;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = (await _unitOfWork.Repository<User>().FindAsync(u => u.Email == request.Dto.Email)).FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid Credentials.");

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResponseDto { Token = token, FullName = user.FullName };
    }
}