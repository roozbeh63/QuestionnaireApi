using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireApi.Helpers;
using QuestionnaireApi.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuestionnaireApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private QAContext _qAContext;

        public HomeController(QAContext dbContext)
        {
            this._qAContext = dbContext;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Reset()
        {
            /** get the assembly file of the current controller */
            System.Reflection.Assembly assembly = typeof(HomeController).Assembly;
            /** get the file version */
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            /** convert to string */
            string version = fvi.FileVersion;
            /** create the initservice */
            DbHelper service = new DbHelper(this._qAContext);
            /** create the tables if dont' exist*/
            await service.CreateTableIfNotExists();

            /** show the version of the backend */
            string content = $@"
		<html>
			<body>
						<div>
						<h1 align='center'> Welcome to Questionnaire app</h1>
						<p align='center'> The backend version ({version}) has been successfully reset.</p>
						</div>

			</body>
		</html>";

            return new ContentResult()
            {
                Content = content,
                ContentType = "text/html"
            };
        }
    }
}