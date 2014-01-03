using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Sitecore;
using Sitecore.Diagnostics;
using System.Web.Security;
using Sitecore.Security.Accounts;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Sitecore.Security.AccessControl;

namespace ExtendedSitecoreAPI
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Security : System.Web.Services.WebService
    {
        private Sitecore.SecurityModel.Credentials _Credentials = null;


        public Sitecore.SecurityModel.Credentials credentials
        {
            get
            {
                return _Credentials;
            }

            set
            {
                value = _Credentials;
            }
        }

        private void Login(Sitecore.SecurityModel.Credentials credentials)
        {
            Error.AssertObject(credentials, "credentials");
            if (Sitecore.Context.IsLoggedIn)
            {
                if (Sitecore.Context.User.Name.Equals(credentials.UserName))
                {
                    return;
                }
                Sitecore.Context.Logout();
            }
            Assert.IsTrue(Membership.ValidateUser(credentials.UserName, credentials.Password), "Unknown username or password.");
            UserSwitcher.Enter(Sitecore.Security.Accounts.User.FromName(credentials.UserName, true));
        }


        [WebMethod]
        public bool CreateUser(string sDomainUser, string sPassword, string sEmail, string sFullName, string sRoles, bool bIsAdmin, Sitecore.SecurityModel.Credentials credentials)
        {
            if (sDomainUser != "")
            {
                Login(credentials);
                if (!Sitecore.Security.Accounts.User.Exists(sDomainUser))
                {
                    System.Web.Security.Membership.CreateUser(sDomainUser, "dummy", sEmail);
                    Sitecore.Security.Accounts.User newUser = Sitecore.Security.Accounts.User.FromName(sDomainUser, true);
                    newUser.Profile.LegacyPassword = sPassword;
                    newUser.Profile.FullName = sFullName;
                    newUser.Profile.IsAdministrator = bIsAdmin;
                    foreach (string sRole in sRoles.Split('|'))
                    {
                        if (sRole != "")
                        {
                            CreateRole(sRole, credentials);
                            newUser.Roles.Add(Sitecore.Security.Accounts.Role.FromName(sRole));
                        }
                    }
                    newUser.Profile.Save();
                }
            }
            return true;
        }


        /// <summary>
        /// The CreateRole() method creates a role in a domain,
        /// if that role does not already exist.
        /// sDomainRole ("domain\role")
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool CreateRole(string sDomainRole, Sitecore.SecurityModel.Credentials credentials)
        {
            Login(credentials);
            if (!Sitecore.Security.Accounts.Role.Exists(sDomainRole))
            {
                System.Web.Security.Roles.CreateRole(sDomainRole);
            }
            return true;
        }

        [WebMethod]
        public string[] GetRoles(Sitecore.SecurityModel.Credentials credentials)
        {
            Login(credentials);
            Sitecore.Security.SitecoreRoleProvider provider = new Sitecore.Security.SitecoreRoleProvider();            
            Sitecore.Security.Accounts.RoleList roles = Sitecore.Security.Accounts.RoleList.FromNames(provider.GetAllRoles());
            string[] sRoles = new string[roles.Count];
            for (int i = 0; i < roles.Count; i++)
            {
                sRoles[i] = roles[i].Name;
            }
            return sRoles;
        }

        /// <summary>
        /// Make one role inherit from another
        /// </summary>
        [WebMethod]
        public bool AddRoleToRole(string sDomainRole, string sTargetRole, Sitecore.SecurityModel.Credentials credentials)
        {
            Login(credentials);

            // If TargetRole doesn't exist, then create it.
            if (!Sitecore.Security.Accounts.Role.Exists(sTargetRole))
            {
                System.Web.Security.Roles.CreateRole(sTargetRole);
            }

            Role domainRole = Sitecore.Security.Accounts.Role.FromName(sDomainRole);
            Role targetRole = Sitecore.Security.Accounts.Role.FromName(sTargetRole);

            if ((domainRole == null) || (targetRole == null))
                return false;

            if (! Sitecore.Security.Accounts.RolesInRolesManager.IsRoleInRole(domainRole, targetRole, true))
                Sitecore.Security.Accounts.RolesInRolesManager.AddRoleToRole(domainRole, targetRole);
            return true;
        }


        /// <summary>
        /// The System.Web.Security.Roles.DeleteRole() method removes all members 
        /// from the role specified by the first parameter, and then removes that role. 
        /// 
        /// Note!  Depending on the number of users, deleting a role can be a long-running operation
        /// </summary>
        /// <param name="sDomainRole"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DeleteRole(string sDomainRole, Sitecore.SecurityModel.Credentials credentials)
        {
            Login(credentials);
            if (Sitecore.Security.Accounts.Role.Exists(sDomainRole))
            {
                System.Web.Security.Roles.DeleteRole(sDomainRole);
            }
            return true;
        }

        // Delete mutiple user using wildcard, should be used with care, example: sitecore/*
        [WebMethod]
        public int DeleteUsers(string sDomainUsers, Sitecore.SecurityModel.Credentials credentials)
        {
            Login(credentials);
            int iCount = 0;
            MembershipUserCollection users = System.Web.Security.Membership.FindUsersByName(sDomainUsers);
            foreach (MembershipUser user in users)
            {
                if (Sitecore.Security.Accounts.User.Exists(user.UserName))
                {
                    System.Web.Security.Membership.DeleteUser(user.UserName);
                    iCount++;
                }
            }
            return iCount;
        }

        private void SetRight(Sitecore.Data.Items.Item item, Sitecore.Security.Accounts.Account account, Sitecore.Security.AccessControl.AccessRight right, Sitecore.Security.AccessControl.AccessPermission rightState, Sitecore.Security.AccessControl.PropagationType propagationType) 
        { 
            Sitecore.Security.AccessControl.AccessRuleCollection accessRules = item.Security.GetAccessRules();
            if (propagationType == Sitecore.Security.AccessControl.PropagationType.Any) 
            { 
                accessRules.Helper.RemoveExactMatches(account, right); 
            } 
            else 
            { 
                accessRules.Helper.RemoveExactMatches(account, right, propagationType); 
            } 
            if (rightState != Sitecore.Security.AccessControl.AccessPermission.NotSet) 
            { 
                if (propagationType == Sitecore.Security.AccessControl.PropagationType.Any) 
                { 
                    accessRules.Helper.AddAccessPermission(account, right, Sitecore.Security.AccessControl.PropagationType.Entity, rightState); 
                    accessRules.Helper.AddAccessPermission(account, right, Sitecore.Security.AccessControl.PropagationType.Descendants, rightState); 
                } 
                else 
                { 
                    accessRules.Helper.AddAccessPermission(account, right, propagationType, rightState); 
                } 
            } 
            item.Security.SetAccessRules(accessRules); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDatabase"></param>
        /// <param name="strItem"></param>
        /// <param name="strAccount"></param>
        /// <param name="strRight"></param>
        /// <param name="rightState"></param>
        /// <param name="propagationType"></param>
        /// <param name="credentials"></param>
        [WebMethod]
        public void SetRight(string strDatabase, string strItem, string strAccount, string strRights, Sitecore.Security.AccessControl.AccessPermission rightState, Sitecore.Security.AccessControl.PropagationType propagationType, Sitecore.SecurityModel.Credentials credentials)
        {
            Error.AssertString(strDatabase, "strDatabase", false);
            Error.AssertString(strItem, "strItem", false);
            Error.AssertString(strAccount, "strAccount", false);
            Error.AssertString(strRights, "strRights", false);

            Login(credentials);

            Sitecore.Data.Database db = Sitecore.Configuration.Factory.GetDatabase(strDatabase); 
            Sitecore.Data.Items.Item item = db.GetItem(strItem); 
            Sitecore.Security.Accounts.AccountType accountType = Sitecore.Security.Accounts.AccountType.User;
            if (Sitecore.Security.SecurityUtility.IsRole(strAccount)) 
            { 
                accountType = Sitecore.Security.Accounts.AccountType.Role;
            } 
            Sitecore.Security.Accounts.Account account = Sitecore.Security.Accounts.Account.FromName(strAccount, accountType);

            // Always ensure that a minimum of 1 "|" character exists 
            if (strRights.IndexOf("|") == -1)
                strRights += '|';

            string[] strRightsList = strRights.Split('|');
            for (int t = 0; t < strRightsList.Length; t++)
            {
                string strRight = strRightsList[t];
                if ((strRight != null) && (strRight != ""))
                {
                    Sitecore.Security.AccessControl.AccessRight right = Sitecore.Security.AccessControl.AccessRight.FromName(strRight);
                    SetRight(item, account, right, rightState, propagationType);
                }
            }
        }

        [WebMethod]
        public string GetRight(string strDatabase, string strItem, string strAccount, SecurityPermission rightState, Sitecore.SecurityModel.Credentials credentials)        
        {            
            Error.AssertString(strDatabase, "strDatabase", false);
            Error.AssertString(strItem, "strItem", false);

            Login(credentials);

            Sitecore.Data.Database db = Sitecore.Configuration.Factory.GetDatabase(strDatabase);
            Sitecore.Data.Items.Item item = db.GetItem(strItem);

            if (strAccount.IndexOf("sitecore\\") == -1)
                strAccount = "sitecore\\" + strAccount;

            Sitecore.Security.Accounts.AccountType accountType = Sitecore.Security.Accounts.AccountType.User;
            if (Sitecore.Security.SecurityUtility.IsRole(strAccount))
            {
                accountType = Sitecore.Security.Accounts.AccountType.Role;
            }
            Sitecore.Security.Accounts.Account account = Sitecore.Security.Accounts.Account.FromName(strAccount, accountType);


            string sResults = "";
            if (rightState == SecurityPermission.AllowAccess)
            {
                if (item.Security.CanAdmin(account))
                    sResults += AccessRight.ItemAdmin + "|";
                if (item.Security.CanCreate(account))
                    sResults += AccessRight.ItemCreate + "|";
                if (item.Security.CanDelete(account))
                    sResults += AccessRight.ItemDelete + "|";
                if (item.Security.CanRead(account))
                    sResults += AccessRight.ItemRead + "|";
                if (item.Security.CanRename(account))
                    sResults += AccessRight.ItemRename + "|";
                if (item.Security.CanWrite(account))
                    sResults += AccessRight.ItemWrite + "|";
            }
            else if (rightState == SecurityPermission.DenyAccess)
            {
                if (!item.Security.CanAdmin(account))
                    sResults += AccessRight.ItemAdmin + "|";
                if (!item.Security.CanCreate(account))
                    sResults += AccessRight.ItemCreate + "|";
                if (!item.Security.CanDelete(account))
                    sResults += AccessRight.ItemDelete + "|";
                if (!item.Security.CanRead(account))
                    sResults += AccessRight.ItemRead + "|";
                if (!item.Security.CanRename(account))
                    sResults += AccessRight.ItemRename + "|";
                if (!item.Security.CanWrite(account))
                    sResults += AccessRight.ItemWrite + "|";
            }
            return sResults;            
        }

        private string GetAccessPermission(AccessRuleCollection rules, Sitecore.Security.Accounts.Account account, AccessRight accessRight, AccessPermission accessPermission, string sExistingPermissions)
        {
            if ((rules.Helper.GetAccessPermission(account, accessRight, PropagationType.Descendants) == accessPermission) &&
                (sExistingPermissions.IndexOf(accessRight.Name) == -1))
            {
                sExistingPermissions += accessRight.Name + "|";
            }
            return sExistingPermissions;
        }




        [WebMethod]
        public string GetAccessRights()
        {
            string sResult = "";
            Sitecore.Security.AccessControl.AccessRightCollection col = Sitecore.Security.AccessControl.AccessRightManager.GetAccessRights();
            foreach (Sitecore.Security.AccessControl.AccessRight right in col)
            {
                sResult += right.Name + "|";
            }
            return sResult;
//            Sitecore.Security.AccessControl.AccessRightProvider tmp = Sitecore.Security.AccessControl.AccessRightProvider();
        }

        [WebMethod]
        public string CreateStandardValues(string strItem, Sitecore.SecurityModel.Credentials credentials)
        {
            Error.AssertString(strItem, "strItem", false);

            Login(credentials);
            using (new SecurityDisabler())
            {
                // get database for which the template will be created
                Database db = Sitecore.Configuration.Factory.GetDatabase("master");

                Item  standardValuesItem = db.Templates[strItem].CreateStandardValues();
                return standardValuesItem.ID.ToString();
            }

        }
    }
}
