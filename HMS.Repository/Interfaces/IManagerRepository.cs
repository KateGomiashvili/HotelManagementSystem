

using HMS.Models.Entities;

namespace HMS.Repository.Interfaces
{
    public interface IManagerRepository : IRepositoryBase<Manager>, IUpdatable<Manager>, ISavable
    {
    }
}
