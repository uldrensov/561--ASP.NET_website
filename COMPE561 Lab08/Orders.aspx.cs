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

//NOTE: It is assumed that there is no need to validate whether or not an order exceeds the current stock, since orders can be delayed until
//the bookstore can stock enough merchandise to meet the demand of the customer(s)
namespace COMPE561_Lab08
{
    public partial class Orders : System.Web.UI.Page
    {
        //instantiate a list of objects to act as a data source for the "shopping cart" gridview
        List<Order> DATALIST = new List<Order>();

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
            //reset error label, if any
            ErrorMessage.Visible = false;

            //create connection string and tie it to a connection object
            const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                //pass the connection object with a SQL directive to a command object
                MySqlCommand comm = new MySqlCommand($"SELECT Title, Isbn, SellingPrice, '{qTextBox.Text}' AS Qty, " +
                    $"SellingPrice * {Convert.ToInt32(qTextBox.Text)} AS LineTotal " +
                    $"FROM title WHERE Isbn = '{bookList.SelectedValue}'", conn);

                //open the connection object and produce a reader object using the command object
                conn.Open();
                MySqlDataReader reader = comm.ExecuteReader();

                //rebuild the list using the current items in the gridview...
                foreach (GridViewRow row in cartGrid.Rows)
                {
                    DATALIST.Add(new Order(row.Cells[0].Text, row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text, row.Cells[4].Text));
                }

                //...then add the currently selected book + quantity as a new list entry
                while (reader.Read())
                {
                    DATALIST.Add(new Order(reader.GetString(0), reader.GetString(1), reader.GetString(2), qTextBox.Text, 
                        (reader.GetDouble(2) * Convert.ToDouble(qTextBox.Text)).ToString("F")));
                }

                //(re)bind data to the ASP gridview
                cartGrid.DataSource = DATALIST;
                cartGrid.DataBind();

                //close reader
                reader.Close();
            }

            catch (Exception err)
            {
                //show an error message if necessary
                ErrorMessage.Text = $"ERROR: {err.Message}";
                ErrorMessage.Visible = true;
            }

            finally
            {
                //close connection regardless of outcome
                conn.Close();
            }

            //clear the quantity input
            qTextBox.Text = null;
        }


        protected void resetButton_Click(object sender, EventArgs e)
        {
            //reset error label, if any
            ErrorMessage.Visible = false;

            //clear the list and quantity input, then rebind data
            DATALIST.Clear();
            qTextBox.Text = null;
            cartGrid.DataSource = DATALIST;
            cartGrid.DataBind();
        }


        protected void submitButton_Click(object sender, EventArgs e)
        {
            //reset error label, if any
            ErrorMessage.Visible = false;

            //create/init fields to hold certain customer/order details
            int custid = 0; int orderid = 0;
            string cardtype = ""; string cardnum = ""; string cardexp = "";

            //get today's date
            DateTime today = new DateTime();
            today = DateTime.Now;

            //create connection string, tie it to a connection object, then pass this object with a SQL directive to a command object
            const string connectionString = "Data Source=localhost;Initial Catalog=final8;User ID=root;Password=";
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand comm1 = new MySqlCommand($"SELECT CustId, CardType, CardNumber, CardExpire FROM customer WHERE CustId = {custList.SelectedValue}", conn);

            //before executing SQL commands, ensure the shopping cart is not empty
            try
            {
                if (cartGrid.Rows[0] != null)
                {
                    //if a row exists, do nothing and proceed beyond this try-catch block
                    //otherwise, attempting to check a row that doesn't exist will throw an error
                }
            }

            catch
            {
                ErrorMessage.Text = "ERROR: Please add at least 1 item to cart before purchasing.";
                ErrorMessage.Visible = true;
            }

            //1) get the customer's information
            //2) write to the "orders" table and obtain the auto-generated order id
            //3) write to the "orderdetail" table
            try
            {
                //1)
                //open the connection object and produce a reader object using the command object
                conn.Open();
                MySqlDataReader reader1 = comm1.ExecuteReader();

                //hold onto this information for subsequent operations
                while (reader1.Read())
                {
                    custid = reader1.GetInt32(0);
                    cardtype = reader1.GetString(1);
                    cardnum = reader1.GetString(2);
                    cardexp = reader1.GetString(3);
                }

                //close the reader
                reader1.Close();

                //2)
                //create the write command using the stored information, then execute
                MySqlCommand comm2 = new MySqlCommand("INSERT INTO orders(OrderDate, CustId, CardType, CardNumber, CardExpire) VALUES" +
                $"('{today.Date.ToString()}', {custid}, '{cardtype}', '{cardnum}', '{cardexp}')", conn);
                comm2.ExecuteNonQuery();

                //create the read command + reader object to obtain and store the newly generated order id
                MySqlCommand comm3 = new MySqlCommand("SELECT MAX(OrderId) FROM orders", conn);
                MySqlDataReader reader2 = comm3.ExecuteReader();
                while (reader2.Read())
                {
                    orderid = reader2.GetInt32(0);
                }

                //close the reader
                reader2.Close();

                //3)
                //execute a command for each row in the gridview
                foreach (GridViewRow row in cartGrid.Rows)
                {
                    MySqlCommand comm4 = new MySqlCommand("INSERT INTO orderdetail(OrderId, Isbn, Qty, Price) VALUES " +
                        $"({orderid}, '{row.Cells[1].Text}', {row.Cells[3].Text}, {row.Cells[2].Text})", conn);
                    comm4.ExecuteNonQuery();
                }
            }

            catch (Exception err)
            {
                //ordering the copies of same title in multiple rows will violate the primary key of the "orderdetail" table
                //in that case, only the first row will be written to the database
                ErrorMessage.Text = $"ERROR: {err.Message}";
                ErrorMessage.Visible = true;
            }

            finally
            {
                //close connection regardless of outcome
                conn.Close();
            }

            //clear the list and quantity input, then rebind data
            DATALIST.Clear();
            qTextBox.Text = null;
            cartGrid.DataSource = DATALIST;
            cartGrid.DataBind();
        }
    }
}