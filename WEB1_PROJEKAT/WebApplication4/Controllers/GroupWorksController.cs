using Common.Entities;
using Common.Enums;
using Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class GroupWorksController : ApiController
    {
        [HttpGet]
        [Route("api/GroupWorksController/GetById")]
        public IHttpActionResult GetById(int id) => Ok(GroupWorks.GetById(id));

        [HttpGet]
        [Route("api/GroupWorksController/GetAllNewByFitness")]
        public IHttpActionResult GetAllNewByFitness(int id) => Ok(GroupWorks.GetAllNewByFitness(id));

        [HttpGet]
        [Route("api/GroupWorksController/GetAllOldByTrainer")]
        public IHttpActionResult GetAllOldByTrainer(int trainerId) => Ok(GroupWorks.GetAllOldByTrainer(trainerId));

        [HttpGet]
        [Route("api/GroupWorksController/GetAllByTrainer")]
        public IHttpActionResult GetAllByTrainer(int trainerId) => Ok(GroupWorks.GetAllByTrainer(trainerId));

        [HttpGet]
        [Route("api/GroupWorksController/GetAllByVisitor")]
        public IHttpActionResult GetAllByVisitor(int userId) => Ok(GroupWorks.GetAllByVisitor(userId));

        [HttpGet]
        [Route("api/GroupWorksController/HistorySearchBy")]
        public IHttpActionResult HistorySearchBy(int userId, string name, WorkType type, string fitnessCenter)
        {
            return Ok(GroupWorks.HistorySearchBy(userId, name, type, fitnessCenter));
        }

        [HttpGet]
        [Route("api/GroupWorksController/TrainerHistorySearchBy")]
        public IHttpActionResult TrainerHistorySearchBy(int trainerId, string name, WorkType type, DateTime minTime, DateTime maxTime)
        {
            return Ok(GroupWorks.TrainerHistorySearchBy(trainerId, name, type, minTime, maxTime));
        }

        [HttpGet]
        [Route("api/GroupWorksController/HistorySortBy")]
        public IHttpActionResult HistorySortBy(int userId, int direction, string type)
        {
            return Ok(GroupWorks.HistorySortBy(userId, direction, type));
        }

        [HttpGet]
        [Route("api/GroupWorksController/TrainerHistorySortBy")]
        public IHttpActionResult TrainerHistorySortBy(int trainerId, int direction, string type)
        {
            return Ok(GroupWorks.TrainerHistorySortBy(trainerId, direction, type));
        }

        [HttpPost]
        [Route("api/GroupWorksController/Create")]
        public async Task<IHttpActionResult> CreateAsync(int trainerId, [FromBody] GroupWork groupWork)
        {
            if(!await GroupWorks.CreateAsync(trainerId, groupWork))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/GroupWorksController/Update")]
        public async Task<IHttpActionResult> UpdateAsync(int trainerId, [FromBody] GroupWork groupWork)
        {
            if(!await GroupWorks.UpdateAsync(groupWork))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/GroupWorksController/Delete")]
        public async Task<IHttpActionResult> Delete(int trainerId, int groupWorkId)
        {
            if(!await GroupWorks.DeleteAsync(groupWorkId))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}