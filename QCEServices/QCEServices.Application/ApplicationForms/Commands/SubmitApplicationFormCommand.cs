using MediatR;
using QCEServices.Domain.Interfaces;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Shared.Enums;
using QCEServices.Shared.Responses;
using QCEServices.Shared.Responses.Errors;

namespace QCEServices.Application.ApplicationForms.Commands;

public sealed record SubmitApplicationFormCommand(Guid Id) : IRequest<Result<Guid>>, ICommand;

public sealed class SubmitApplicationFormCommandHandler(IApplicationFormRepository applicationFormRepository) : IRequestHandler<SubmitApplicationFormCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SubmitApplicationFormCommand request, CancellationToken cancellationToken)
    {
        var applicationForm = await applicationFormRepository.GetOneAsync(af => af.Id == request.Id, cancellationToken);
        if (applicationForm is null) return ApplicationFormError.NotFound(request.Id);

        applicationForm.Status = ApplicationFormStatus.Submitted;
        applicationForm.SubmittedAt = DateTime.UtcNow;
        applicationForm.SubmittedBy = "System";
        await applicationFormRepository.UpdateAsync(applicationForm, cancellationToken);

        return applicationForm.Id;
    }
}