//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using System.Xml;
//namespace BusinessModel.Common
//{
//    public class DAL
//    {
//        public string ConnectionString;
//        public int CommandTimeout;
//        public const string lstrFolderName = "SQL";
//        string MyConnection = ConfigurationManager.ConnectionStrings["SQLConnection123"].ConnectionString;

//        public DAL()
//        {
//            Log objLog = new Log();
//            XmlDocument XMLDoc = new XmlDocument();
//            string lstrSQLCONN = string.Empty;
//            string lstrDBTimeout = string.Empty;
//            string lstrSPTimeout = string.Empty;
//            try
//            {

//                // string filename = ConfigurationManager.AppSettings["Path"];
//                string filename = Configuration.GetSection("appSettings")["Path"];
//                //AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

//                XMLDoc.Load(filename);
//                XmlNode xnSQLConnection = XMLDoc.SelectSingleNode("/root/SqlConnection");
//                lstrSQLCONN = xnSQLConnection != null ? xnSQLConnection.InnerText : "";

//                XmlNode xnDBTimeout = XMLDoc.SelectSingleNode("/root/DBTimeout");
//                lstrDBTimeout = xnDBTimeout != null ? xnDBTimeout.InnerText : "";

//                XmlNode xnSPTimeout = XMLDoc.SelectSingleNode("/root/SPTimeout");
//                lstrSPTimeout = xnSPTimeout != null ? xnSPTimeout.InnerText : "";

//                ConnectionString = lstrSQLCONN + ";" + lstrDBTimeout;
//                CommandTimeout = Convert.ToInt32(lstrSPTimeout);

//            }
//            catch (Exception ex)
//            {
//                objLog.WriteErrorLog("SQL :" + ex.ToString(), lstrFolderName);
//            }
//            finally
//            {
//                objLog = null;
//                XMLDoc = null;
//            }
//        }

//        public DAL(string ConnString)
//        {
//            Log objLog = new Log();
//            try
//            {
//                ConnectionString = ConnString;
//            }
//            catch (Exception ex)
//            {
//                objLog.WriteErrorLog("SQL With Param :" + ex.ToString(), lstrFolderName);
//            }
//            finally
//            {
//                objLog = null;
//            }
//        }

//        public DataSet ExecuteQuery(string Query)
//        {
//            Log objLog = new Log();
//            try
//            {

//                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
//                {
//                    sqlConnection.Open();
//                    SqlDataAdapter DA = new SqlDataAdapter();
//                    SqlCommand CMD = new SqlCommand();
//                    DataSet dataSet = new DataSet();
//                    CMD.Connection = sqlConnection;
//                    CMD.CommandText = Query;
//                    CMD.CommandType = CommandType.Text;
//                    DA.SelectCommand = CMD;
//                    DA.SelectCommand.CommandTimeout = CommandTimeout;
//                    DA.Fill(dataSet);
//                    sqlConnection.Close();
//                    return dataSet;
//                }
//            }
//            catch (Exception ex)
//            {
//                objLog.WriteErrorLog("ExecuteQuery :" + ex.ToString(), lstrFolderName);
//                return null;
//            }
//            finally
//            {
//                objLog = null;
//            }
//        }

//        public DataSet ExecuteQueryWithParam(SqlCommand CMD, string SP)
//        {
//            Log objLog = new Log();
//            try
//            {
//                using (SqlConnection sqlConnection = new SqlConnection(MyConnection))

//                // using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
//                {
//                    sqlConnection.Open();
//                    SqlDataAdapter DA = new SqlDataAdapter(CMD);
//                    DataSet dataSet = new DataSet();
//                    CMD.Connection = sqlConnection;
//                    //CMD.CommandTimeout = 0;
//                    CMD.CommandText = SP;
//                    CMD.CommandType = CommandType.StoredProcedure;
//                    DA.SelectCommand.CommandTimeout = CommandTimeout;
//                    DA.Fill(dataSet);
//                    sqlConnection.Close();
//                    return dataSet;
//                }
//            }
//            catch (Exception ex)
//            {
//                objLog.WriteErrorLog("ExecuteQueryWithParam :" + ex.ToString(), lstrFolderName);
//                return null;
//            }
//            finally
//            {
//                objLog = null;
//            }
//        }

//        public DataSet ExecuteQueryWithoutParam(string SP)
//        {
//            Log objLog = new Log();
//            try
//            {
//                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
//                {
//                    sqlConnection.Open();

//                    SqlCommand CMD = new SqlCommand();
//                    DataSet DS = new DataSet();
//                    CMD.Connection = sqlConnection;
//                    CMD.CommandText = SP;
//                    CMD.CommandTimeout = 0;
//                    CMD.CommandType = CommandType.StoredProcedure;
//                    SqlDataAdapter DA = new SqlDataAdapter(CMD);
//                    DA.SelectCommand.CommandTimeout = CommandTimeout;
//                    DA.Fill(DS);
//                    sqlConnection.Close();
//                    return DS;
//                }
//            }
//            catch (Exception ex)
//            {
//                objLog.WriteErrorLog("ExecuteQueryWithoutParam :" + ex.ToString(), lstrFolderName);
//                return null;
//            }
//            finally
//            {
//                objLog = null;
//            }
//        }
//    }















//}

