using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.DataAccessLayer
{
    internal class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
