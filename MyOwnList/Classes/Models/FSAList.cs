using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class FSAList {
        public int idreg;
        public int iduser;
        public int idfsa;
        public int typefsa;
        public int personalrating;
        public int watchedeps;
        public int statusfsa;
        public bool favorite;
        public DateTime created;
        public DateTime lastupdate;
        Classes.DataBase db;

        public FSAList(int idreg, int iduser, int idfsa, int typefsa, int personalrating, int watchedeps, 
                       int statusfsa, bool favorite, DateTime created, DateTime lastupdate) {
            this.idreg = idreg;
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.personalrating = personalrating;
            this.watchedeps = watchedeps;
            this.statusfsa = statusfsa;
            this.favorite = favorite;
            this.created = created;
            this.lastupdate = lastupdate;
        }

        public FSAList(int iduser, int idfsa, int typefsa, int personalrating, int watchedeps, int statusfsa, bool favorite) {
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.personalrating = personalrating;
            this.watchedeps = watchedeps;
            this.statusfsa = statusfsa;
            this.favorite = favorite;
        }

        public FSAList(int iduser, int idfsa, int typefsa, int personalrating, int statusfsa) {
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.personalrating = personalrating;
            this.watchedeps = 0;
            this.statusfsa = statusfsa;
            this.favorite = false;
        }
        public void add() {
            string sql = @"INSERT INTO userfsa_regs(iduser, idfsa, typefsa, personalrating, watchedeps, statusfsa, favorite)
                            VALUES (@iduser, @idfsa, @typefsa, @personalrating, @watchedeps, @statusfsa, @favorite)";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@iduser",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.iduser
                },
                new SqlParameter() {
                ParameterName = "@idfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idfsa
                },
                new SqlParameter() {
                ParameterName = "@typefsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.typefsa
                },
                new SqlParameter() {
                ParameterName = "@personalrating",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.personalrating
                },
                new SqlParameter() {
                ParameterName = "@watchedeps",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.watchedeps
                },
                new SqlParameter() {
                ParameterName = "@statusfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.statusfsa
                },
                new SqlParameter() {
                ParameterName = "@favorite",
                SqlDbType = System.Data.SqlDbType.Bit,
                Value = this.favorite
                },
            };
            db.runSQL(sql, parameters);
        }
        public void update() {
            string sql = @"UPDATE userfsa_regs SET personalrating = @personalrating, 
                           watchedeps = @watchedeps, statusfsa = @statusfsa, lastupdate = getdate() 
                           WHERE idreg=@idreg";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idreg",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idreg
                },
                new SqlParameter() {
                ParameterName = "@personalrating",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.personalrating
                },
                new SqlParameter() {
                ParameterName = "@watchedeps",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.watchedeps
                },
                new SqlParameter() {
                ParameterName = "@statusfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.statusfsa
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void delete(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"DELETE FROM userfsa_regs WHERE idreg=@idreg";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idreg",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
                },
            };
            db.runSQL(sql, parameters);
        }
        static public DataTable ListFSAByUser(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM userfsa_regs WHERE iduser=@iduser";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@iduser",
                    SqlDbType=SqlDbType.Int,
                    Value= id
                },
            };

            return db.recieveSQL(strSQL, parameters);
        }
    }
}