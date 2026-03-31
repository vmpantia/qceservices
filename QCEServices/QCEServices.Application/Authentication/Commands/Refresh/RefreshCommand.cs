using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.Authentication.Commands.Refresh;

public sealed record RefreshCommand(string RefreshToken) : IRequest<Result<AuthTokens>>, ICommand;