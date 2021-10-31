using QuestionnaireApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public class TopicService : BaseService<Topic>
    {
        public TopicService(QAContext context)
         : base(context) { }
    }
}