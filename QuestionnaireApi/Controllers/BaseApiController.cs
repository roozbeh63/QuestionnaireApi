using Microsoft.AspNetCore.Mvc;
using QuestionnaireApi.Models;
using QuestionnaireApi.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QuestionnaireApi.Controllers
{
    [ApiController]
    public class BaseApiController<TService, TModel> : ControllerBase where TService : BaseService<TModel> where TModel : BaseModel
    {
        /// <summary>
        /// The report service
        /// </summary>
        protected readonly TService service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        protected BaseApiController(TService service)
        {
            this.service = service;
        }

        /// <summary>
        /// HTTP request to get all T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        public virtual async Task<IEnumerable<TModel>> Get()
        {
            IEnumerable<TModel> result = await service.GetAsync();

            return result;
        }

        /// <summary>
        /// HTTP request to get a single T by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        public virtual async Task<TModel> Get(long id)
        {
            TModel result = await service.GetAsync(id);

            return result;
        }

        /// <summary>
        /// HTTP request to insert the supplied report task
        /// </summary>
        /// <param name="id">  The identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>Actionresult.</returns>
        [HttpPut("{id:long}")]
        public virtual async Task<IActionResult> Put(long id, TModel data)
        {
            if (id != data.ID)
            {
                return BadRequest("The ID in the url doesn't match the ID of the sent object.");
            }

            TModel saveResult = await service.SaveAsync(data);

            return StatusCode(200, saveResult);
        }

        /// <summary>
        /// HTTP request to update the existing record of the supplied T
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost()]
        public virtual async Task<TModel> Post(TModel data)
        {
            TModel saveResult = await service.SaveAsync(data);

            return saveResult;
        }

        /// <summary>
        /// HTTP request to delete the T with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            TModel getResult = await service.GetAsync(id);

            if (getResult == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, id);
            }

            TModel deleteResult = await service.DeleteAsync(id);

            return StatusCode((int)HttpStatusCode.OK, deleteResult);
        }
    }
}