using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using QCEServices.Application.ApplicationForms;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Extensions;
using QCEServices.Shared.Responses;

namespace QCEServices.Application.MarriageLicenses.Commands.CreateMarriageLicense;

public sealed class CreateMarriageLicenseCommandHandler(IMarriageLicenseRepository marriageLicenseRepository, IMapper mapper, 
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateMarriageLicenseCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateMarriageLicenseCommand request, CancellationToken cancellationToken)
    {
        var entity = MarriageLicenseBuilder.Empty()
            .WithMarriageLicense(mapper.Map<MarriageLicense>(request.MarriageLicense))
            .WithApplicationForm(ApplicationFormBuilder.Empty()
                .WithType(ApplicationFormType.MarriageLicense)
                .WithStatus(ApplicationFormStatus.Saved)
                .WithApplicantId(httpContextAccessor.HttpContext!.User.GetUpn()))
            .Build();
        
        var result = await marriageLicenseRepository.CreateAsync(entity, cancellationToken);
        return result.Id;
    }
}