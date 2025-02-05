using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using webappication1.Models;
using webappication1.Repository;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
namespace webappication1.Controllers
{
    public class eventController : Controller
    {

        private SqlConnection con;
        public string passgene;
        public ActionResult Login()
        {



            return View();




        }

        [HttpPost]

        public ActionResult Login(Regmodel reg)
        {
            if (reg.email == "admin" && reg.password == "admin")
            {
                // Set the session for the admin user
                Session["UserEmail"] = reg.email;

                // Redirect to the admin page
                return RedirectToAction("admin", "Event");
            }
            else
            {
                string consstring = ConfigurationManager.ConnectionStrings["getconnect"].ToString();
                con = new SqlConnection(consstring);

                SqlCommand cmd = new SqlCommand("spCheckUserLogin", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", reg.email);
                cmd.Parameters.AddWithValue("@Password", reg.password);

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
                    // Set the session for the logged-in user
                    Session["UserEmail"] = reg.email;

                    // Redirect to the home page (or a different page)
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Show an alert for invalid login
                    Response.Write("<script>alert('Invalid credentials. Please try again.')</script>");
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            // Clear session data
            Session.Clear();

            // Redirect to Login action in the Event controller
            return RedirectToAction("Login", "Event");
        }




        public ActionResult Signup()
        {
            // Clear session data  Session.Clear();

            // Redirect to Login action in the Event controller
            return RedirectToAction("registrtion", "Event");
        }


        public ActionResult Logincalling()
        {
            // Clear session data  Session.Clear();

            // Redirect to Login action in the Event controller
            return RedirectToAction("Login", "Event");
        }


        public ActionResult admin()
        {


            return View();



        }

        public void CreateAndSendPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.AlertMessage = "Email is required.";
            }

            // Step 1: Generate a random password
            string randomPassword = GenerateRandomPassword();

            passgene = randomPassword;

            // Step 2: Encrypt the password
            string encryptedPassword = EncryptPassword(randomPassword);

            // Step 3: Send the password via email
            bool emailSent = SendEmail(email, "Your New Password", $"Your new password is: {randomPassword}");

            if (emailSent)
            {
                // Save the encrypted password to the database (not shown here)
                ViewBag.AlertMessage = "Password created and sent successfully.";
            }
            else
            {
                ViewBag.AlertMessage = "Failed to send email. Please try again.";
            }
        }




        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            Random random = new Random();
            char[] passwordChars = new char[10]; // Adjust the length as needed

            for (int i = 0; i < passwordChars.Length; i++)
            {
                passwordChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(passwordChars);
        }

  


        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("adhilhabeeb960571@gmail.com"); // Replace with your email
                // 
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587, // Replace with your SMTP port
                    Credentials = new System.Net.NetworkCredential("adhilhabeeb960571@gmail.com", "hxppgeqwqjmfjmzd"), // Replace with your credentials  ithile hxppgeqwqjmfjmzd  from tghe app password 
                    EnableSsl = true
                };

                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        private void Connection()
        {
            string consstring = ConfigurationManager.ConnectionStrings["getconnect"].ToString();

            con = new SqlConnection(consstring);
        }
        public ActionResult registrtion() { 
       
            
            return View();
        
        
        
        }
        [HttpPost]
      
        public ActionResult registrtion(Regmodel reg)
        {
          

            
           
            try
            {
                if (ModelState.IsValid)
                {
                    Eventrepositry eventrepositry = new Eventrepositry();
                    bool isRegistered = Register(reg);

                    if (isRegistered)

                    {
                        ViewBag.AlertMessage = "Registered successfully!";
                        ModelState.Clear(); // Clears all data from the form after successful registration
                        return RedirectToAction("Registrtion"); // Optionally redirect to the same page or another page
                    }
                    else
                    {
                        ViewBag.maage = "Registration failed. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., to a file or database)
                ViewBag.Message = "An error occurred. Please try again.";
            }

            return View();
        }

        public bool Register(Regmodel reg)
        {

            CreateAndSendPassword(reg.email);
            Connection();

            if (passgene !=null)
            {

                ViewBag.AlertMessage = passgene;

                SqlCommand cmd = new SqlCommand("InsertUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", reg.name);
                // Email parameter
                cmd.Parameters.AddWithValue("@UserType", "admin");            // Email parameter


                // Adding parameters with the names as defined in the stored procedure
                cmd.Parameters.AddWithValue("@Email", reg.email);            // Email parameter
                cmd.Parameters.AddWithValue("@Password", passgene);      // Password parameter
                cmd.Parameters.AddWithValue("@CurrentPassword", passgene); // CurrentPassword paramete

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                // Check if the insertion was successful based on rows affected
                if (rowsAffected > 0)
                {
                    return true; // Return true if insertion was successful
                }
                else
                {
                    return false; // Return false if insertion failed
                }
            }
            else
            {
                ViewBag.AlertMessage = "illa";
                return false;

             
            }
         // Make sure the Connection() method is called to initialize the connection

            // Create a new SqlCommand to call the InsertUser stored procedure



            // Open the connection and execute the query
          
        }


        public ActionResult AddUser()
    {
        // Create a list to hold the user data
        List<Regmodel> userList = new List<Regmodel>();

        // Connection string from the web.config file
        string connectionString = ConfigurationManager.ConnectionStrings["getconnect"].ToString();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // Define the SQL query
            string query = "SELECT ID, Name, Email, Password, CurrentPassword FROM usertable";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Open the connection
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Read each row and add it to the list
                    while (reader.Read())
                    {
                        userList.Add(new Regmodel
                        {
                            id = Convert.ToInt32(reader["ID"]),
                            name = reader["Name"].ToString(),
                            email = reader["Email"].ToString(),
                            password = reader["Password"].ToString(),
                            currentpassword = reader["CurrentPassword"].ToString()
                        });
                    }
                }
            }
        }

        // Pass the list to the view
        return View(userList);
    }

        public ActionResult Resetpasword()
        {

            return View();
        }



    // GET: event
    //public ActionResult Index()
    //{
    //    return View();
    //}

        //// GET: event/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: event/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: event/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: event/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: event/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: event/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: event/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
}
}
