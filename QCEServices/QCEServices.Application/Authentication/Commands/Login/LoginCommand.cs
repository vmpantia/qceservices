using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Models.Dtos.Authentication;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.Authentication.Commands.Login;

public sealed record LoginCommand(LoginDto Login) : IRequest<Result<string>>, ICommand;