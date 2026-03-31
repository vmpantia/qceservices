using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries.GetMarriageLicenses;

public sealed record GetMarriageLicensesQuery : IRequest<Result<IEnumerable<MarriageLicenseDto>>>, IQuery;