using MediatR;
using AuthLocationApp.Application.DTOs.Users;

namespace AuthLocationApp.Application.CQRS.Users.Commands
{
   public record CreateUserCommand(UserCreateDto User) : IRequest<int>;
}
