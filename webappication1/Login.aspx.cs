using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webappication1
{
    public partial class Login : System.Web.UI.Page
    {

        private SqlConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void Connection()
        {
            string consstring = ConfigurationManager.ConnectionStrings["getconnect"].ToString();

            con = new SqlConnection(consstring);
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Connection();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

          

                SqlCommand cmd = new SqlCommand("spCheckUserLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlParameter isValidParam = new SqlParameter("@IsValid", System.Data.SqlDbType.Bit)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(isValidParam);

                con.Open();
                cmd.ExecuteNonQuery();

                bool isValid = Convert.ToBoolean(isValidParam.Value);

                if (isValid)
                {
                    lblMessage.Text = "Login successful!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    // Redirect to home page
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            
        }






    }
}