using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyOwnList.Models {
    public class ShopCart {
        public int idcart;
        public int iduser;
        public int idfsa;
        public int typefsa;
        public int quantity;
        public DateTime created;
        Classes.DataBase db;

        public ShopCart(int idcart, int iduser, int idfsa, int typefsa, int quantity, DateTime created) {
            this.idcart = idcart;
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.quantity = quantity;
            this.created = created;
        }

        public ShopCart(int iduser, int idfsa, int typefsa, int quantity) {
            this.iduser = iduser;
            this.idfsa = idfsa;
            this.typefsa = typefsa;
            this.quantity = quantity;
        }
        public void add() {
            string sql = @"INSERT INTO shop_cart(iduser, idfsa, typefsa, quantity)
                            VALUES (@iduser, @idfsa, @typefsa, @quantity)";

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
                ParameterName = "@quantity",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = this.quantity
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void update(int iduser, int quantity) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"UPDATE shop_cart SET quantity=@quantity WHERE iduser=@iduser";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@iduser",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = iduser
                },
                new SqlParameter() {
                ParameterName = "@quantity",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = quantity
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void delete(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"DELETE FROM shop_cart WHERE idcart=@idcart";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@idcart",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
                },
            };
            db.runSQL(sql, parameters);
        }
        static public void deleteAllFromUser(int iduser) {
            Classes.DataBase db = new Classes.DataBase();
            string sql = @"DELETE FROM shop_cart WHERE iduser=@iduser";
            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                ParameterName = "@iduser",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = iduser
                },
            };
            db.runSQL(sql, parameters);
        }
        static public DataTable recieveShopCartByID(int id) {
            Classes.DataBase db = new Classes.DataBase();

            string strSQL = "SELECT * FROM shop_cart WHERE idcart=@idcart";

            List<SqlParameter> parameters = new List<SqlParameter>(){
                new SqlParameter() {
                    ParameterName="@idcart",
                    SqlDbType=SqlDbType.Int,
                    Value=id
                },
            };
            DataTable data = db.recieveSQL(strSQL, parameters);
            return data;
        }
        static public DataTable ListShopCartByUserID(int id) {
            Classes.DataBase db = new Classes.DataBase();
            string strSQL = "SELECT * FROM shop_cart WHERE iduser=@iduser";
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