using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MMUserCreation
{
    public partial class AddUserForm : Form
    {
        private static string serverName { get; set; }
        private static string dbName { get; set; }
        public AddUserForm(string server, string db)
        {
            InitializeComponent();
            serverName = server;
            dbName = db;
            GetDomains(server, db);
            GetFunctionalTiles(server, db);
        }

        private void GetFunctionalTiles(string server, string db)
        {
            List<string> list = new List<string>();
            string conString = "data source=" + server + ";initial catalog="+db+";integrated security=SSPI;persist security info=false;";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT ReportFunctionalType from ReportFunctionalTypes", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        list = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    }
                }
                functionalTilesCheckedListBox.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Functional Tiles Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetDomains(string server, string db)
        {
            List<string> list = new List<string>();
            string conString = "data source=" + server + ";initial catalog=" + db + ";integrated security=SSPI;persist security info=false;";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DomainName from Domain", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        list = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    }
                }
                domainsCheckedListBox.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Domains Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CreateUser(User user)
        {
            List<string> list = new List<string>();
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand getUserCmd = new SqlCommand("SELECT userId from AppUser where userId ='"+user.UserId+"'", con))
                    {
                        SqlDataReader dr = getUserCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        list = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
                        if (list.Count > 0)
                        {
                            MessageBox.Show("User Already Exists.");
                            return;
                        }
                        else
                        {
                            InsertUser(user);
                            CreatePrincipals(user);
                            MapUserToPrincipal(user);
                            MapFunctionalTiles(user);
                            AddGroups(user);
                            MapPrincipalGroup(user);
                            MessageBox.Show("User Created Successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Check Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MapPrincipalGroup(User user)
        {
            var mapPrinciaplGroupQuery = "INSERT INTO PrincipalGroupMap(PrincipalId, GroupID, IsEdit, IsDelete, IsDefault, CanPublishNotifications) SELECT PrincipalId, (SELECT TOP 1 GroupID from Groups WHERE GroupName = @UserId) as GroupID,'Y' AS IsEdit,'Y' AS IsDelete,'Y' AS IsDefault,'Y' AS CanPublishNotifications FROM Principals WHERE userid = @UserId";
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand mapPrinciaplGroupCmd = new SqlCommand(mapPrinciaplGroupQuery, con))
                    {
                        mapPrinciaplGroupCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        int x = mapPrinciaplGroupCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Map Principal Group Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddGroups(User user)
        {
            var addGroupQuery = "INSERT INTO Groups (GroupName,EntityId,GridSizeLimit,CanEmailResult,CanShareGlobally,MaxOfflineLimit,GroupType) VALUES(@UserId, 1, 5, 'Y', 'Y', 100, NULL)";
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand addGroupCmd = new SqlCommand(addGroupQuery, con))
                    {
                        addGroupCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        int x = addGroupCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Groups Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapFunctionalTiles(User user)
        {
            var mapFunctionalTilesQuery = "insert into UserFunctionalTypeMap select  @UserId as UserId,ReportFunctionalTypeId, @CanEditTileReports AS IsEdit from ReportFunctionalTypes where ReportFunctionalType in (@ReportFunctionalTypes)";
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand mapFunctionalTilesCmd = new SqlCommand(mapFunctionalTilesQuery, con))
                    {
                        mapFunctionalTilesCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        mapFunctionalTilesCmd.Parameters.Add("@CanEditTileReports", SqlDbType.VarChar).Value = user.CanEditTileReports;
                        mapFunctionalTilesCmd.AddArrayParameters("ReportFunctionalTypes", user.FunctionalTiles);
                        int x = mapFunctionalTilesCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Map Functional Tiles Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapUserToPrincipal(User user)
        {
            var userPrincipalMapQuery = "insert into UserPrincipalMap select UserId,PrincipalId, 'Y' as IsDefault from Principals where UserId = @UserId";
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand userPrincipalCmd = new SqlCommand(userPrincipalMapQuery, con))
                    {
                        userPrincipalCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        int x = userPrincipalCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Principal Map Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreatePrincipals(User user)
        {
            var principalQuery = "insert into Principals(UserId, EntityId, DomainId, ContextName, CanIgnoreContextFilters) select @UserId, 1, DomainID,'DistributorContext',@canIgnoreContextFilters from domain where DomainName in (@domainString)";
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand setPrincipalCmd = new SqlCommand(principalQuery, con))
                    {
                        setPrincipalCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        setPrincipalCmd.Parameters.Add("@canIgnoreContextFilters", SqlDbType.Bit).Value = user.CanIgnoreContextFilters ? 1 : 0;
                        setPrincipalCmd.AddArrayParameters("domainString", user.Domains);
                        int x = setPrincipalCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Principal Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void InsertUser(User user)
        {
            List<string> list = new List<string>();
            try
            {
                string conString = "data source=" + serverName + ";initial catalog=" + dbName + ";integrated security=SSPI;persist security info=false;";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand getUserCmd = new SqlCommand("insert into AppUser([UserID],[LastName],[FirstName],[usrPassword],[PswExpDate],[LastLoginDate],[AccountStatus],[Email],[CanPublish],[ActiveFlag],[PreferredLanguage],[UserFriendlyName],[PreferredTheme]) values(@UserId,@LastName,@FirstName,'LzS+9A3TNSaZwNQanUUw+Q==',DATEADD(YEAR,2,GETDATE()),CURRENT_TIMESTAMP,'E',@Email,@CanPublish,1,1,@UserFriendlyName,'dark')", con))
                    {
                        getUserCmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = user.UserId;
                        getUserCmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = user.LastName;
                        getUserCmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = user.FirstName;
                        getUserCmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.Email;
                        getUserCmd.Parameters.Add("@CanPublish", SqlDbType.Bit).Value = user.CanPublish;
                        getUserCmd.Parameters.Add("@UserFriendlyName", SqlDbType.VarChar).Value = user.UserFriendlyName;
                        int x = getUserCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "User Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            if (isValidateSelections())
            {
                var user = new User();
                user.UserId = userIdTextBox.Text;
                user.FirstName = firstNameTextBox.Text;
                user.LastName = lastNameTextBox.Text;
                user.Email = emailTextBox.Text;
                user.UserFriendlyName = userFriendlyNameTextBox.Text;
                user.CanEditTileReports = canEditTileReportsCheckBox.Checked ? "Y" : "N";
                user.CanIgnoreContextFilters = canIgnoreContextFiltersCheckBox.Checked;
                user.CanNotify = canNotifyCheckBox.Checked;
                user.CanPublish = canPublishCheckBox.Checked;
                user.Domains = domainsCheckedListBox.CheckedItems.Cast<object>().Select(x => domainsCheckedListBox.GetItemText(x)).ToList();
                user.FunctionalTiles = functionalTilesCheckedListBox.CheckedItems.Cast<object>().Select(x => functionalTilesCheckedListBox.GetItemText(x)).ToList();
                CreateUser(user);
            }
            else
            {
                MessageBox.Show("Kindly check whether all fields are entered.");
            }
        }

        private bool isValidateSelections()
        {
            bool isValidSelections = true;
            if (userIdTextBox.Text == "" || firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || emailTextBox.Text == "" || userFriendlyNameTextBox.Text == "")
            {
                isValidSelections = false;
            }
            if(domainsCheckedListBox.SelectedItems.Count == 0 || functionalTilesCheckedListBox.SelectedItems.Count == 0)
            {
                isValidSelections = false;
            }
            return isValidSelections;
        }
    }

    public static class Extensions
    {
        public static void AddArrayParameters<Ti>(this SqlCommand cmd, string name, IEnumerable<Ti> values)
        {
            name = name.StartsWith("@") ? name : "@" + name;
            var names = string.Join(", ", values.Select((value, i) => {
                var paramName = name + i;
                cmd.Parameters.AddWithValue(paramName, value);
                return paramName;
            }));
            cmd.CommandText = cmd.CommandText.Replace(name, names);
        }
    }
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserFriendlyName { get; set; }
        public bool CanPublish { get; set; }
        public bool CanIgnoreContextFilters{ get; set; }
        public bool CanNotify { get; set; }
        public string CanEditTileReports { get; set; }
        public List<string> Domains { get; set; }
        public List<string> FunctionalTiles { get; set; }
    }
}
