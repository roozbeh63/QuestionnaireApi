using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuestionnaireApi.Models;
using QuestionnaireApi.Services;

namespace QuestionnaireApi.Controllers
{
    [ApiController]
    [Route("question")]
    public class QuestionController : BaseApiController<QuestionService, Question>
    {
        private readonly ILogger<QuestionController> _logger;

        private readonly DbContext dbContext;

        public QuestionController(ILogger<QuestionController> logger, QAContext dbContext, QuestionService service)
            : base(service)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
    }
}