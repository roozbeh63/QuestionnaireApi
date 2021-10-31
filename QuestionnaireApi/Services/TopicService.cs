using QuestionnaireApi.Models;

namespace QuestionnaireApi.Services
{
    public class TopicService : BaseService<Topic>
    {
        public TopicService(QAContext context)
         : base(context) { }
    }
}