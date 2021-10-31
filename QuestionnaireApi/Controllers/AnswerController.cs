using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionnaireApi.Models;
using QuestionnaireApi.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionnaireApi.Controllers
{
    [ApiController]
    [Route("answer")]
    public class AnswerController : BaseApiController<AnswerService, Answer>
    {
        private readonly ILogger<QuestionController> _logger;

        private readonly DbContext dbContext;

        public AnswerController(ILogger<QuestionController> logger, DbContext dbContext, AnswerService service)
           : base(service)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
    }
}