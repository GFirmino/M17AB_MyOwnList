using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class User {
        public int iduser;
        public string username;
        public string email;
        public string password;
        public int profiletype;
        public bool isdarkmode;
        public bool issupended;
        public string recuplink;
        public DateTime recupdate;
        public DateTime created;
        public DateTime lastupdate;
        Classes.DataBase db;

        public User(int iduser, string username, string email, string password, int profiletype, bool isdarkmode,
                    bool issupended, string recuplink, DateTime recupdate, DateTime created, DateTime lastupdate) {
            this.iduser = iduser;
            this.username = username;
            this.email = email;
            this.password = password;
            this.profiletype = profiletype;
            this.isdarkmode = isdarkmode;
            this.issupended = issupended;
            this.recuplink = recuplink;
            this.recupdate = recupdate;
            this.created = created;
            this.lastupdate = lastupdate;
            this.db = new Classes.DataBase();
        }
        public User(int iduser, string username, string email, string password, int profiletype, bool isdarkmode,
            bool issupended, string recuplink, DateTime created, DateTime lastupdate) {
            this.iduser = iduser;
            this.username = username;
            this.email = email;
            this.password = password;
            this.profiletype = profiletype;
            this.isdarkmode = isdarkmode;
            this.issupended = issupended;
            this.recuplink = recuplink;
            this.created = created;
            this.lastupdate = lastupdate;
            this.db = new Classes.DataBase();
        }
        public User(string username, string email, string password, int profiletype, bool isdarkmode, bool issupended) {
            this.username = username;
            this.email = email;
            this.password = password;
            this.profiletype = profiletype;
            this.isdarkmode = isdarkmode;
            this.issupended = issupended;
            this.db = new Classes.DataBase();
        }
        public User(string username, string email, string password) {
            this.username = username;
            this.email = email;
            this.password = password;
            this.profiletype = 0;
            this.isdarkmode = false;
            this.issupended = false;
            this.db = new Classes.DataBase();
        }
        public int add() {
            string sql = @"INSERT INTO users(username, email, password, profiletype, isdarkmode, issuspended)
                            VALUES (@username, @email, HASHBYTES('SHA2_512', @password), @profiletype, @isdarkmode, @issuspended);SELECT CAST(SCOPE_IDENTITY() AS INT)";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@username",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.username
                },
                new SqlParameter(){
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.email
                },
                new SqlParameter(){
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.password
                },
                new SqlParameter(){
                    ParameterName="@profiletype",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.profiletype
                },
                new SqlParameter(){
                    ParameterName="@isdarkmode",
                    SqlDbType=System.Data.SqlDbType.Bit,
                    Value=this.isdarkmode
                },
                    new SqlParameter(){
                    ParameterName="@issuspended",
                    SqlDbType=System.Data.SqlDbType.Bit,
                    Value=this.issupended
                }
            };
            return db.runAndRecieveSQL(sql, parameters);
        }
        static public void delete(int id) {
            Classes.DataBase db = new Classes.DataBase();
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@iduser",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id
                },
            };

            string strSQL = $"DELETE FROM users WHERE iduser=@iduser";

            db.runSQL(strSQL, parameters);
        }
        static public void updateUsername(int id, string newUsername) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"UPDATE users SET username = @username WHERE iduser = @iduser";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@username",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=newUsername
                },
                new SqlParameter(){
                    ParameterName="@iduser",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void updateEmail(int id, string newEmail) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"UPDATE users SET email = @email WHERE iduser = @iduser";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=newEmail
                },
                new SqlParameter(){
                    ParameterName="@iduser",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void recoverPassword(string guid, string email) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"UPDATE users SET recuplink = @recuplink WHERE email=@email";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@email",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=email
                },
                new SqlParameter(){
                    ParameterName="@recuplink",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=guid
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void updatePassword(string guid, string newPassword) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"UPDATE users SET password = HASHBYTES('SHA2_512',@password), recuplink=null WHERE recuplink = @recuplink";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@password",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=newPassword
                },
                new SqlParameter(){
                    ParameterName="@recuplink",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=guid
                },
            };
            db.runSQL(sql, parameters);
        }
        static public bool readSuspendStatus(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"SELECT issuspended FROM users WHERE iduser=@iduser";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter(){
                    ParameterName="@iduser",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=id
                },
            };
           DataTable data = db.recieveSQL(sql, parameters);
            return bool.Parse(data.Rows[0][0].ToString());
        }
        static public void suspendAccount(int id){
            Classes.DataBase db = new Classes.DataBase();
            bool sus = readSuspendStatus(id);
            if (sus == true) sus = false;
            else sus = true;

            string strSQL = "UPDATE users SET issuspended=@issuspended WHERE iduser=@iduser";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {ParameterName="@issuspended",SqlDbType=SqlDbType.Bit,Value=sus },
                new SqlParameter() {ParameterName="@iduser",SqlDbType=SqlDbType.Int,Value=id }
            };
            db.runSQL(strSQL, parameters);
        }
        static public DataTable recieveUserDataByEmail(string email) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM users WHERE email=@email";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@email",
                    SqlDbType=SqlDbType.VarChar,
                    Value=email 
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public User recieveUserByID(int id) {
            Classes.DataBase db = new Classes.DataBase();

            DataTable data = recieveUserDataByID(id);
            int id_, profiletype_;
            string username_, email_, password_, recuplink_;
            bool isdarkmode_, issuspended_;
            DateTime recupdate_ = DateTime.Now, created_, lastupdate_;

            bool recupdateNull = false;

            try {
                recupdate_ = DateTime.Parse(data.Rows[0]["recupdate"].ToString());
            } catch { 
                recupdateNull = true; 
            } finally {
                id_ = int.Parse(data.Rows[0]["iduser"].ToString());
                username_ = data.Rows[0]["username"].ToString();
                email_ = data.Rows[0]["email"].ToString();
                password_ = data.Rows[0]["password"].ToString();
                profiletype_ = int.Parse(data.Rows[0]["profiletype"].ToString());
                isdarkmode_ = bool.Parse(data.Rows[0]["isdarkmode"].ToString());
                issuspended_ = bool.Parse(data.Rows[0]["issuspended"].ToString());
                recuplink_ = data.Rows[0]["recuplink"].ToString();
                created_ = DateTime.Parse(data.Rows[0]["created"].ToString());
                lastupdate_ = DateTime.Parse(data.Rows[0]["lastupdate"].ToString());
            }

            User user;

            if (recupdateNull == true) {
                user = new User(id_, username_, email_, password_, profiletype_, isdarkmode_, issuspended_, recuplink_, created_, lastupdate_);
            } else {
                user = new User(id_, username_, email_, password_, profiletype_, isdarkmode_, issuspended_, recuplink_, recupdate_, created_, lastupdate_);
            }

            return user;
        }
        static public DataTable recieveUserDataByID(int id) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM users WHERE iduser=@iduser";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@iduser",
                    SqlDbType=SqlDbType.Int,
                    Value=id
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable recieveUserDataByUserName(string username) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM users WHERE username=@username";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@username",
                    SqlDbType=SqlDbType.VarChar,
                    Value=username
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable ListAllUsers() {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT iduser as IdUser, email as Email, IIF(profiletype = 0, 'Utilizador', 'Administrador') as ProfileType, " +
                            "IIF(isdarkmode = 0, 'false', 'true') as isDarkMode, IIF(issuspended = 0, 'false', 'true') as IsSuspended, " +
                            "IIF(len(recuplink) != 0, 'link enviado', '') as RecoverLink, created as Created, lastupdate as LastUpdate" +
                            "FROM users";

            return db.recieveSQL(strSQL);
        }
        static public DataTable ListAllUsers(bool admin = false) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT iduser as IdUser, email as Email, IIF(profiletype = 0, 'Utilizador', 'Administrador') as ProfileType, " +
                            "IIF(isdarkmode = 0, 'false', 'true') as isDarkMode, IIF(issuspended = 0, 'false', 'true') as IsSuspended, " +
                            "IIF(len(recuplink) != 0, 'link enviado', '') as RecoverLink, created as Created, lastupdate as LastUpdate" +
                            "FROM users WHERE profiletype = ";
            if (admin == true)
                strSQL += "1";
            else
                strSQL += "0";

            return db.recieveSQL(strSQL);
        }
        static public DataTable ListAllUsersExceptSuspended() {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT iduser as IdUser, email as Email, IIF(profiletype = 0, 'Utilizador', 'Administrador') as ProfileType, " +
                            "IIF(isdarkmode = 0, 'false', 'true') as isDarkMode, IIF(issuspended = 0, 'false', 'true') as IsSuspended, " +
                            "IIF(len(recuplink) != 0, 'link enviado', '') as RecoverLink, created as Created, lastupdate as LastUpdate" +
                            "FROM users WHERE issuspended = 0";

            return db.recieveSQL(strSQL);
        }
        static public DataTable ListAllUsersWithDarkModeIn(bool darkmode) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT iduser as IdUser, email as Email, IIF(profiletype = 0, 'Utilizador', 'Administrador') as ProfileType, " +
                            "IIF(isdarkmode = 0, 'false', 'true') as isDarkMode, IIF(issuspended = 0, 'false', 'true') as IsSuspended, " +
                            "IIF(len(recuplink) != 0, 'link enviado', '') as RecoverLink, created as Created, lastupdate as LastUpdate" +
                            "FROM users WHERE isdarkmode = ";
            if (darkmode == true)
                strSQL += "1";
            else
                strSQL += "0";

            return db.recieveSQL(strSQL);
        }
        static public DataTable ListAllUsersWithPasswordForRecover(bool passrecover) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT iduser as IdUser, email as Email, IIF(profiletype = 0, 'Utilizador', 'Administrador') as ProfileType, " +
                            "IIF(isdarkmode = 0, 'false', 'true') as isDarkMode, IIF(issuspended = 0, 'false', 'true') as IsSuspended, " +
                            "IIF(len(recuplink) != 0, 'link enviado', '') as RecoverLink, created as Created, lastupdate as LastUpdate" +
                            "FROM users WHERE len(recuplink) ";
            if (passrecover == true)
                strSQL += " != 0";
            else
                strSQL += "= 0";

            return db.recieveSQL(strSQL);
        }
        static public DataTable LoginVerify(string email, string password) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM users WHERE email=@email AND password=HASHBYTES('SHA2_512', @password)";

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