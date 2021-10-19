using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsrMgm
{
    class Program
    {
        private static bool CreateUser(string firstName, string lastName, string dispName, string userLogonName/*, string employeeID*/, string emailAddress/*, string telephone, string address*/)
        {
            // Creating the PrincipalContext
            PrincipalContext principalContext = null;
            try
            {
                principalContext = new PrincipalContext(ContextType.Domain, "192.168.0.80", "mnestic", "mn2021!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create PrincipalContext. Exception: " + e);
                return false;
            }

            // Check if user object already exists in the store
            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, userLogonName);
            if (usr != null)
            {
                Console.WriteLine(userLogonName + " already exists. Please use a different User Logon Name.");
                return false;
            }

            // Create the new UserPrincipal object
            UserPrincipal userPrincipal = new UserPrincipal(principalContext);

            if (lastName != null && lastName.Length > 0)
                userPrincipal.Surname = lastName;

            if (firstName != null && firstName.Length > 0)
                userPrincipal.GivenName = firstName;

            if (dispName != null && dispName.Length > 0) 
            { 
                userPrincipal.DisplayName = dispName;
                userPrincipal.Name = dispName;
            }

            //if (employeeID != null && employeeID.Length > 0)
            //    userPrincipal.EmployeeId = employeeID;

            if (emailAddress != null && emailAddress.Length > 0)
                userPrincipal.EmailAddress = emailAddress;

            //if (telephone != null && telephone.Length > 0)
            //    userPrincipal.VoiceTelephoneNumber = telephone;

            if (userLogonName != null && userLogonName.Length > 0)
            {
                userPrincipal.SamAccountName = userLogonName;
                userPrincipal.UserPrincipalName = userLogonName + "@" + "dummy.org";
            }          
            
            string pwdOfNewlyCreatedUser = firstName.Substring(0, 1).ToLower() + lastName.Substring(0, 1).ToLower() + DateTime.Now.Year.ToString() + "!!";
            userPrincipal.SetPassword(pwdOfNewlyCreatedUser);

            userPrincipal.Enabled = true;
            //userPrincipal.ExpirePasswordNow();

            try
            {
                userPrincipal.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception creating user object. " + e);
                return false;
            }

            return true;
        }

        private static bool DeleteUser (string userName)
        {
            // Creating the PrincipalContext
            PrincipalContext principalContext = null;
            try
            {
                principalContext = new PrincipalContext(ContextType.Domain, "192.168.0.80", "mnestic", "mn2021!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create PrincipalContext. Exception: " + e);
                return false;
            }

            // Check if user object already exists in the store
            UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, userName);
            if (usr == null)
            {
                Console.WriteLine(userName + " does not exists. Please use a different User Logon Name.");
                return false;
            }

            try
            {
                usr.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception deleting user object. " + e);
                return false;
            }

            return true;
        }

        static void Main(string[] args)
        {
            List<string> names = File.ReadAllLines("names.txt").ToList();
            Console.WriteLine("GO?");
            ConsoleKey k = Console.ReadKey().Key;
            if ( k == ConsoleKey.Y)
            {
                foreach (string name in names)
                {
                    string firstName = name.Split(' ')[0];
                    string lastName = name.Split(' ')[1];
                    string dispName = firstName + " " + lastName;
                    string username = firstName.First().ToString().ToLower() + lastName.ToLower();
                    string email = firstName.ToLower() + "." + lastName.ToLower() + "@" + "dummycorp.com";

                    Console.WriteLine(username + "\t\t" + CreateUser(firstName, lastName, dispName, username, email).ToString());
                }
            }else
                if (k == ConsoleKey.D)
            {
                foreach (string name in names)
                {
                    string firstName = name.Split(' ')[0];
                    string lastName = name.Split(' ')[1];
                    string dispName = firstName + " " + lastName;
                    string username = firstName.First().ToString().ToLower() + lastName.ToLower();
                    string email = firstName.ToLower() + "." + lastName.ToLower() + "@" + "dummycorp.com";

                    Console.WriteLine(username + "\t\t" + DeleteUser(username).ToString());
                }
            }
            Console.ReadLine();
        }
            

    }
}

