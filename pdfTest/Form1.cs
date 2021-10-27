using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdfTest
{
    public partial class Form1 : Form
    {
        BindingList<Group> reportGroups = new BindingList<Group>();
        BindingList<Group> searchGroups = new BindingList<Group>();
        public static string reportName;
        private string adUsrName = null;
        private string adUsrPass = null;
        private List<string> listAdObjectAttributes;


        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

        private string GetCurrentNamingContext()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

            return de.Properties["defaultNamingContext"][0].ToString();
        }

        public Form1()
        {
            InitializeComponent();

            // In your constructor or somewhere more suitable:
            Program.SendMessage(textBoxSearch.Handle, 0x1501, 1, "Unesite ime grupe.");


            listBox1.DataSource = searchGroups;
            listBox1.DisplayMember = "GroupName";


            listBox2.DataSource = reportGroups;
            listBox2.DisplayMember = "GroupName";

            try
            {
                comboBoxDomains.Items.Add(Domain.GetComputerDomain());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, ex.Source);
                Debug.WriteLine(ex.Message);
            }


            comboBoxDomains.SelectedIndex = comboBoxDomains.Items.Count - 1;

            //var forest = Forest.GetCurrentForest();
            //comboBoxDomains.Items.Add(forest.Domains);

            //Debug.WriteLine(GetCurrentDomainPath());

            //DirectorySearcher ds = null;
            //DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);

            //using (DirectoryEntry computerEntry = new DirectoryEntry("WinNT://" + Environment.MachineName))
            //{
            //    IEnumerable<string> userNames = computerEntry.Children
            //        .Cast<DirectoryEntry>()
            //        .Where(childEntry => childEntry.SchemaClassName == "Group")
            //        .Select(userEntry => userEntry.Name);

            //    foreach (string name in userNames)
            //        Console.WriteLine(name);
            //}

            ////ds = new DirectorySearcher(localMachine);
            ////ds.Filter = "(&(objectCategory=User)(objectClass=person))";

            ////SearchResultCollection results;
            ////results = ds.FindAll();

            ////foreach (SearchResult sr in results)
            ////{
            ////    // Using the index zero (0) is required!
            ////    Debug.WriteLine(sr.Properties["name"][0].ToString());
            ////}

        }

        private void button1Search_Click(object sender, EventArgs e)
        {
            try
            {
                PrincipalContext ctx;
                DirectoryEntry entry;

                if ("Local Machine" == comboBoxDomains.Text)
                {
                    ctx = new PrincipalContext(ContextType.Machine, Environment.MachineName);
                    using (entry = new DirectoryEntry("WinNT://" + Environment.MachineName))
                    {
                        IEnumerable<string> groupNames = entry.Children
                            .Cast<DirectoryEntry>()
                            .Where(childEntry => childEntry.SchemaClassName == "Group" && childEntry.Name.ToLower().Contains(textBoxSearch.Text.ToLower()))
                            .Select(userEntry => userEntry.Name);

                        searchGroups.Clear();
                        if (groupNames != null && groupNames.Count() > 0)
                        {
                            foreach (string grp in groupNames)
                            {
                                GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, grp);
                                searchGroups.Add(new Group(ctx, group));
                                //searchGroups.Add(new Group(new PrincipalContext(ContextType.Machine), Environment.MachineName, grp, "grp desc_loc"));
                            }
                        }

                    }
                }
                else
                {
                    while (true)
                    {
                        try
                        {
                            if (adUsrName != null && adUsrName.Length > 0)
                            {
                                ctx = new PrincipalContext(ContextType.Domain, comboBoxDomains.Text, adUsrName, adUsrPass);
                                entry = new DirectoryEntry("LDAP://" + ctx.Name, adUsrName, adUsrPass);
                            }
                            else
                            {
                                ctx = new PrincipalContext(ContextType.Domain, comboBoxDomains.Text);
                                entry = new DirectoryEntry("LDAP://" + ctx.Name);
                            }

                            using (entry)
                            {
                                DirectorySearcher searcher = new DirectorySearcher(entry);

                                //AD group search string
                                string gName = "*";
                                if (textBoxSearch.Text != null && textBoxSearch.Text.Length > 0)
                                    gName += textBoxSearch.Text + "*";

                                //AD group search filter set-up
                                searcher.Filter = "(&(ObjectClass=group)(name=" + gName + "))";
                                searcher.PropertiesToLoad.Add("name");
                                SearchResultCollection results = searcher.FindAll();

                                //clear previus searchList and iterate over current search results
                                searchGroups.Clear();
                                foreach (SearchResult grp in results)
                                {
                                    //from returned query, find group principal object and add it to list
                                    GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, grp.Properties["name"][0].ToString());
                                    searchGroups.Add(new Group(ctx, group));
                                    //Debug.WriteLine(res.Properties["name"][0].ToString());
                                }
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "The user name or password is incorrect.\r\n")
                            {
                                Form3 fmCred = new Form3();
                                DialogResult dr = fmCred.ShowDialog(this);
                                if (dr == DialogResult.Cancel)
                                {
                                    return;
                                }
                                else if (dr == DialogResult.OK)
                                {
                                    adUsrName = fmCred.GetUsername();
                                    adUsrPass = fmCred.GetPassword();
                                    fmCred.Close();

                                }
                            }
                            else
                            {
                                return;
                            }
                        }

                        //  old
                        /////////////////////
                        /*try
                        {
                            srv = ctx.ConnectedServer;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "The user name or password is incorrect.\r\n")
                            {
                                Form3 fmCred = new Form3();
                                DialogResult dr = fmCred.ShowDialog(this);
                                if (dr == DialogResult.Cancel)
                                {
                                    return;
                                }
                                else if (dr == DialogResult.OK)
                                {
                                    usrName = fmCred.GetUsername();
                                    usrPass = fmCred.GetPassword();
                                    fmCred.Close();
                                    ctx = new PrincipalContext(ContextType.Domain, comboBoxDomains.Text, usrName, usrPass);
                                }
                            }
                            else
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }

                        }

                        using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + ctx.Name, usrPass, usrPass))
                        {
                            DirectorySearcher searcher = new DirectorySearcher(entry);

                            //AD group search string
                            string gName = "*";
                            if (textBoxSearch.Text != null && textBoxSearch.Text.Length > 0)
                                gName += textBoxSearch.Text + "*";

                            //AD group search filter set-up
                            searcher.Filter = "(&(ObjectClass=group)(name=" + gName + "))";
                            searcher.PropertiesToLoad.Add("name");
                            SearchResultCollection results = searcher.FindAll();

                            //clear previus searchList and iterate over current search results
                            searchGroups.Clear();
                            foreach (SearchResult grp in results)
                            {
                                //from returned query, find group principal object and add it to list
                                GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, grp.Properties["name"][0].ToString());
                                searchGroups.Add(new Group(ctx, group));
                                //Debug.WriteLine(res.Properties["name"][0].ToString());
                            }
                        }*/

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            reportGroups.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (int indx in listBox1.SelectedIndices)
            {
                if (reportGroups.Contains(searchGroups[indx]) == false) reportGroups.Add(searchGroups[indx]);
            }

            listBox1.SelectedItem = null;

        }

        private void buttonRemoveItem_Click(object sender, EventArgs e)
        {
            reportGroups.Remove(reportGroups.SingleOrDefault (x => x.GroupName == listBox2.SelectedItem.ToString()));
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            Form2 fmRprtName = new Form2();
            DialogResult dr = fmRprtName.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else if (dr == DialogResult.OK)
            {
                reportName = fmRprtName.getText();
                fmRprtName.Close();
            }

            //using (DirectoryEntry computerEntry = new DirectoryEntry("WinNT://" + Environment.MachineName))
            //{
            //    DirectoryEntry admGroup = computerEntry.Children
            //        .Find("homeusers", "Group");

            //    object members = admGroup.Invoke("members", null);

            //    foreach (object groupMember in (IEnumerable)members)
            //    {
            //        DirectoryEntry member = new DirectoryEntry(groupMember);
            //        Console.WriteLine(member.Name);
            //    }
            //    //listBox1.DisplayMember = 
            //}


            /*
            foreach (Group grp in reportGroups)
            {
                    // iterate over members
                    foreach (Principal p in grp.GroupItem.GetMembers())
                    {
                        // add users to groups
                        if (grp.groupUsers.Contains(p) == false) grp.groupUsers.Add(p);

                    }
             
            }
            */

            ReportGenerator.NewReport(reportName);

            foreach (Group grp in reportGroups)
            {
                ReportGenerator.CreateNewTable(grp.GroupDomain, grp.GroupName, grp.GroupDescription);
                //Debug.WriteLine(grp.GroupName);

                foreach (Principal p in grp.GroupItem.GetMembers().OrderBy(usr => usr.DisplayName))
                {
                    UserPrincipal usr = p as UserPrincipal;
                    if (usr == null)
                    {
                        //principal je grupa
                        //potencijalni kod...
                        continue;
                    }
                    else
                    {
                        //principal is user
                        ReportGenerator.AddMember(grp.GroupDomain, grp.GroupName,
                            usr.DisplayName, usr.SamAccountName, usr.EmailAddress,
                            usr.GetDepartment(),
                            usr.Enabled == true ? "Aktiviran" : "Deaktiviran") ;
                        //Debug.WriteLine("{0} {1} {2} {3}", usr.DisplayName, usr.SamAccountName, usr.EmailAddress, usr.Enabled);
                    }

                }
            }

            try{
                PrincipalContext ctx;
                using (ctx = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal currUser = UserPrincipal.Current;
                    ReportGenerator.GenerateReport(currUser.DisplayName + "(" + currUser.UserPrincipalName + ")");   
                }
            }catch(Exception ex)
            {
                ReportGenerator.GenerateReport();
                Debug.WriteLine(ex.Message);
            }
            
            
            
        }

        private void podesiAtributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fmAtr = new Form4();
            DialogResult dr = fmAtr.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else if (dr == DialogResult.OK)
            {
                listAdObjectAttributes = fmAtr.getAttributes();
                fmAtr.Close();
            }
        }

        private void izveziKaoCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Group grp in reportGroups)
            {
                //Debug.WriteLine(grp.GroupName);

                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + comboBoxDomains.Text, "mnestic", "mn2021."))
                {
                    DirectorySearcher searcher = new DirectorySearcher(entry);

                    //AD group search filter set-up
                    searcher.Filter = "(&(memberOf:1.2.840.113556.1.4.1941:=CN=" + grp.GroupName +",OU=GRP,OU=ZG,OU=EU,DC=dummy,DC=org))";
                    //"(&(ObjectClass=group)(name=" + gName + "))";
                    foreach (string item in listAdObjectAttributes)
                    {
                        searcher.PropertiesToLoad.Add(item);
                    }
                    SearchResultCollection results = searcher.FindAll();

                    StringBuilder strbuild = new StringBuilder();
                    foreach (string item in listAdObjectAttributes)
                    {
                        strbuild.Append(item + ";");
                    }
                    strbuild.Remove(strbuild.Length - 1, 1);
                    strbuild.AppendLine();
                    foreach (SearchResult usr in results)
                    {
                        //from returned query, find group principal object and add it to list
                        foreach (string item in listAdObjectAttributes)
                        {
                            if (usr.Properties.Contains(item) == true)
                                strbuild.Append(usr.Properties[item][0].ToString());
                            strbuild.Append(";");

                        }
                        strbuild.Remove(strbuild.Length - 1, 1);
                        strbuild.AppendLine();
                    }
                    strbuild.Remove(strbuild.Length - 2, 2);
                    #if DEBUG
                        Debug.WriteLine(strbuild);
                    #endif
                    System.IO.File.WriteAllText(@grp.GroupName + ".csv", strbuild.ToString());
                }

            }

        }
    }
}

