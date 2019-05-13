using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace COMPE561_Lab08
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
            const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand comm = new MySqlCommand("SELECT CustId, FirstName, LastName, Email, Phone FROM customer", conn);

            try
            {
                //open the connection object and produce a reader object using the command object
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();

                //bind data to the ASP repeater
                customersRepeater.DataSource = reader;
                customersRepeater.DataBind();

                //close reader
                reader.Close();
            }


            finally
            {
                //close connection regardless of outcome
                conn.Close();
            }
        }
    }
}