using Microsoft.AspNetCore.Mvc;
using QuestionnaireApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Services
{
    public class QuestionService : BaseService<Question>
    {
        public QuestionService(QAContext context)
            : base(context) { }
    }
}