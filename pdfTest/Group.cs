using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfTest
{
    class Group
    {
        public PrincipalContext ctx { get; }
        //public ContextType GroupContextType { get; }
        public string GroupName { get; }
        public string GroupDescription { get; }
        public string GroupDomain { get; }
        public GroupPrincipal GroupItem { get; }

        public List<Principal> groupUsers = new List<Principal>();
        //public Group(ContextType grpContextType, string grpName)
        /*public Group(PrincipalContext grpPrincipalContext, string domain, string grpName, string grpDesc)
        {
            this.ctx = grpPrincipalContext;
            this.GroupDomain = domain;
            //this.GroupContextType = grpContextType;
            this.GroupName = grpName;
            this.GroupDescription = grpDesc;
        }*/
        public Group (PrincipalContext grpPrincipalContext, GroupPrincipal grp)
        {
            this.ctx = grpPrincipalContext;
            this.GroupItem = grp;

            if (grp.Context.Name != null && grp.Context.Name.Length > 0)
                this.GroupDomain = grp.Context.Name;
            else
                this.GroupDomain = "";

            if (grp.Name != null && grp.Name.Length > 0)
                this.GroupName = grp.Name;
            else
                this.GroupName = "";

            if (grp.Description != null && grp.Description.Length > 0)
                this.GroupDescription = grp.Description;
            else
                this.GroupDescription = "";

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
