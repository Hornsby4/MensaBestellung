using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

namespace DataBaseWrapper
{
    public class DataBase
    {
        OdbcConnection connection;
        public DataBase(string connStrg){ connection = new OdbcConnection(connStrg); }
        public bool ConnectionIsOpen{ get => connection.State == ConnectionState.Open; }
        public void Open(){ connection.Open(); }
        public void Close(){ connection.Close(); }
        public object RunQueryScalar(string sqlCmd)
        {
            OdbcCommand cmd = new OdbcCommand();
            object anyValue;
            bool isConnectionInitiallyClosed = connection.State == ConnectionState.Closed;
            if (isConnectionInitiallyClosed){ Open(); }
            try
            {
                cmd.CommandText = sqlCmd;
                cmd.Connection = connection;
                anyValue = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                if (isConnectionInitiallyClosed){ Close(); }
            }
            return anyValue;
        }
        public DataTable RunQuery(string sqlCmd)
        {
            DataTable resultSet = new DataTable();
            // Connection open/close automatically
            // managed by DataAdapter 
            OdbcDataAdapter da = new OdbcDataAdapter(sqlCmd, connection);
            da.Fill(resultSet);
            return resultSet;
        }
        /// <summary>
        /// Bei geschlossener Connection wird diese geöffnet und
        /// am Ende der Methode wieder geschlossen
        /// Bei geöffneter Connection wird diese nicht geöffnet und am Ende
         /// der Methode nicht geschlossen
        /// Auch bei einem LZF muss der ursprüngliche Verbindungsstatus 
        /// wieder hergestellt werden
         /// </summary>
         /// <param name="sqlCmd"></param>
        /// <returns></returns>
        public int RunNonQuery(string sqlCmd)
        {
            int numRecs = 0;
            OdbcCommand cmd = new OdbcCommand();
            bool isConnectionInitiallyClosed = connection.State == ConnectionState.Closed;
            cmd.Connection = connection;
            cmd.CommandText = sqlCmd;
            //Open();
            if (isConnectionInitiallyClosed){ Open(); }
            try
            {
                numRecs = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; // new Exception(ex.Message);
            }
            finally
            {
                if (isConnectionInitiallyClosed){ Close(); }
            }
            //Close();
            return numRecs;
        }
    }
}