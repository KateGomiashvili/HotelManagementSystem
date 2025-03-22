using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Interfaces
{
    public interface IGuestRepository : IRepositoryBase<Guest> , IUpdatable<Guest> , ISavable
    {
    }
}
