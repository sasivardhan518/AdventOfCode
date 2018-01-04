using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace MMUserCreation
{
    public partial class Form1 : Form
    {
        private bool start { get; set; }
        public Form1()
        {
            InitializeComponent();
            GetServers();
        }

        private void GetServers()
        {
            start = true;
            DataTable dt = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources();
            List<string> serverList = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
            serversDropdown.DataSource = serverList;
        }

        public List<string> GetDatabaseList(string server)
        {
            List<string> list = new List<string>();
            string conString = "data source="+server+";initial catalog=master;integrated security=SSPI;persist security info=false;";
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * from sys.databases", con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        list = dt.AsEnumerable().Select(x => x[0].ToString()).ToList();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Some title", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            databaseDropdown.DataSource = list;
            return list;
            
        }

        private void serversDropdown_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!start)
            {
                databaseDropdown.DataSource = new List<string>();
                List<string> databaseList = GetDatabaseList(serversDropdown.SelectedValue.ToString());
            }
            else
            {
                start = false;
            }
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            
            AddUserForm addUserForm = new AddUserForm(serversDropdown.SelectedValue.ToString(), databaseDropdown.SelectedValue.ToString());
            addUserForm.Show();
        }
    }
}