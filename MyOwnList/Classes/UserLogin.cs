using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Classes {
    public class UserLogin {
        DataBase db;
        public UserLogin() {
            this.db = new DataBase();
        }
        public DataTable LoginVerification(string email, string password) {
            string strSQL = "SELECT * FROM Utilizadores WHERE email=@email AND password=HASHBYTES('SHA2_512', @password) AND estado=1";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@email",
                    SqlDbType = SqlDbType.VarChar,
                    Value = email
                },
                    new SqlParameter() {
                    ParameterName="@password",
                    SqlDbType = SqlDbType.VarChar,
                    Value = password
                }
            };

            DataTable data = db.recieveSQL(strSQL, parameters);
            if (data == null || data.Rows.Count == 0 || data.Rows.Count > 1)
                return null;
            return data;
        }
    }
}