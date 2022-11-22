using System;
using System.Collections.Generic;
using System.Linq;
using MedScan.Models;

namespace MedScan.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        //new DAO object instantiated
        public SecurityService()
        {

        }

        public bool ValidUser(User user)
        {
            return usersDAO.ValidateUserByUsernameAndPassword(user);
            //checks if user is present in the database
        }
    }
}

