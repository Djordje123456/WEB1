using Common.Entities;
using Services;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class FitnessCentersController : ApiController
    {
        [HttpGet]
        [Route("api/FitnessCentersController/GetAll")]
        public IHttpActionResult GetAll() => Ok(FitnessCenters.GetAll());
        //ucitaj grupne trenige za taj fitnes centar
        [HttpGet]
        [Route("api/FitnessCentersController/GetById")]
        public IHttpActionResult GetById(int id) => Ok(FitnessCenters.GetById(id));
        //Unos centra u formi za trenera
        [HttpGet]
        [Route("api/FitnessCentersController/GetByName")]
        public IHttpActionResult GetByName(string name) => Ok(FitnessCenters.GetByName(name));
        //Fitnes centri koje poseduje taj vlasnik
        [HttpGet]
        [Route("api/FitnessCentersController/GetByOwnerId")]
        public IHttpActionResult GetByOwnerId(int id) => Ok(FitnessCenters.GetByOwnerId(id));

        [HttpGet]
        [Route("api/FitnessCentersController/SearchBy")]
        public IHttpActionResult SearchBy(string name, string address, int? minYear, int? maxYear)
        {
            if (maxYear != null && minYear != null)
                if (minYear > maxYear)
                    return BadRequest();

            return Ok(FitnessCenters.SearchBy(name, address, minYear, maxYear));
        }

        // 0 - rastuce // 1 - opadajuce
        [HttpGet]
        [Route("api/FitnessCentersController/SortByName")]
        public IHttpActionResult SortByName(int direction) => Ok(FitnessCenters.SortByName(direction));

        [HttpGet]
        [Route("api/FitnessCentersController/SortByAddress")]
        public IHttpActionResult SortByAddress(int direction) => Ok(FitnessCenters.SortByAddress(direction));

        [HttpGet]
        [Route("api/FitnessCentersController/SortByYear")]
        public IHttpActionResult SortByYear(int direction) => Ok(FitnessCenters.SortByYear(direction));

        [HttpDelete]
        [Route("api/FitnessCentersController/Delete")]
        public IHttpActionResult Delete(int ownerId, int fitnessId) => Ok(FitnessCenters.DeleteAsync(fitnessId));

        [HttpPost]
        [Route("api/FitnessCentersController/Create")]
        public IHttpActionResult Create(int ownerId, [FromBody] FitnessCenter fitness) => Ok(FitnessCenters.CreateAsync(ownerId, fitness));

        [HttpPut]
        [Route("api/FitnessCentersController/Update")]
        public IHttpActionResult Update(int ownerId, [FromBody] FitnessCenter fitness) => Ok(FitnessCenters.UpdateAsync(fitness));

    }
}
