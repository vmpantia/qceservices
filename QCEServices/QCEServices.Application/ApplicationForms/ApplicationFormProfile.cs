using AutoMapper;
using QCEServices.Domain.Entities;
using QCEServices.Shared.Models.Dtos.ApplicationForms;

namespace QCEServices.Application.ApplicationForms;

public sealed class ApplicationFormProfile : Profile
{
    public ApplicationFormProfile()
    {
        CreateMap<ApplicationForm, ApplicationFormDto>()
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
    }
}