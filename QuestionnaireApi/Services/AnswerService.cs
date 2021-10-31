using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public class AnswerService : BaseService<QuestionnaireApi.Models.Answer>
    {
        public AnswerService(QAContext context)
            : base(context) { }
    }
}