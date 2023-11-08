using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class Comments
    {
        public static List<Comment> GetAll()
        {
            var comments = FileService.GetAllComments();

            return comments;
        }

        public static List<Comment> GetAllForVisitorByFitness(int visitorId, int fitnessId)
        {
            var comments = GetAll();

            return comments.Where(x => x.Approval && x.FitnessCenterId == fitnessId && x.VisitorId == visitorId).ToList();
        }

        public static object GetAllByOwner(int ownerId)
        {
            var owner = Users.GetById(ownerId);

            if (owner == null)
            {
                return null;
            }

            var fitnesses = FitnessCenters.GetByOwnerId(ownerId);

            if (fitnesses == null)
            {
                return null;
            }

            var comments = new List<Comment>();
            foreach (var fitness in fitnesses)
            {
                var temp = (List<Comment>)GetAllByFitnessForOwner(fitness.Id);
                comments.AddRange(temp);
            }

            return comments;
        }

        public static object GetAllByFitness(int id)
        {
            var comments = GetAll();

            return comments.Where(x => x.FitnessCenterId == id && x.Approval).ToList();
        }

        public static object GetAllByFitnessForOwner(int id)
        {
            var comments = GetAll();

            return comments.Where(x => x.FitnessCenterId == id).ToList();
        }

        public static async Task<object> UpdateApprovalAsync(int commentId, bool isApproved)
        {
            var comments = GetAll();
            var founded = comments.FirstOrDefault(x => x.Id == commentId);

            if (founded == null)
                return false;

            founded.Approval = isApproved;

            await FileService.UpdateComments(comments);

            return true;
        }

        public static async Task<object> CreateCommentAsync(Comment comment)
        {
            if (comment == null || comment.Content == null || comment.Grade == 0)
            {
                return null;
            }

            comment.Id = GenerateId();

            var comments = GetAll();

            comments.Add(comment);

            await FileService.UpdateComments(comments);

            return comment;
        }

        private static int GenerateId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }

    }
}
