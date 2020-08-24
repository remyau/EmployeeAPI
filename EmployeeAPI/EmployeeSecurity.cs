using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataEmployee;

namespace EmployeeAPI
{
    public class EmployeeSecurity
    {
        public static bool Login(String username, String password)
        {
            using (EmployeeDBEntities entity = new EmployeeDBEntities())
            {
                return entity.Users.Any(user => user.UserName.Equals(username,
                    StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}