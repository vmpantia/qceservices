using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.ApplicationForms.Commands.SubmitApplicationForm;

public sealed record SubmitApplicationFormCommand(Guid Id) : IRequest<Result<Guid>>, ICommand;