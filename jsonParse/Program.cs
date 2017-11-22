using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using changelog.dal;
using System.Data;

namespace jsonParse
{
    class Program
    {
        /// <summary>
        /// Консольное приложение работает 3 режимах
        /// 1. Без параметров. Загрузка происходит всех проектов из таблицы RW_Projects. В которой должно быть ссылка на загрузку и последний загруженный пулл
        /// 2. С параметром названия проекта. Загрузка происходит одного проекта. В таблице RW_Projects указан последний загруженный пулл
        /// 3. С параметром названия проекта и номеро пулла, который необходимо загрузить
        /// </summary>
        /// <param name="args[0]">Название проекта</param>
        /// <param name="args[1]">Номер пулла для загрузки</param>
        static void Main(string[] args)
        {

            string sHTML = "";
            Int32 PullMax = 0;


            if (args.Length > 0) { cs_authentication.ProjectName = args[0]; }

            if (args.Length > 1)
            {
                Int32.TryParse(args[1], out PullMax);
                cs_authentication.PullMax = PullMax;
            }


            foreach (DataRowView row in db_database.RW_ProjectGet(cs_authentication.ProjectName).AsDataView())
            {
                cs_authentication.ProjectLink = row["ProjectLink"].ToString();
                // если в параметре запуска указан номер пулла то игнорируем данные в базе данных 
                if (PullMax == 0) { cs_authentication.PullMax = Convert.ToInt32(row["PullMax"].ToString()); }

                cs_authentication.ProjectName = row["ProjectName"].ToString();
                cs_authentication.id_Projects = Convert.ToInt32(row["id_Projects"].ToString());

                do
                {

                    sHTML = cs_parse.getJsonFromHttp(cs_webrequest.getJsonFromWebPage(cs_authentication.ProjectLink, cs_authentication.PullMax));
 
                    if (sHTML != "")
                    {
                        db_database.RW_ProjectDataAdd(cs_authentication.id_Projects, db_database.JsonToXML(sHTML));
                        db_database.RW_ProjectLogSave("Добавляем Pull под номером #" + cs_authentication.PullMax.ToString(), "Main");
                        // если в параметре запуска указан номер пулла то выходим из цикла
                        if (PullMax == 0)
                        {
                            cs_authentication.PullMax++;
                            db_database.RW_ProjectPullUpdate(cs_authentication.id_Projects, cs_authentication.PullMax);
                        }
                        else
                        {
                            sHTML = "";
                        }
                    }
                }
                while (sHTML != "");

            }


        }
    }
}
