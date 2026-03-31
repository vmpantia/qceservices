using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries.GetMarriageLicenses;

public sealed class GetMarriageLicensesQueryHandler(IMarriageLicenseRepository marriageLicenseRepository, IMapper mapper) : IRequestHandler<GetMarriageLicensesQuery, Result<IEnumerable<MarriageLicenseDto>>>
{
    public async Task<Result<IEnumerable<MarriageLicenseDto>>> Handle(GetMarriageLicensesQuery request, CancellationToken cancellationToken)
    {
        var data = await marriageLicenseRepository
            .Get()
            .Include(tbl => tbl.ApplicationForm)
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<MarriageLicenseDto>>(data);

        return result;
    }
}