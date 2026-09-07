using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<User>();
        var existing = (await repo.FindAsync(u => u.Email == request.Dto.Email)).FirstOrDefault();
        if (existing != null)
            throw new ApplicationException("Email already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.Dto.FullName,
            Email = request.Dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password),
        };

        await repo.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResponseDto { Token = token, FullName = user.FullName };
    }
}