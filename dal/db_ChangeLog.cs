using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace changelog.dal
{
    public class db_ChangeLog
    {

        public List<db_projects> getProjectsView(int id = 0, int pageIndex=0, int pageSize=2)
        {
            DataTable dt = db_database.RW_ProjectsView(id);
            dt.TableName = "getProjectsView";
            int i = 15;
            return (
                                               from _projects in dt.AsEnumerable()
                                               select new db_projects()
                                               {
                                                   id_Projects = Convert.ToInt32(_projects.ItemArray[0].ToString()),
                                                   ProjectName = _projects.ItemArray[1].ToString(),
                                                   id = Convert.ToInt32(_projects.ItemArray[2].ToString()),
                                                   version = Convert.ToInt32(_projects.ItemArray[3].ToString()),
                                                   title = _projects.ItemArray[4].ToString(),
                                                   description = _projects.ItemArray[5].ToString(),
                                                   state = _projects.ItemArray[6].ToString(),
                                                   open = Convert.ToBoolean(_projects.ItemArray[7].ToString()),
                                                   closed = Convert.ToBoolean(_projects.ItemArray[8].ToString()),
                                                   createDate = _projects.ItemArray[9].ToString(),
                                                   updatedDate = _projects.ItemArray[10].ToString(),
                                                   closedDate = _projects.ItemArray[11].ToString(),
                                                   locked = Convert.ToBoolean(_projects.ItemArray[12].ToString()),
                                                   links = _projects.ItemArray[13].ToString(),
                                                   descriptionAsHtml = _projects.ItemArray[14].ToString(),
                                                   AuthorRole = _projects.ItemArray[i].ToString(),
                                                   AuthorApproved = Convert.ToBoolean(_projects.ItemArray[i + 1].ToString()),
                                                   AuthorStatus = _projects.ItemArray[i + 2].ToString(),
                                                   AuthorName = _projects.ItemArray[i + 3].ToString(),
                                                   AuthorEmailAddress = _projects.ItemArray[i + 4].ToString(),
                                                   AuthorId = Convert.ToInt32(_projects.ItemArray[i + 5].ToString()),
                                                   AuthorDisplayName = _projects.ItemArray[i + 6].ToString(),
                                                   AuthorActive = Convert.ToBoolean(_projects.ItemArray[i + 7].ToString()),
                                                   AuthorSlug = _projects.ItemArray[i + 8].ToString(),
                                                   AuthorType = _projects.ItemArray[i + 9].ToString(),
                                                   AuthorLinks = _projects.ItemArray[i + 10].ToString(),
                                                   AuthorAvatarUrl = _projects.ItemArray[i + 11].ToString(),
                                                   Reviewers = getProjectsReviewersView(Convert.ToInt32(_projects.ItemArray[2].ToString()))
                                               }
                                            ).Skip(pageIndex * pageSize)
                                             .Take(pageSize)
                                             .ToList();




        }

        public List<db_Author> getProjectsAuthorView()
        {
            DataTable dt = db_database.RW_ProjectsViewAuthor();
            dt.TableName = "getProjectsAuthorView";
            int i = 0;
            return (
                                               from _projects in dt.AsEnumerable()
                                               select new db_Author()
                                               {
                                                   AuthorRole = _projects.ItemArray[i].ToString(),
                                                   AuthorApproved = Convert.ToBoolean(_projects.ItemArray[i + 1].ToString()),
                                                   AuthorStatus = _projects.ItemArray[i + 2].ToString(),
                                                   AuthorName = _projects.ItemArray[i + 3].ToString(),
                                                   AuthorEmailAddress = _projects.ItemArray[i + 4].ToString(),
                                                   AuthorId = Convert.ToInt32(_projects.ItemArray[i + 5].ToString()),
                                                   AuthorDisplayName = _projects.ItemArray[i + 6].ToString(),
                                                   AuthorActive = Convert.ToBoolean(_projects.ItemArray[i + 7].ToString()),
                                                   AuthorSlug = _projects.ItemArray[i + 8].ToString(),
                                                   AuthorType = _projects.ItemArray[i + 9].ToString(),
                                                   AuthorLinks = _projects.ItemArray[i + 10].ToString(),
                                                   AuthorAvatarUrl = _projects.ItemArray[i + 11].ToString()
                                               }
                                            ).ToList();


        }

        public List<db_reviewers> getProjectsReviewersView(int id = 0)
        {
            DataTable dt = db_database.RW_ProjectsViewReviewers(id);
            dt.TableName = "RW_ProjectsViewReviewers";
            int i = 0;
            return (
                                               from _projects in dt.AsEnumerable()
                                               select new db_reviewers()
                                               {
                                                   id_Projects = Convert.ToInt32(_projects.ItemArray[i].ToString()),
                                                   id = Convert.ToInt32(_projects.ItemArray[i + 1].ToString()),
                                                   ReviewersLastReviewedCommit = _projects.ItemArray[i+2].ToString(),
                                                   ReviewersRole = _projects.ItemArray[i+3].ToString(),
                                                   ReviewersApproved = Convert.ToBoolean(_projects.ItemArray[i + 4].ToString()),
                                                   ReviewersStatus = _projects.ItemArray[i + 5].ToString(),

                                                   AuthorName = _projects.ItemArray[i + 6].ToString(),
                                                   AuthorEmailAddress = _projects.ItemArray[i + 7].ToString(),
                                                   AuthorId = Convert.ToInt32(_projects.ItemArray[i + 8].ToString()),
                                                   AuthorDisplayName = _projects.ItemArray[i + 9].ToString(),
                                                   AuthorActive = Convert.ToBoolean(_projects.ItemArray[i + 10].ToString()),
                                                   AuthorSlug = _projects.ItemArray[i + 11].ToString(),
                                                   AuthorType = _projects.ItemArray[i + 12].ToString(),
                                                   AuthorLinks = _projects.ItemArray[i + 13].ToString(),
                                                   AuthorAvatarUrl = _projects.ItemArray[i + 14].ToString()
                                               }
                                            ).ToList();


        }

    }

    public class db_projects : db_Author
    {
        [Key]
        [DisplayName("Уникальный код записи")]
        public int id_Projects { get; set; }
        [DisplayName("Название проекта")]
        public string ProjectName { get; set; }
        [DisplayName("id pull")]
        public int id { get; set; } 
        [DisplayName("version")]
        public int version { get; set; }
        [DisplayName("Название")]
        public string title { get; set; }
        [DisplayName("Описание")]
        public string description { get; set; }
        [DisplayName("Статус")]
        public string state { get; set; }
        [DisplayName("Открыт")]
        public bool open { get; set; }
        [DisplayName("Открыт")]
        public bool closed { get; set; }
        [DisplayName("Дата создания")]
        public string createDate { get; set; }

        [DisplayName("Дата изменения")]
        public string updatedDate { get; set; }

        [DisplayName("Дата закрытия")]
        public string closedDate { get; set; }

        [DisplayName("Заблокирован")]
        public bool locked { get; set; }

        [DisplayName("Заблокирован")]
        public string links { get; set; }
        [DisplayName("Описание")]
        public string descriptionAsHtml { get; set; }
        public List<db_reviewers> Reviewers { get; set; }
    }

    public class db_Author
    {

        [DisplayName("Роль")]
        public string AuthorRole { get; set; }
        [DisplayName("Approved")]
        public bool AuthorApproved { get; set; }
        [DisplayName("Author Status")]
        public string AuthorStatus { get; set; }
        [DisplayName("Author Name")]
        public string AuthorName { get; set; }
        [DisplayName("Author EmailAddress")]
        public string AuthorEmailAddress { get; set; }
        [DisplayName("Author id")]
        public int AuthorId { get; set; }
        [DisplayName("Author DisplayName")]
        public string AuthorDisplayName { get; set; }
        [DisplayName("Author Active")]
        public bool AuthorActive { get; set; }
        [DisplayName("Author Slug")]
        public string AuthorSlug { get; set; }
        [DisplayName("Author Type")]
        public string AuthorType { get; set; }
        [DisplayName("Author Links")]
        public string AuthorLinks { get; set; }
        [DisplayName("Author AvatarUrl")]
        public string AuthorAvatarUrl { get; set; }  
    }

    public class db_reviewers : db_Author
    {
        [DisplayName("Ссылка на проект")]
        public int id_Projects { get; set; }
        [DisplayName("Ссылка на pull")]
        public int id { get; set; }
        [DisplayName("Reviewers LastReviewedCommit")]
        public string ReviewersLastReviewedCommit { get; set; }
        [DisplayName("Reviewers Role")]
        public string ReviewersRole { get; set; }
        [DisplayName("Reviewers Approved")]
        public bool ReviewersApproved { get; set; }
        [DisplayName("Reviewers status")]
        public string ReviewersStatus { get; set; }
    }    


}
