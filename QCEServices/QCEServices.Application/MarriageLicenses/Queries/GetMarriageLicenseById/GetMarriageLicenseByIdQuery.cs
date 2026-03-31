using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries.GetMarriageLicenseById;

public sealed record GetMarriageLicenseByIdQuery(Guid Id) : IRequest<Result<MarriageLicenseDto>>, IQuery;