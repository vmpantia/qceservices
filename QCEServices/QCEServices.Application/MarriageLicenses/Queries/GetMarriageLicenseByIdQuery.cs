using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries;

public sealed record GetMarriageLicenseByIdQuery(Guid Id) : IRequest<Result<MarriageLicenseDto>>, IQuery;

public sealed class GetMarriageLicenseByIdQueryHandler(IMarriageLicenseRepository marriageLicenseRepository, IMapper mapper) : IRequestHandler<GetMarriageLicenseByIdQuery, Result<MarriageLicenseDto>>
{
    public async Task<Result<MarriageLicenseDto>> Handle(GetMarriageLicenseByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await marriageLicenseRepository
            .Get(ml => ml.Id == request.Id)
            .Include(tbl => tbl.ApplicationForm)
            .FirstOrDefaultAsync(cancellationToken);

        var result = mapper.Map<MarriageLicenseDto>(data);

        return result;
    }
}