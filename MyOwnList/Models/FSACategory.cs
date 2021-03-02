using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class FSACategory {
        public int idcategory;
        public string name;
        public string description;
        public DateTime created;
        public DateTime lastupdate;
        Classes.DataBase db;

        public FSACategory(int idcategory, string name, string description, DateTime created, DateTime lastupdate) {
            this.idcategory = idcategory;
            this.name = name;
            this.description = description;
            this.created = created;
            this.lastupdate = lastupdate;
            this.db = new Classes.DataBase();
        }
        public FSACategory(string name, string description) {
            this.name = name;
            this.description = description;
            this.db = new Classes.DataBase();
        }
        public void add() {
            string sql = @"INSERT INTO categoriesfsa(name, description)
                            VALUES (@name, @description)";

            List<SqlParameter> parameters = new List<SqlParameter>(){
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
            string sql = @"UPDATE categoriesfsa SET name = @name, description = @description WHERE idcategory=@idcategory";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idcategory",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.idcategory
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void delete(int id) {
            Classes.DataBase db = new Classes.DataBase();
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idcategory",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
                },
            };
            string strSQL = $@"DELETE FROM categoriesfsa WHERE idcategory = @idcategory";
            db.runSQL(strSQL, parameters);
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
        static public DataTable ListAllCategoriesByDescriptionWith(string text) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM categoriesfsa WHERE description like @description";
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