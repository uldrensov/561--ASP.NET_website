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
using System.Collections.Generic;

namespace COMPE561_Lab08
{
    public partial class Orders : System.Web.UI.Page
    {
        //
        List<Order> DATA = new List<Order>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //only execute this method on initial load
            {
                //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
                const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
                MySqlConnection conn = new MySqlConnection(connectionString);
                MySqlCommand comm1 = new MySqlCommand("SELECT CustId, CONCAT(FirstName, ' ', LastName) AS FullName FROM customer", conn);

                //populate customer dropdown
                try
                {
                    //open the connection object and produce a reader object using the command object
                    conn.Open();
                    MySqlDataReader reader1 = comm1.ExecuteReader();

                    //bind data to the ASP dropdown
                    custList.DataSource = reader1;
                    custList.DataValueField = "CustId";
                    custList.DataTextField = "FullName";
                    custList.DataBind();

                    //close reader
                    reader1.Close();
                }

                finally
                {
                    //close connection regardless of outcome
                    conn.Close();
                }

                //populate book dropdown
                MySqlCommand comm2 = new MySqlCommand("SELECT Title, Isbn FROM title", conn);
                try
                {
                    //open the connection object and produce a reader object using the command object
                    conn.Open();
                    MySqlDataReader reader2 = comm2.ExecuteReader();

                    //bind data to the ASP dropdown
                    bookList.DataSource = reader2;
                    bookList.DataValueField = "Isbn";
                    bookList.DataTextField = "Title";
                    bookList.DataBind();

                    //close reader
                    reader2.Close();
                }

                finally
                {
                    //close connection regardless of outcome
                    conn.Close();
                }
            }
        }


        protected void addButton_Click(object sender, EventArgs e)
        {
            //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
            const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand comm = new MySqlCommand($"SELECT Title, Isbn, SellingPrice FROM title WHERE Isbn = '{bookList.SelectedValue}'", conn);

            try
            {
                //open the connection object and produce a reader object using the command object
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();

                foreach (RepeaterItem item in Cart.Items)
                {
                    DATA.Add(new Order(item.FindControl(titleLabel) as Label.Text,
                        item.FindControl(isbnLabel) as Label).Text,
                    "","","");
                }

                
                //
                while (reader.Read())
                {
                    //DATA.Add(new Order(reader.GetString(0), reader.GetString(1), reader.GetString(2), qTextBox.Text,
                    //(reader.GetDouble(2) * Convert.ToDouble(qTextBox)).ToString("F")));
                    DATA.Add(new Order("samp","samp2","","",""));
                }



                //bind data to the ASP repeater
                Cart.DataSource = DATA;
                Cart.DataBind();

                //close reader
                reader.Close();
            }

            finally
            {
                //close connection regardless of outcome
                conn.Close();
            }
        }


        protected void resetButton_Click(object sender, EventArgs e)
        {

        }


        protected void submitButton_Click(object sender, EventArgs e)
        {

        }
    }
}