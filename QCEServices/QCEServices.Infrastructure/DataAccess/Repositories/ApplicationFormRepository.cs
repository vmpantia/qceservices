using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Infrastructure.DataAccess.Contexts;

namespace QCEServices.Infrastructure.DataAccess.Repositories;

public sealed class ApplicationFormRepository(QCEServicesDbContext context) : BaseRepository<ApplicationForm>(context), IApplicationFormRepository;