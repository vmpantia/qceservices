using AutoMapper;
using MediatR;
using QCEServices.Application.Contracts;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.MarriageLicenses.Dtos;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Queries;

public sealed record GetMarriageLicenseByIdQuery(Guid Id) : IRequest<Result<MarriageLicenseDto>>, IQuery;

public sealed class GetMarriageLicenseByIdQueryHandler(IMarriageLicenseRepository marriageLicenseRepository, IMapper mapper) : IRequestHandler<GetMarriageLicenseByIdQuery, Result<MarriageLicenseDto>>
{
    public async Task<Result<MarriageLicenseDto>> Handle(GetMarriageLicenseByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await marriageLicenseRepository
            .GetOneAsync(ml => ml.Id == request.Id, cancellationToken);

        var result = mapper.Map<MarriageLicenseDto>(data);

        return result;
    }
}