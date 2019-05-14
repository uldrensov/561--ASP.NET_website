using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMPE561_Lab08
{
    public class Order
    {
        //fields
        public string title { get; set; }
        public string isbn { get; set; }
        public string price { get; set; }
        public string qty { get; set; }
        public string linetotal { get; set; }


        //default constr
        public Order()
        {
            title = "-NULL-";
            isbn = "-NULL-";
            price = "-NULL-";
            qty = "-NULL-";
            linetotal = "-NULL-";
        }


        //exp val constr
        public Order(string TITLE, string ISBN, string PRICE, string QTY, string LINETOTAL)
        {
            title = TITLE;
            isbn = ISBN;
            price = PRICE;
            qty = QTY;
            linetotal = LINETOTAL;
        }
    }
}