using Common.Entities;
using Services;
using System.Web.Http;

namespace WebApplication4.Controllers
{
    public class CommentsController : ApiController
    {
        [HttpGet]
        [Route("api/CommentsController/GetAllByOwner")]
        public IHttpActionResult GetAllByOwner(int ownerId) => Ok(Comments.GetAllByOwner(ownerId));

        [HttpGet]
        [Route("api/CommentsController/GetAllByFitness")]
        public IHttpActionResult GetAllByFitness(int id) => Ok(Comments.GetAllByFitness(id));

        [HttpGet]
        [Route("api/CommentsController/GetAllByFitnessVisitor")]
        public IHttpActionResult GetAllByFitnessVisitor(int visitorId, int fitnessId) => Ok(Comments.GetAllForVisitorByFitness(visitorId, fitnessId));

        [HttpPost]
        [Route("api/CommentsController/LeaveComment")]
        public IHttpActionResult LeaveComment(Comment comment) => Ok(Comments.CreateCommentAsync(comment));
        
        [HttpPut]
        [Route("api/CommentsController/UpdateApproval")]
        public IHttpActionResult UpdateApproval(int userId, int commentId, bool isApproved) => Ok(Comments.UpdateApprovalAsync(commentId, isApproved));
    }
}