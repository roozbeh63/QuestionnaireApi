using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionnaireApi.Models
{
    [Table("_as_QA_Topic")]
    public class Topic : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}