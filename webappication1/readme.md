




 create table 
 USE mvceventmanagement;
GO

CREATE TABLE dbo.usertable
(
    ID INT IDENTITY(1,1) PRIMARY KEY,   -- Auto-incrementing ID
    Name VARCHAR(100)  NULL,          -- Name of the user (required)
    Email VARCHAR(100)  NULL ,  -- Email of the user (required and unique)
    Password VARCHAR(100)  NULL,      -- Password of the user (required)
    CurrentPassword VARCHAR(100)   NULL,        -- Current password (nullable)
    UserType VARCHAR(100)   NULL               -- User type (nullable)
);


1first  we craete procedure   in sql
USE [mvceventmanagement]
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 21-11-2024 09:57:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertUser]
    @Name VARCHAR(100) = NULL,               -- Name is nullable
    @Email VARCHAR(100) = NULL,              -- Email is nullable
    @Password VARCHAR(100) = NULL,           -- Password is nullable
    @CurrentPassword VARCHAR(100) = NULL,    -- CurrentPassword is nullable
    @UserType VARCHAR(100) = NULL            -- UserType is nullable
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert data into usertable
    INSERT INTO dbo.usertable 
        (Name, Email, Password, CurrentPassword, UserType)
    VALUES 
        (@Name, @Email, @Password, @CurrentPassword, @UserType);
END;


  

2  connected the data abdse i it   
Data Source=DESKTOP-637KI6S;Initial Catalog=mvceventmanagement;Integrated Security=True;

ennit  ettvum thazhe ulla il   webcpnfig il mnammal     configuration nu   idayil 	<connectionStrings>
		<add name="getconnect"
			 connectionString="Data Source=DESKTOP-637KI6S;Initial Catalog=mvceventmanagement;Integrated Security=True;"
			 providerName="System.Data.SqlClient" />
	</connectionStrings>


3  credated new   file  in repositry    
enit   private   void Connection()
        {
            string consstring = ConfigurationManager.ConnectionStrings["getconnect"].ToString();

            con= new SqlConnection(consstring);
        }

        ithile A A getconnect ennath   name="getconnect"  a  datastring ile name 2  step il nokk   

    (rigtclick-add--class)  ingne ann css  file ctrreate cheyynde 
4  craeted new file in  models  and added the called  regmodel.cs

  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webappication1.Models
{
    public class Regmodel
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string currentpassword { get; set; }
    }
}   ee  ithilke a a inport and exprt code koduth 





5   controllers in puthiya  file  
eventcontrolles.cs  ennitt 

public ActionResult registrtion() { 
       
            
            return View();
        
        
        
        }


        eecode cheyth 

  ennitt registrtion()   ithil rigt click cheythitt    ---add to view  --add  cheyynm 
  
  
  6    ennitt           conroller  in namml puthiyathayi crteate cheytha  file     il   thaxzhab ull acode add  
  
    [HttpPost]
  public ActionResult registrtion(Regmodel reg) {

            try
            {

                if (ModelState.IsValid)
                {
                    Eventrepositry  eventrepositry = new Eventrepositry();
                    if (eventrepositry.Register(reg))
                    {
                        ViewBag.Message = "registered suceedddfully";
                        ModelState.Clear();
                        
                    }

                }

                return View();
            } 
            catch (Exception ex) {

                throw ex;
            
            }
        
        }


          
        7   ennit     nammal reposirt=y il crfeate chetha  file   (eventrepositry)  il nammal   thaxha cpode idnm 
        
        public bool Register(Regmodel reg)
        {
           Connection();
            SqlCommand cmd = new SqlCommand("ourprocedurename", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", reg.email);
            cmd.Parameters.AddWithValue("@currentpassword", reg.currentpassword);

            cmd.Parameters.AddWithValue("@password", reg.password);
            con.Open();
            int i= cmd.ExecuteNonQuery();

            if (i > 0) {

                return true;


            }
            else
            {
                return false;
            }


        }   

        8       app  strt ile     routeconfig.cs il anamal    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "event", action = "registrtion", id = UrlParameter.Optional }
            );  ee ecoide paste cheyyn,


            evde action nammal regustration a akkanam   enityt   controller  namade contriolller name 




            9  login   mammal  sql      il   procedure  create  cheyyanam   athil    sql  il namala new query il   nammal 
