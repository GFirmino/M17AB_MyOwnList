using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Classes
{
    public class DataBase
    {
        private string ConnectionString;
        private SqlConnection DBConnection;
        public DataBase(){
            //ligação à bd
            ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ToString();
            DBConnection = new SqlConnection(ConnectionString);
            DBConnection.Open();
        }
        ~DataBase(){
            try{
                DBConnection.Close();
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        #region Transações
        public SqlTransaction startTransaction(){
            return DBConnection.BeginTransaction();
        }
        public SqlTransaction startTransaction(IsolationLevel isolamento){
            return DBConnection.BeginTransaction(isolamento);
        }
        #endregion
        #region SQL de ação
        public void runSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public void runSQL(string sql, List<SqlParameter> parametros){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public void runSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public int runAndRecieveSQL(string sql){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        public int runAndRecieveSQL(string sql, List<SqlParameter> parametros){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.Parameters.AddRange(parametros.ToArray());
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        public int runAndRecieveSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
            int id = (int)comando.ExecuteScalar();
            comando.Dispose();
            comando = null;
            return id;
        }
        #endregion
        #region SQL Consultas
        /// <summary>
        /// Devolve o resultado de uma consulta
        /// </summary>
        /// <param name="sql">Select à base de dados</param>
        /// <returns></returns>
        public DataTable recieveSQL(string sql){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        public DataTable recieveSQL(string sql, List<SqlParameter> parametros){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        public DataTable recieveSQL(string sql, List<SqlParameter> parametros, SqlTransaction transacao){
            SqlCommand comando = new SqlCommand(sql, DBConnection);
            comando.Transaction = transacao;
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
        #endregion
    }
}