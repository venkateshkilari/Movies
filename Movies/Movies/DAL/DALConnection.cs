
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Movies.DAL
{
    public class DALConnection
    {
        private SqlConnection sqlConnection;
        private string connectionString;
        public DALConnection(string connectionString )
        {
            this.connectionString = connectionString;
        }
        public void GetConnection()
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
              
                
                
                    
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }
        
        public DataTable selectFromDatabase(string query)
        {
            SqlDataAdapter sqlCommand;
            DataTable datable=new DataTable();
            try
            {
                GetConnection();
                sqlCommand = new SqlDataAdapter(query,sqlConnection);
               sqlCommand.Fill(datable);
              

                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                
            }
            return datable;
        }

        public DataSet selectDatasetFromDatabase(string query)
        {
            SqlDataAdapter sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                GetConnection();
                sqlCommand = new SqlDataAdapter(query, sqlConnection);
                sqlCommand.Fill(dataSet);


                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return dataSet;
        }
        public bool ExecuteNonQuery(string query)
        {
            SqlCommand sqlCommand;
            bool result;
            try
            {
                GetConnection();
                sqlCommand = new SqlCommand(query, sqlConnection);
                result=Convert.ToBoolean(sqlCommand.ExecuteNonQuery());

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
            return result;
        }

    
    }
}
