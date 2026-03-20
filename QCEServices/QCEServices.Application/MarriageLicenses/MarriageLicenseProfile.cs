using AutoMapper;
using QCEServices.Domain.Entities;
using QCEServices.Shared.MarriageLicenses.Dtos;

namespace QCEServices.Application.MarriageLicenses;

public sealed class MarriageLicenseProfile : Profile
{
    public MarriageLicenseProfile()
    {
        CreateMap<MarriageLicense, MarriageLicenseDto>()
            .ForMember(dst => dst.LastModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt ?? src.CreatedAt))
            .ForMember(dst => dst.LastModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy ?? src.CreatedBy));
        CreateMap<SaveMarriageLicenseDto, MarriageLicense>();
    }
}