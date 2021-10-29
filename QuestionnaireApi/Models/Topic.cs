using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_Topic")]
    public class Topic : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}