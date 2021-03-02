using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class FSA {
        public int idfsa;
        public int typefsa;
        public int idcategory;
        public string name;
        public string description;
        public int duration;
        public int rating;
        public int numeps;
        public int numseasons;
        public DateTime created;
        public DateTime lastupdate;
        Classes.DataBase db;

        public FSA(int idfsa, int typefsa, int idcategory, string name, string description, int duration, int rating, 
                   int numeps, int numseasons, DateTime created, DateTime lastupdate) {
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.idcategory = idcategory;
            this.name = name;
            this.description = description;
            this.duration = duration;
            this.rating = rating;
            this.numeps = numeps;
            this.numseasons = numseasons;
            this.created = created;
            this.lastupdate = lastupdate;
        }

        public FSA(int typefsa, int idcategory, string name, string description, int duration, int rating, int numeps, int numseasons) {
            this.typefsa = typefsa;
            this.idcategory = idcategory;
            this.name = name;
            this.description = description;
            this.duration = duration;
            this.rating = rating;
            this.numeps = numeps;
            this.numseasons = numseasons;
        }

        public FSA(int typefsa, int idcategory, string name, string description) {
            this.typefsa = typefsa;
            this.idcategory = idcategory;
            this.name = name;
            this.description = description;
        }
        public void add() {
            string sql = @"INSERT INTO datafsa(typefsa, idcategory, name, description)
                            VALUES (@typefsa, @idcategory, @name, @description)";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@typefsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.typefsa
                },
                new SqlParameter() {
                ParameterName = "@idcategory",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idcategory
                },
                new SqlParameter() {
                ParameterName = "@name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.name
                },
                new SqlParameter() {
                ParameterName = "@description",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.description
                },
            };
            db.runSQL(sql, parameters);
        }
        public void update() {
            string sql = @"UPDATE categoriesfsa SET typefsa=@typefsa, idcategory=@idcategory, 
                           name=@name, description=@description, duration=@duration, rating=@rating, 
                           numeps=@numeps, numseasons=@numseasons, lastupdate=getdate() WHERE idfsa=@idfsa";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idfsa",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.idfsa
                },
                new SqlParameter() {
                ParameterName = "@typefsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.typefsa
                },
                new SqlParameter() {
                ParameterName = "@idcategory",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idcategory
                },
                new SqlParameter() {
                ParameterName = "@name",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.name
                },
                new SqlParameter() {
                ParameterName = "@description",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = this.description
                },
                new SqlParameter() {
                ParameterName = "@duration",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.duration
                },
                new SqlParameter() {
                ParameterName = "@rating",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.rating
                },
                new SqlParameter() {
                ParameterName = "@numeps",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.numeps
                },
                new SqlParameter() {
                ParameterName = "@numseasons",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.numseasons
                },
            };

            db.runSQL(sql, parameters);
        }
        static public void delete(int id) {
            Classes.DataBase db = new Classes.DataBase();
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idfsa",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
                },
            };
            string strSQL = $@"DELETE FROM datafsa WHERE idcategory = @idcategory";

            db.runSQL(strSQL, parameters);
        }
        static public DataTable recieveFSAByName(string name) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM datafsa WHERE name=@name";

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
        static public DataTable recieveFSAByID(int id) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM datafsa WHERE idfsa=@idfsa";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@idfsa",
                    SqlDbType=SqlDbType.Int,
                    Value=id
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable ListAllFSAByDescriptionWith(string text) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM datafsa WHERE description like @description";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@description",
                    SqlDbType=SqlDbType.VarChar,
                    Value= "%"+text+"%"
                },
            };

            return db.recieveSQL(strSQL, parameters);
        }
    }
}