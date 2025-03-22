

using HMS.Models.Entities;

namespace HMS.Repository.Interfaces
{
    public interface IHotelRepository : IRepositoryBase<Hotel>, IUpdatable<Hotel>, ISavable
    {
    }
}
