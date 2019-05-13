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
    public partial class BooksByAuthor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //only execute this method on initial load
            {
                //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
                const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand comm = new MySqlCommand("SELECT AuthorId, CONCAT(FirstName, ' ', LastName) AS FullName FROM author", conn);

                try
                {
                    //open the connection object and produce a reader object using the command object
                    conn.Open();
                    MySqlDataReader reader = comm.ExecuteReader();

                    //bind data to the ASP dropdown
                    authorList.DataSource = reader;
                    authorList.DataValueField = "AuthorId";
                    authorList.DataTextField = "FullName";
                    authorList.DataBind();

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


        protected void submitButton_Click(object sender, EventArgs e)
        {
            //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
            const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand comm = new MySqlCommand("SELECT AuthorId, Title, FirstName, LastName, Type, Isbn " +
                $"FROM author INNER JOIN wrote USING (AuthorId) INNER JOIN title USING (Isbn) WHERE AuthorId = {authorList.SelectedValue}", conn);

            try
            {
                //open the connection object and produce a reader object using the command object
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();

                //bind data to the ASP repeater
                authorRepeater.DataSource = reader;
                authorRepeater.DataBind();

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