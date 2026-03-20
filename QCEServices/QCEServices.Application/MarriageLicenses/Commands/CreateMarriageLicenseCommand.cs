using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using QCEServices.Application.ApplicationForms;
using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Extensions;
using QCEServices.Shared.Models.Dtos.MarriageLicenses;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Validators.MarriageLicenses;

namespace QCEServices.Application.MarriageLicenses.Commands;

public sealed record CreateMarriageLicenseCommand(SaveMarriageLicenseDto MarriageLicense) : IRequest<Result<Guid>>, ICommand;

public sealed class CreateMarriageLicenseCommandValidator : AbstractValidator<CreateMarriageLicenseCommand>
{
    public CreateMarriageLicenseCommandValidator(IMarriageLicenseRepository marriageLicenseRepository)
    {
        RuleFor(cml => cml.MarriageLicense).SetValidator(new SaveMarriageLicenseValidator());

        RuleFor(cml => cml.MarriageLicense)
            .MustAsync(async (sml, ct) =>
            {
                // Check if any party (groom or bride) has existing marriage license
                var result = await marriageLicenseRepository.IsExistAsync(
                    expression: ml => (ml.Groom.Name.FirstName == sml.Groom.Name.FirstName && ml.Groom.Name.LastName == sml.Groom.Name.LastName) ||
                                      (ml.Bride.Name.FirstName == sml.Bride.Name.FirstName && ml.Bride.Name.LastName == sml.Bride.Name.LastName),
                    cancellationToken: ct);
                return !result;
            })
            .WithMessage("One of the parties have existing marriage license on our system.");
    }
}

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