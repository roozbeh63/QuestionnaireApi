using QuestionnaireApi.Helpers;
using QuestionnaireApi.Models;
using System.Data;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public class QuestionnaireService : BaseService<Questionnaire>
    {
        public QuestionnaireService(QAContext context)
        : base(context) { }

        public async Task<DataTable> GetQuestionOfQuestionnaireAsync(string appName)
        {
            return await this.context.RawSqlQueryAsync($@"SELECT * FROM {new Questionnaire().TableName} Q
LEFT JOIN {new Topic().TableName} T ON Q.ID = T.QuestionnaireId
LEFT JOIN {new Question().TableName} QQ ON T.ID = QQ.TopicId",
                new System.Collections.Generic.Dictionary<string, object>
            {
                { "app", appName }
            });
        }
    }
}