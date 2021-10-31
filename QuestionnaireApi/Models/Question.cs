using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionnaireApi.Models
{
    public enum QuestionType
    {
        Boolean = 1,
        Short_Answer = 2,
        Long_Answer = 3,
        Number = 4,
        Date = 5,
        Lookup = 6,
        Multi_Choice = 7,
        Linear_Scale = 8
    }

    [Table("_as_QA_Question")]
    public class Question : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public QuestionType Type { get; set; }
        public bool Enabled { get; set; }
        public long DependentQuestionId { get; set; }

        public long TopicId { get; set; }
    }
}