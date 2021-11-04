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
#if DEBUG
                Debug.WriteLine("Get Computer Domain: " + ex.Message);
#else
                MessageBox.Show("Get Computer Domain: " + ex.Message);
#endif

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
            string searchDomain = comboBoxDomains.Text;
            string searchGrp = textBoxSearch.Text;
            if (searchGrp != null && searchGrp.Length > 0)
                searchGrp = "*" + searchGrp + "*";
            else
                searchGrp = "*";

            try
            {
                DirectoryEntry entry;

                if ("Local Machine" == comboBoxDomains.Text)
                {
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
                                GroupPrincipal localGroup = GroupPrincipal.FindByIdentity(new PrincipalContext(ContextType.Machine), grp);
                                searchGroups.Add(new Group(Environment.MachineName, localGroup.Name, localGroup.DistinguishedName, localGroup.Description, entry.Path, "", ""));
                                localGroup.Dispose();
#if DEBUG
                                Debug.WriteLine("SearchDomain: Domain: {0}, " +
                                        "name: {1}, " +
                                        "DN: {2}, " +
                                        "description {3}, " +
                                        "entryPath {4}, " +
                                        "entryCredentialsUsed: {5}", searchGroups.Last().GroupDomain,
                                                                     searchGroups.Last().GroupName,
                                                                     searchGroups.Last().GroupDN,
                                                                     searchGroups.Last().GroupDescription,
                                                                     searchGroups.Last().EntryPath,
                                                                     searchGroups.Last().EntryUseCredentials.ToString());
#endif
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
                            entry = new DirectoryEntry("LDAP://" + searchDomain);
                            using (entry)
                            {
                                DirectorySearcher searcher = new DirectorySearcher(entry);
                                
                                //AD group search filter set-up
                                searcher.Filter = "(&(ObjectClass=group)(name=" + searchGrp + "))";
                                searcher.PropertiesToLoad.Add("name");
                                searcher.PropertiesToLoad.Add("distinguishedName");
                                searcher.PropertiesToLoad.Add("description");
                                SearchResultCollection results = searcher.FindAll();

                                //clear previus searchList and iterate over current search results
                                searchGroups.Clear();
                                foreach (SearchResult grp in results)
                                {
                                    //from returned query, find group principal object and add it to list
                                    string _name, _dn, _desc;
                                    _name = _dn = _desc = "";
                                    if (grp.Properties.Contains("name") == true)    _name = grp.Properties["name"][0].ToString();
                                    if (grp.Properties.Contains("distinguishedName") == true) _dn = grp.Properties["distinguishedName"][0].ToString();
                                    if (grp.Properties.Contains("description") == true) _desc = grp.Properties["description"][0].ToString();
                                    
                                        searchGroups.Add(new Group(searchDomain,
                                                                    _name,
                                                                    _dn,
                                                                    _desc,
                                                                    entry.Path,
                                                                    "",
                                                                    ""));
                                    
#if DEBUG
                                    Debug.WriteLine("SearchDomain: Domain: {0}, " +
                                        "name: {1}, " +
                                        "DN: {2}, " +
                                        "description {3}, " +
                                        "entryPath {4}, " +
                                        "entryCredentialsUsed: {5}", searchGroups.Last().GroupDomain,
                                                                     searchGroups.Last().GroupName,
                                                                     searchGroups.Last().GroupDN,
                                                                     searchGroups.Last().GroupDescription,
                                                                     searchGroups.Last().EntryPath,
                                                                     searchGroups.Last().EntryUseCredentials.ToString());
#endif
                                }
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            //if (ex.Message == "The user name or password is incorrect.\r\n")
                            //{
                            //    Form3 fmCred = new Form3();
                            //    DialogResult dr = fmCred.ShowDialog(this);
                            //    if (dr == DialogResult.Cancel)
                            //    {
                            //        return;
                            //    }
                            //    else if (dr == DialogResult.OK)
                            //    {
                            //        entryUsrName = fmCred.GetUsername();
                            //        entryUsrPass = fmCred.GetPassword();
                            //        fmCred.Close();

                            //    }
                            //}
                            //else
                            {
#if DEBUG
                                Debug.WriteLine("DomainSearch: " + ex.Message);
#else
                                MessageBox.Show(ex.Message);
#endif
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
#if DEBUG
                Debug.WriteLine("Search error: " + ex.Message);
#else
                MessageBox.Show("Search error: " + ex.Message, ex.Source);
#endif
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
            reportGroups.Remove(reportGroups.SingleOrDefault(x => x.GroupName == listBox2.SelectedItem.ToString()));
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
#if DEBUG
            Debug.WriteLine("buttonPrintClick: frm.closed(), reportName: " + reportName);
#endif

            ReportGenerator.NewReport(reportName);

            foreach (Group grp in reportGroups)
            {
                ReportGenerator.CreateNewTable(grp.GroupDomain, grp.GroupName, grp.GroupDescription);
                DirectoryEntry entry = new DirectoryEntry(grp.EntryPath);
                using (entry)
                {
                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    //searcher.Filter = "(&(memberOf:1.2.840.113556.1.4.1941:=" + grp.GroupDN + "))";
                    searcher.Filter = "(&(memberOf=" + grp.GroupDN + "))";
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("sAMAccountName");
                    searcher.PropertiesToLoad.Add("mail");
                    searcher.PropertiesToLoad.Add("department");
                    searcher.PropertiesToLoad.Add("enabled");
                    SearchResultCollection results = searcher.FindAll();

                    List<ReportUser> rptUsers = new List<ReportUser>();
                    ReportUser rptUser;
                    foreach (SearchResult usr in results)
                    {
                        rptUser = new ReportUser();
                        if (usr.Properties.Contains("displayName") == true)
                            rptUser.DisplayName = usr.Properties["displayName"][0].ToString();
                        if (usr.Properties.Contains("sAMAccountName") == true)
                            rptUser.SAMAccountName = usr.Properties["sAMAccountName"][0].ToString();
                        if (usr.Properties.Contains("mail") == true)
                            rptUser.Mail = usr.Properties["mail"][0].ToString();
                        if (usr.Properties.Contains("enabled") == true)
                            rptUser.Enabled = (bool)usr.Properties["enabled"][0];
                        rptUsers.Add(rptUser);
                    }
                    foreach (ReportUser usr in rptUsers.OrderBy(x => x.DisplayName))
                    {
                        ReportGenerator.AddMember(grp.GroupDomain, grp.GroupName,
                        usr.DisplayName, usr.SAMAccountName, usr.Mail,
                        usr.Department,
                        usr.Enabled == true ? "Aktiviran" : "Deaktiviran");

#if DEBUG
                    Debug.WriteLine("buttonPrintClick Display Name: {0}, " +
                        "SAMAccName: {1}, " +
                        "Email Address: {2}, " +
                        "Department: {3}" +
                        "Enabled: {4}",
                        usr.DisplayName, usr.SAMAccountName, usr.Mail, usr.Department, usr.Enabled);
#endif
                    }
                }

            }

            try
            {
                PrincipalContext ctx;
                using (ctx = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal currUser = UserPrincipal.Current;
                    ReportGenerator.GenerateReport(currUser.DisplayName + "(" + currUser.UserPrincipalName + ")");
                }
            }
            catch (Exception ex)
            {
                ReportGenerator.GenerateReport();

#if DEBUG
                Debug.WriteLine(ex.Message);
#else
                MessageBox.Show("Generate report: " + ex.Message, ex.Source);
#endif
            }
        }

        private void podesiAtributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 fmAtr = new Form4();
            fmAtr.ShowDialog(this);
            fmAtr.Dispose();
        }

        private void izveziKaoCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get attrbutes from adAttributes File
            List<string> listAdObjectAttributes = new AdAttributeFileRW(Properties.Resources.adAttributesPath).
                                                        AdAttributes.Where(x => x.Enabled == true).
                                                        Select(x => x.Name).
                                                        ToList();
            foreach (Group grp in reportGroups)
            {
                DirectoryEntry entry;
                if (grp.EntryUseCredentials == true)
                {
                    entry = new DirectoryEntry(grp.EntryPath, grp.EntryUsername, grp.EntryPassword);
                }
                else
                {
                    entry = new DirectoryEntry(grp.EntryPath);
                }
                using (entry)
                {
                    DirectorySearcher searcher = new DirectorySearcher(entry);
                    searcher.Filter = "(&(memberOf:1.2.840.113556.1.4.1941:=" + grp.GroupDN + "))";
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
                    strbuild.Remove(strbuild.Length - 1, 1);                //ukloni delimiter viska s kraja retka
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
                        strbuild.Remove(strbuild.Length - 1, 1);            //ukloni delimiter viska s kraja retka
                        strbuild.AppendLine();
                    }
                    strbuild.Remove(strbuild.Length - 2, 2);                //ukloni zadnji /cr/lf
#if DEBUG
                    Debug.WriteLine("IzvezikaoCSV:" + strbuild);
#endif
                    System.IO.File.WriteAllText(@grp.GroupName + ".csv", strbuild.ToString());
                }

            }

        }
    }
}

