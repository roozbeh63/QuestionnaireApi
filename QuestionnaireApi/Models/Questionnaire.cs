using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_Questionnaire")]
    public class Questionnaire : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string App { get; set; }
    }
}