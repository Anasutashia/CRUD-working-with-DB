using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Работа_с_БД
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VITAN\source\repos\Работа с БД\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Products]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "  " + Convert.ToString(sqlReader["Name"]) + "  " + Convert.ToString(sqlReader["Price"]));
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error); }

            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
    }
}
