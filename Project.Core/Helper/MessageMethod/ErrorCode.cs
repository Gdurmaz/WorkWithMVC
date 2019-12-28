using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Helper.MessageMethod
{
    public enum ErrorCode
    {
        UserIsNotActive = 201,//Login
        UsernameOrPasswordWrong = 202,
        CheckYourEmail = 203,
        UserAllreadyActive = 204,
        ActiveIDDoesNotExists = 205,
        UsernameAlreadyExists = 341,//Register
        UserCouldNotInserted = 342,
        UserNotFound = 343,
        ProfileCouldNotUpdate = 344,
        UserCouldNotRemove = 345,
    }
}
