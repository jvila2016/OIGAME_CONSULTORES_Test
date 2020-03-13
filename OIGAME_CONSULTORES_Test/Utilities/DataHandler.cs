using OIGAME_CONSULTORES_Test.Utilities;
using OIGAME_CONSULTORES_Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OIGAME_CONSULTORES_Test.Utilities
{
    public static class DataHandler
    {
        private static readonly log4net.ILog log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal static bool loginUser(string userid, string password)
        {
            string qryStr = @"SELECT * FROM PazYSalvo.dbo.Usuario
                            where iduser = '@idUser' and passw = '@password'";
            qryStr = qryStr.Replace("@idUser", userid);
            qryStr = qryStr.Replace("@password", password);

            SqlCommand command = new SqlCommand(qryStr);
            DataSet ds = DBManager.selectQuery(command, "PazySalvo");
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }


        internal static bool Insert_Customer(int? CustomerTypeId, string Passport, string FirstName, string LastName, bool Active)
        {
            try
            {
                SqlCommand command = new SqlCommand("SPCreateCustomer");
                command.Parameters.AddWithValue("@CustomerTypeId", CustomerTypeId);
                command.Parameters.AddWithValue("@Passport", Passport);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Active", Active);
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        
        internal static List<string> Get_UserRoll(string useremail)
        {
            try
            {
                string qryStr = @"SELECT r.Name
                                    FROM AspNetRoles r
                                    inner join AspNetUserRoles ur  
                                    ON ur.RoleId = r.Id inner join AspNetUsers us on us.Id = ur.UserId
                                    WHERE us.Email = @Email";

                SqlCommand command = new SqlCommand(qryStr);
                command.Parameters.AddWithValue("@Email", useremail);
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");

                List<string> NombRolles = new List<string>();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
                           NombRolles.Add(ds.Tables[0].Rows[i][0].ToString());
                  
                    return NombRolles;
                }

                NombRolles.Add("");
                return NombRolles;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        internal static bool PastportExist(string pasport)
        {

            try
            {
               
                SqlCommand command = new SqlCommand("Select * from Customer c where c.Passport = " + "'"+ pasport + "'");
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    

                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal static List<Customer> GetCustomerfromSPT()
        {
            try
            {

                SqlCommand command = new SqlCommand("AllCustomersActive");
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");


                List<Customer> customerList = new List<Customer>();

                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {

                    Customer c = new Customer();

                    c.Id = int.Parse(row["Id"].ToString());
                    c.CustomerTypeId =int.Parse(row["CustomerTypeId"].ToString());
                    c.Passport = row["Passport"].ToString();
                    c.FirstName = row["FirstName"].ToString();
                    c.LastName = row["LastName"].ToString();
                    c.Active = bool.Parse(row["Active"].ToString());

                    customerList.Add(c);

                }

                return customerList;
            }
            catch (Exception ex)
            {
                return null;

                throw ex;
            }
        }


        internal static List<Customer1> AllCustomerType1()
        {
            try
            {

                SqlCommand command = new SqlCommand("AllCustomerType1");
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");


                List<Customer1> customerList = new List<Customer1>();

                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {

                    Customer1 c = new Customer1();

                    c.Id = int.Parse(row["Id"].ToString());
                    c.Description = row["Description"].ToString();
                    c.Passport = row["Passport"].ToString();
                    c.FirstName = row["FirstName"].ToString();
                    c.LastName = row["LastName"].ToString();
                    c.Active = bool.Parse(row["Active"].ToString());

                    customerList.Add(c);

                }

                return customerList;
            }
            catch (Exception ex)
            {
                return null;

                throw ex;
            }
        }

        internal static void DeleteAllCustomerEnding7()
        {
            try
            {
                SqlCommand command = new SqlCommand("DeleteAllCustomerEnding7");
                DataSet ds = DBManager.selectQuery(command, "OIGAME_CONSULTORES_Test");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}