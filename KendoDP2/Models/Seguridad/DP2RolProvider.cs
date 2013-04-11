using KendoDP2.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace KendoDP2.Models.Seguridad
{
    public class DP2RolProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
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

        public override string[] GetRolesForUser(string username)
        {
            using (DP2Context db = new DP2Context())
            {
                var user = db.TablaUsuarios.Dbset.FirstOrDefault(u => u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                var roles = from ur in user.Roles
                            from r in db.TablaRoles.Dbset
                            where ur.ID == r.ID
                            select r.Nombre;
                if (roles != null)
                    return roles.ToArray();
                else
                    return new string[] { }; ;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (DP2Context db = new DP2Context())
            {
                var user = db.TablaUsuarios.Dbset.FirstOrDefault(u => u.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                var roles = from ur in user.Roles
                            from r in db.TablaRoles.Dbset
                            where ur.ID == r.ID
                            select r.Nombre;
                if (user != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}