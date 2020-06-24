using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class WikiArticle
    {
        [Key]
        public int ArticleId { get; set; }
        [NotMapped]
        public object[] Article { get; set; }
    }

}
