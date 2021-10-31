using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public class QuestionnaireService : BaseService<QuestionnaireApi.Models.Questionnaire>
    {
        public QuestionnaireService(QAContext context)
        : base(context) { }
    }
}