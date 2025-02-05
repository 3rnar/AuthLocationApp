using MediatR;
using AuthLocationApp.Application.CQRS.Users.Commands;
using AuthLocationApp.Application.Interfaces.Repositories;
using AuthLocationApp.Application.Interfaces.Services;
using AuthLocationApp.Domain;
using AuthLocationApp.Domain.Exceptions;

namespace AuthLocationApp.Application.CQRS.Users.Commands.Handlers
{
   public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
   {
      private readonly IUserRepository _userRepository;
      private readonly IPasswordHasher _passwordHasher;

      public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
      {
         _userRepository = userRepository;
         _passwordHasher = passwordHasher;
      }

      public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
      {
         bool isExists = await _userRepository.EmailExistsAsync(request.User.Email, cancellationToken);
         if (isExists)
         {
            throw new DomainException("User with this email already exists.");
         }

         string hashedPassword = _passwordHasher.HashPassword(request.User.Password);

         var user = new User(0, request.User.Email, hashedPassword, request.User.CountryId!.Value, request.User.ProvinceId!.Value);

         await _userRepository.AddAsync(user, cancellationToken);
         await _userRepository.SaveChangesAsync(cancellationToken);

         var existingUser = await _userRepository.GetUserByEmailAsync(request.User.Email, cancellationToken);

         return existingUser != null ? existingUser.Id : 0;
      }
   }
}
