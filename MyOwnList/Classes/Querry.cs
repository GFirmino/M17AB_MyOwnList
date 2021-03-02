using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyOwnList.Classes {
    public class Querry {
        public string name;
        public string strSQL;
        public Querry(string nome_, string sql_) {
            name = nome_;
            strSQL = sql_;
        }
        public override string ToString() {
            return name;
        }

    }
}