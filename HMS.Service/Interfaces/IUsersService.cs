using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Interfaces
{
    public interface IUsersService
    {
        Task DeleteManager(string managerId);
        Task DeleteGuest(string guestId);
    }
}
