namespace QuestionnaireApi.Services
{
    public class AnswerService : BaseService<QuestionnaireApi.Models.Answer>
    {
        public AnswerService(QAContext context)
            : base(context) { }
    }
}