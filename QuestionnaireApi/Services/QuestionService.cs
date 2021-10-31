using QuestionnaireApi.Models;

namespace QuestionnaireApi.Services
{
    public class QuestionService : BaseService<Question>
    {
        public QuestionService(QAContext context)
            : base(context) { }
    }
}