using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Commands.CreateMarriageLicense;

public sealed record CreateMarriageLicenseCommand(SaveMarriageLicenseDto MarriageLicense) : IRequest<Result<Guid>>, ICommand;