using OIGAME_CONSULTORES_Test.Models.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OIGAME_CONSULTORES_Test.Utilities
{
    public static class DBManager
    { 
        public static int executeQuery(SqlCommand command, string database)
        {

            try
            {
                int resul = int.MinValue;
                SqlTransaction transaction = default(SqlTransaction);
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[database].ConnectionString;
                if (command.CommandText.Contains(" "))
                {
                    command.CommandType = CommandType.Text;
                }
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                connection.Open();
                transaction = connection.BeginTransaction("New query Transaction");
                try
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    resul = command.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                catch (Exception gEx)
                {
                    throw gEx;
                }
                finally
                {
                    connection.Dispose();
                }
                return resul;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public static DataSet selectQuery(SqlCommand command, string database)
        {
            try
            {

                DataSet dataSet = new DataSet();
                int resul = int.MinValue;
                SqlTransaction transaction = default(SqlTransaction);
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ModelOIGAMECONSULTORESTest"].ConnectionString;
                //connection.ConnectionString = Encr_Decr.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["ModelOIGAMECONSULTORESTest"].ConnectionString);
                
                if (command.CommandText.Contains(" "))
                {
                    command.CommandType = CommandType.Text;

                }
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                connection.Open();
                transaction = connection.BeginTransaction("New select Transaction");
                try
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dataSet);
                    transaction.Commit();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Dispose();
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static DataTable selectQuery2(SqlCommand command, string database)
        {
            try
            {
                DataTable datatable = new DataTable();
                int resul = int.MinValue;
                SqlTransaction transaction = default(SqlTransaction);
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Encr_Decr.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["ModelPazYSalvo"].ConnectionString);
                if (command.CommandText.Contains(" "))
                {
                    command.CommandType = CommandType.Text;

                }
                else
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
                connection.Open();
                transaction = connection.BeginTransaction("New select Transaction");
                try
                {
                    command.Connection = connection;
                    command.Transaction = transaction;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(datatable);
                    transaction.Commit();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Dispose();
                }
                return datatable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}