using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}