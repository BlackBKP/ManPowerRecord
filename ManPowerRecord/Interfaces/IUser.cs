using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface IUser
    {
        List<UserModel> GetUsers();
        string CreateUser(UserModel user);
        string UpdateUser(UserModel user);
    }
}
