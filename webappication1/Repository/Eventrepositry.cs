using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using webappication1.Models;

namespace webappication1.Repository
{
    public class Eventrepositry
    {

        private SqlConnection con;


        private   void Connection()
        {
            string consstring = ConfigurationManager.ConnectionStrings["getconnect"].ToString();

            con= new SqlConnection(consstring);
        }






    

    }



}
