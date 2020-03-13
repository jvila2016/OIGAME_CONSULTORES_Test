
using OIGAME_CONSULTORES_Test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OIGAME_CONSULTORES_Test.Models.Security
{
    public class OIGAME_CONSULTORES_TestRoleProvider : RoleProvider
    {
        public bool AuthenticateUser(string username, string password)
        {
            bool isValid = DataHandler.loginUser(username, password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return isValid;
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = DataHandler.Get_UserRoll(username);
            return roles.ToArray();

        }

        public override bool IsUserInRole(string username, string roleName)
        {
            List<string> roles = DataHandler.Get_UserRoll(username);
            foreach(string role in roles)
            {
                if (role == roleName)
                    return true;
            }

            return false;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}