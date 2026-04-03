using MediatR;
using QCEServices.Domain.Interfaces.Authentication;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Application.Authentication.Commands.Login;

public sealed class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetOneAsync(expression: u =>
                u.Username == request.Login.UsernameOrEmail ||
                u.Email == request.Login.UsernameOrEmail,
            cancellationToken);

        if (user is null) 
            return UserError.UsernameOrEmailNotFound();
        
        if (!passwordHasher.Verify(request.Login.Password, user.Password))
            return UserError.PasswordIncorrect();
        
        return tokenProvider.Create(user);
    }
}