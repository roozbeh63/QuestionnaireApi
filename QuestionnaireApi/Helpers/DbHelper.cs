using QuestionnaireApi.Models;
using QuestionnaireApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuestionnaireApi.Helpers
{
    public class DbHelper
    {
        private QAContext dbContext;

        public DbHelper(QAContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateTableIfNotExists()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(new Questionnaire().CreateTableIfNotExists<Questionnaire>(dbContext));
            tasks.Add(new Question().CreateTableIfNotExists<Question>(dbContext));
            tasks.Add(new Answer().CreateTableIfNotExists<Answer>(dbContext));
            tasks.Add(new Topic().CreateTableIfNotExists<Topic>(dbContext));

            await Task.WhenAll(tasks);
        }
    }
}