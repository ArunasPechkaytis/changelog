using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonParse
{
    /// <summary>
    /// парсим json данные
    /// </summary>
    static class cs_parse
    {
        /// <summary>
        /// Так как json внутри HTML ответа,
        /// 'обризаем' лишнее
        /// </summary>
        /// <param name="content">HTML ответ</param>
        static public string getJsonFromHttp(string content)
        {
            string sSearchJson = "pullRequestJSON': ";
            int istart = content.IndexOf(sSearchJson) + sSearchJson.Length;
            int iLength = content.IndexOf(", 'canDelete':", istart) - istart;
            return content.Substring(istart, iLength);
        }

    }

}
