using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace changelog.dal
{
    static class db_connect
    {
        /// <summary>
        /// строка подключения
        /// </summary>
        static public string cs()
        {
            string _cs = null;
            _cs = "Password={0};Persist Security Info=True;User ID={1};Initial Catalog=changelog;Data Source=localhost;Application Name=tempdb;Workstation ID=Тестовая задание";
            _cs = String.Format(_cs, cs_authentication.getPasswordDataBase(), cs_authentication.getLoginDataBase());
            return _cs;
        }

    }
}
