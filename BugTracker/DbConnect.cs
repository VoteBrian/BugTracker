using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    class DbConnect
    {
        enum DB_ERROR{
            NO_ERROR,
            DOES_NOT_EXIST,
            TABLE_MISSING,
            NUM_ERROR_MSGS,
            QUERY_FAILED,
            NO_DATA_RETURNED
        };

        string strAccessConn = "Provider=Microsoft.SQLSERVER.CE.OLEDB.4.0;Data Source=..\\..\\bugtracker.sdf;";
        OleDbConnection myAccessConn = null;


        /*****************************************************************************/
        public DbConnect()
        /*****************************************************************************/
        {
            // Constructor does nothing
        }

        /*****************************************************************************/
        public void QueryUser_ProjectTitle()
        /*****************************************************************************/
        {
            Window_InitProject ip = new Window_InitProject();
            ip.ShowDialog();
        }
        
        /*****************************************************************************/
        public int GetProjectName(ref String name)
        /*****************************************************************************/
        {
            Boolean status;
            String projectQuery;
            String projectTable = "Projects";
            DataRowCollection dataRows;
            DataRow row;
            DataSet dataSet = new DataSet();

            // connect and open database
            status = ConnectDb();
            if (status != true)
            {
                // Database doesn't exist
                return (int)DB_ERROR.DOES_NOT_EXIST;
            }

            projectQuery = "SELECT * from Projects";
            status = QueryDb(projectQuery, projectTable, ref dataSet);
            if (status != true)
            {
                return (int)DB_ERROR.QUERY_FAILED;
            }

            dataRows = dataSet.Tables["Projects"].Rows;
            if (dataRows.Count <= 0)
            {
                return (int)DB_ERROR.NO_DATA_RETURNED;
            }
            else
            {
                row = dataRows[0];
                name = (String)row[1];
            }

            // close database connection
            CloseDb();

            return (int)DB_ERROR.NO_ERROR;
        }

        /*****************************************************************************/
        public int Update_ProjectName(String name)
        /*****************************************************************************/
        {
            Boolean status;
            String projectQuery = "";

            // connect and open database
            status = ConnectDb();
            if (status != true)
            {
                // Database doesn't exist
                return (int)DB_ERROR.DOES_NOT_EXIST;
            }
            
            projectQuery = String.Format("UPDATE Projects SET name = '{0}' WHERE (ID = 1)", name);
            
            status = UpdateDb(projectQuery);
            if (status != true)
            {
                return (int)DB_ERROR.QUERY_FAILED;
            }

            // close database connection
            CloseDb();

            return (int)DB_ERROR.NO_ERROR;
        }

        /*****************************************************************************/
        public int CreateNewBug(String title, String description, int priority)
        /*****************************************************************************/
        {
            String query;
            Boolean status;
            // ----------------------------------------

            // connect and open database
            status = ConnectDb();
            if (status != true)
            {
                // Database doesn't exist
                return (int)DB_ERROR.DOES_NOT_EXIST;
            }

            query = String.Format("INSERT INTO Bugs (title, [desc], priority) VALUES ('{0}', '{1}', {2})",
                title, description, priority);

            status = UpdateDb(query);
            if (status != true)
            {
                return (int)DB_ERROR.QUERY_FAILED;
            }

            // close database connection
            CloseDb();

            return (int)DB_ERROR.NO_ERROR;
        }

        /*****************************************************************************/
        private Boolean ConnectDb()
        /*****************************************************************************/
        {
            // Connect to database
            try
            {
                myAccessConn = new OleDbConnection(strAccessConn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
                return false;
            }

            try
            {
                myAccessConn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to open database connection \n{0}", ex.Message);
                return false;
            }

            return true;
        }

        /*****************************************************************************/
        private void CloseDb()
        /*****************************************************************************/
        {
            myAccessConn.Close();
        }

        /*****************************************************************************/
        private Boolean QueryDb(String query, String table, ref DataSet data)
        /*****************************************************************************/
        {
            // Query table Projects
            try
            {

                OleDbCommand myAccessCommand = new OleDbCommand(query, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                myDataAdapter.Fill(data, table);

                // myAccessCommand.ExecuteNonQuery();

                // myAccessCommand = new OleDbCommand(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                return false;
            }

            return true;
        }

        /*****************************************************************************/
        private Boolean UpdateDb(String query)
        /*****************************************************************************/
        {
            int lines_affected;

            OleDbCommand myAccessCommand = new OleDbCommand(query, myAccessConn);

            try
            {
                lines_affected = myAccessCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to execute query.\n{0}", ex.Message);
                return false;
            }

            return true;
        }
    }
}
