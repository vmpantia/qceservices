using QCEServices.Domain.Entities;
using QCEServices.Domain.Interfaces.Repositories;
using QCEServices.Infrastructure.DataAccess.Contexts;

namespace QCEServices.Infrastructure.DataAccess.Repositories;

public sealed class UserRepository(QCEServicesDbContext context) : BaseRepository<User>(context), IUserRepository;