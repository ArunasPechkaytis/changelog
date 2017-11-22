using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace changelog.dal
{
    static public class db_database
    {

        /// <summary>
        /// Сохраняем сообщение об ошибке
        /// </summary>
        /// <param name="sError">Сообщение об ошибке</param>
        /// <param name="sSP">Название класса и процедуры</param>
        static public void RW_ProjectLogSave(string sError, string sSP)
        {
            // пишем в базу или лог файл
        }

        /// <summary>
        /// Загружаем данные по названию проекта, если оно есть 
        /// </summary>
        /// <param name="sprojectname">Название проекта</param>
        static public DataTable RW_ProjectGet(string ProjectName)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("RW_ProjectGet", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add(new SqlParameter("@ProjectName", SqlDbType.VarChar, 1000)).Value = ProjectName;

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectGet");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Преобразовываем данные из формата json в xml
        /// </summary>
        /// <param name="json">Данные json формате</param>
        static public XmlDocument JsonToXML(string json)
        {

            // To convert JSON text contained in string json into an XML node
            XmlDocument doc = JsonConvert.DeserializeXmlNode(
                @"{
                   '?xml': {
                     '@version': '1.0',
                     '@standalone': 'no'
                  },
                   'root': {
                     'changelog': [" 
                                  + json 
                                  + @"
                                   ]
                                  }
                                }
                                "
                            );

            return doc;
        }

        /// <summary>
        /// Сохраняем сообщение об ошибке
        /// </summary>
        /// <param name="id_Projects">Сообщение об ошибке</param>
        /// <param name="ProjectData">Преобразованный json в xml</param>
        static public void RW_ProjectDataAdd(int id_Projects, XmlDocument ProjectData)
        {
            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new SqlCommand("RW_ProjectDataAdd", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add(new SqlParameter("@id_Projects", SqlDbType.Int)).Value = id_Projects;
                    sqlComm.Parameters.Add(new SqlParameter("@ProjectData", SqlDbType.Xml)).Value = new SqlXml(ProjectData.ToXDocument().CreateReader());

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectDataAdd");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }

            }
        }

        /// <summary>
        /// Обновляем PullMax у проекта
        /// </summary> 
        /// <param name="id_Projects">Сообщение об ошибке</param>
        /// <param name="ProjectData">Преобразованный json в xml</param>
        static public void RW_ProjectPullUpdate(int id_Projects, int PullMax)
        {
            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new SqlCommand("RW_ProjectPullUpdate", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.Add(new SqlParameter("@id_Projects", SqlDbType.Int)).Value = id_Projects;
                    sqlComm.Parameters.Add(new SqlParameter("@PullMax", SqlDbType.Int)).Value = PullMax;

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectPullUpdate");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }

            }
        }

        /// <summary>
        /// Загружаем данные по названию проекта, если оно есть 
        /// </summary>
        /// <param name="sprojectname">Название проекта</param>
        static public DataTable RW_ProjectsView(int id = 0)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("RW_ProjectsView", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectsView");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Отображение авторов логов 
        /// </summary> 
        static public DataTable RW_ProjectsViewAuthor()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("RW_ProjectsViewAuthor", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure; 

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectsViewAuthor");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Отображение аудиторов
        /// </summary> 
        static public DataTable RW_ProjectsViewReviewers(int id = 0)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(db_connect.cs()))
            {
                using (SqlCommand sqlComm = new System.Data.SqlClient.SqlCommand("RW_ProjectsViewReviewers", cn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

                    try
                    {
                        cn.Open();
                        sqlComm.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    catch (Exception ex)
                    {
                        RW_ProjectLogSave(ex.Message.ToString(), "RW_ProjectsViewReviewers");
                    }
                    finally
                    {
                        sqlComm.Connection.Close();
                        sqlComm.Connection.Dispose();
                        sqlComm.Dispose();

                        cn.Close();
                        cn.Dispose();
                    }
                }
            }
            return dt;
        }

    }


    public static class DocumentExtensions
    {
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }

}
