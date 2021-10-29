using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_Questionnaire")]
    public class Questionnaire : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}