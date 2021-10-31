using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestionnaireApi.Models;
using QuestionnaireApi.Services;
using System.Data;
using System.Threading.Tasks;

namespace QuestionnaireApi.Controllers
{
    [ApiController]
    [Route("Questionnaire")]
    public class QuestionnaireController : BaseApiController<QuestionnaireService, Questionnaire>
    {
        private readonly ILogger<QuestionnaireController> _logger;

        private readonly QAContext dbContext;

        public QuestionnaireController(ILogger<QuestionnaireController> logger, QAContext dbContext, QuestionnaireService service)
            : base(service)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// returns all the questions in a questionnaire
        /// </summary>
        /// <param name="name">name of the questionnaire that is supposed to be loaded</param>
        /// <returns></returns>
        [HttpGet("GetQuestionOfQuestionnaire/{appName}")]
        public async Task<DataTable> GetQuestionOfQuestionnaire(string appName)
        {
            return await this.service.GetQuestionOfQuestionnaireAsync(appName);
        }
    }
}