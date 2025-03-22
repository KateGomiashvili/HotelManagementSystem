

using HMS.Models.Entities;

namespace HMS.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<ApplicationUser>, IUpdatable<ApplicationUser>, ISavable
    {
    }
}
