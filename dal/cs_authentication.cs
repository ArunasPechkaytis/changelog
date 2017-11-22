using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace changelog.dal
{
    /// <summary>
    /// Логины и пароли для доступа
    /// </summary>
    static public class cs_authentication
    {
        private static readonly string loginDataBase = "";
        private static readonly string passwordDataBase = "";

        private static readonly string loginWeb = "";
        private static readonly string passwordWeb = "";

        public static string ProjectName { set; get; }
        public static int PullMax { set; get; }
        public static string ProjectLink { set; get; }
        public static int id_Projects { set; get; }

        static public string getLoginDataBase()
        {
            return loginDataBase;
        }

        static public string getPasswordDataBase()
        {
            return passwordDataBase;
        }
        static public string getLoginWeb()
        {
            return loginWeb;
        }
        static public string getPasswordWeb()
        {
            return passwordWeb;
        }


    }
}
