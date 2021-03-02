using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class FSAComment {
        public int idcommentfsa;
        public int iduser;
        public int idfsa;
        public int typefsa;
        public string message;
        public bool isactive;
        public DateTime created;
        public DateTime lastupdate;
        Classes.DataBase db;
        public FSAComment(int idcommentfsa, int iduser, int idfsa, int typefsa, string message, bool isactive, DateTime created, DateTime lastupdate) {
            this.idcommentfsa = idcommentfsa;
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.message = message;
            this.isactive = isactive;
            this.created = created;
            this.lastupdate = lastupdate;
        }
        public FSAComment(int iduser, int idfsa, int typefsa, string message, bool isactive) {
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.message = message;
            this.isactive = isactive;
        }
        public FSAComment(int iduser, int idfsa, int typefsa, string message) {
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.message = message;
            this.isactive = true;
        }
        public void add() {
            string sql = @"INSERT INTO fsa_comments(iduser, idfsa, typefsa, message, isactive)
                            VALUES (@iduser, @idfsa, @typefsa, @message, @isactive)";

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
                ParameterName = "@message",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.message
                },
                new SqlParameter() {
                ParameterName = "@isactive",
                SqlDbType = System.Data.SqlDbType.Bit,
                Value = this.isactive
                },
            };
            db.runSQL(sql, parameters);
        }
        public void update() {
            string sql = @"UPDATE fsa_comments SET message = @message, isactive = @isactive WHERE idcommentfsa=@idcommentfsa";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idcommentfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idcommentfsa
                },
                new SqlParameter() {
                ParameterName = "@message",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.message
                },
                new SqlParameter() {
                ParameterName = "@isactive",
                SqlDbType = System.Data.SqlDbType.Bit,
                Value = this.isactive
                },
            };
            db.runSQL(sql, parameters);
        }
        public void delete() {
            string sql = @"DELETE FROM fsa_comments WHERE idcommentfsa=@idcommentfsa";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idcommentfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idcommentfsa
                },
            };
            db.runSQL(sql, parameters);
        }
        static public DataTable recieveCategoryByID(int id) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM categoriesfsa WHERE idcategory=@idcategory";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@idcategory",
                    SqlDbType=SqlDbType.Int,
                    Value=id
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable recieveCategoryByName(string name) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM categoriesfsa WHERE name=@name";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@name",
                    SqlDbType=SqlDbType.VarChar,
                    Value=name
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable ListAllCommentsByUser(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM fsa_comments WHERE iduser=@iduser";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@iduser",
                    SqlDbType=SqlDbType.Int,
                    Value= id
                },
            };

            return db.recieveSQL(strSQL, parameters);
        }
        static public DataTable ListAllCommentsByFSA(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM fsa_comments WHERE idfsa=@idfsa ORDER BY created DESC";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@idfsa",
                    SqlDbType=SqlDbType.Int,
                    Value= id
                },
            };

            return db.recieveSQL(strSQL, parameters);
        }
    }
}