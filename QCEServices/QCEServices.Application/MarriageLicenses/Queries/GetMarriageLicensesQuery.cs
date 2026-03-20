using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QCEServices.Application.Contracts;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.MarriageLicenses.Dtos;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries;

public sealed record GetMarriageLicensesQuery : IRequest<Result<IEnumerable<MarriageLicenseDto>>>, IQuery;

public sealed class GetMarriageLicensesQueryHandler(IMarriageLicenseRepository marriageLicenseRepository, IMapper mapper) : IRequestHandler<GetMarriageLicensesQuery, Result<IEnumerable<MarriageLicenseDto>>>
{
    public async Task<Result<IEnumerable<MarriageLicenseDto>>> Handle(GetMarriageLicensesQuery request, CancellationToken cancellationToken)
    {
        var data = await marriageLicenseRepository
            .Get()
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<MarriageLicenseDto>>(data);

        return result;
    }
}