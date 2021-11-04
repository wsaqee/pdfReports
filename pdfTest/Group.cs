using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace pdfTest
{
    class Group
    {
        //public PrincipalContext ctx { get; }
        public string GroupDomain { get; }
        public string GroupDN { get; }
        public string GroupName { get; }
        public string GroupDescription { get; }
        public string EntryPath { get; }
        public string EntryUsername { get; }
        public string EntryPassword { get; }
        public bool EntryUseCredentials { get; }


        //public Group (PrincipalContext grpPrincipalContext, GroupPrincipal grp)
        //{
        //    this.ctx = grpPrincipalContext;
        //    this.GroupItem = grp;

        //    if (grp.Context.Name != null && grp.Context.Name.Length > 0)
        //        this.GroupDomain = grp.Context.Name;
        //    else
        //        this.GroupDomain = "";

        //    if (grp.Name != null && grp.Name.Length > 0)
        //        this.GroupName = grp.Name;
        //    else
        //        this.GroupName = "";

        //    if (grp.Description != null && grp.Description.Length > 0)
        //        this.GroupDescription = grp.Description;
        //    else
        //        this.GroupDescription = "";

        //}

        public Group(string groupDomain, string groupName, string groupDN, string groupDesc, string entryPath, string entryUsername, string entryPassword)
        {
            this.GroupDomain = groupDomain;
            this.GroupDN = groupDN;

            if (groupName != null && groupName.Length > 0)
                this.GroupName = groupName;
            else
                this.GroupName = "";

            if (groupDesc != null && groupDesc.Length > 0)
                this.GroupDescription = groupDesc;
            else
                this.GroupDescription = "";

            this.EntryPath = entryPath;
            
            if (entryUsername != null && entryUsername.Length > 0)
            {
                this.EntryUsername = entryUsername;
                this.EntryPassword = entryPassword;
                this.EntryUseCredentials = true;
            }
            else
            {
                this.EntryUsername = null;
                this.EntryPassword = null;
                this.EntryUseCredentials = false;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return GroupName;
        }
    }
}
