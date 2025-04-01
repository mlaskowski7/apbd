using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.DataAccessLayer
{
    public interface IUserRepository
    {
        void AddUser(User user);
    }
}
