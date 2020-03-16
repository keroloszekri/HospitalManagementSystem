using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace ADB.DAL
{
    class DAL
    {
      public static  MySqlConnection con = new MySqlConnection("server=localhost;database=clinic_sheet;uid=root;pwd=rootroot");
        public static MySqlCommand cmd;
    }
}
