using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using changelog.dal;

namespace changelog.Models
{
    public class ChangeLogModel
    {
        public IEnumerable<db_projects> _db_projects { get; set; }
        public IEnumerable<db_Author> _db_author { get; set; }
        public IEnumerable<db_reviewers> _db_reviewers { get; set; }
    }
}