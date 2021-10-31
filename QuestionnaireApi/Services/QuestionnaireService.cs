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
            return await this.context.RawSqlQueryAsync($"select * from {new Questionnaire().TableName}", new System.Collections.Generic.Dictionary<string, object>
            {
                { "app", appName }
            });
        }
    }
}