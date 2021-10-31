using Microsoft.AspNetCore.Mvc;
using QuestionnaireApi.Models;
using QuestionnaireApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await new Questionnaire().CreateTableIfNotExists<Questionnaire>(dbContext);

            await new Question().CreateTableIfNotExists<Question>(dbContext);

            await new Answer().CreateTableIfNotExists<Answer>(dbContext);

            await new QuestionOnTopic().CreateTableIfNotExists<QuestionOnTopic>(dbContext);
        }
    }
}