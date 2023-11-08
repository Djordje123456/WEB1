using Common.Entities;
using Common.Models.Txt;
using Services.Helper;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class FileService
    {
        private static string rootPath = @"C:\Users\38162\Desktop\HTML&CSS\Najnoviji projekat\Najnoviji projekat\WebApplication4\";
        private static string commentPath = rootPath + "comments.txt";
        private static string userPath = rootPath + "users.txt";

        private static string groupWorkPath = rootPath + "groupWorks.txt";
        private static string fitnessCenterPath = rootPath + "fitnessCenters.txt";
        private static string groupUserPath = rootPath + "groupUsers.txt";

        public static int CountVisitors(int id)
        {
            var groupUsers = GetAllGroupkUser();

            return groupUsers.Count(x => x.GrupId == id);
        }

        private static string groupFitnesstPath = rootPath + "groupFitnesses.txt";

        public static List<Comment> GetAllComments()
        {
            List<string> data = File.ReadAllLines(commentPath).ToList();
            List<Comment> comments = new List<Comment>();

            foreach (string line in data)
            {
                Comment comment = ParseHelper.CommentParse(line);
                comments.Add(comment);
            }

            return comments;
        }

        public static List<User> GetAllUsers()
        {
            List<string> data = File.ReadAllLines(userPath).ToList();
            List<User> users = new List<User>();

            foreach (string line in data)
            {
                User user = ParseHelper.UserParse(line);
                users.Add(user);
            }

            return users;
        }

        public static List<GroupWork> GetAllGroupWorks()
        {
            List<string> data = File.ReadAllLines(groupWorkPath).ToList();
            List<GroupWork> groupWorks = new List<GroupWork>();

            foreach (string line in data)
            {
                GroupWork groupWork = ParseHelper.GroupWorkParse(line);
                groupWorks.Add(groupWork);
            }

            return groupWorks;
        }

        public static List<FitnessCenter> GetAllFitnessCenters()
        {
            List<string> data = File.ReadAllLines(fitnessCenterPath).ToList();
            List<FitnessCenter> fitnessCenters = new List<FitnessCenter>();

            foreach (string line in data)
            {
                FitnessCenter fitnessCenter = ParseHelper.FitnessCenterParse(line);
                fitnessCenters.Add(fitnessCenter);
            }

            return fitnessCenters;
        }

        public static List<GroupUser> GetAllGroupkUser()
        {
            List<string> data = File.ReadAllLines(groupUserPath).ToList();
            List<GroupUser> groupUsers = new List<GroupUser>();

            foreach (string line in data)
            {
                GroupUser groupUser = ParseHelper.GroupUserParse(line);
                groupUsers.Add(groupUser);
            }

            return groupUsers;
        }

        public static List<GroupFitness> GetAllGroupFitness()
        {
            List<string> data = File.ReadAllLines(fitnessCenterPath).ToList();
            List<GroupFitness> groupFitnesses = new List<GroupFitness>();

            foreach (string line in data)
            {
                GroupFitness groupFitness = ParseHelper.GroupFitnessParse(line);
                groupFitnesses.Add(groupFitness);
            }

            return groupFitnesses;
        }

        public static async Task UpdateUsers(List<User> users)
        {
            File.Delete(userPath);

            using (var file = new StreamWriter(userPath))
            {
                foreach (var user in users)
                    await file.WriteLineAsync(user.ToTxt());
            }
        }

        public static async Task UpdateComments(List<Comment> comments)
        {
            File.Delete(commentPath);

            using (var file = new StreamWriter(commentPath))
            {
                foreach (var comment in comments)
                    await file.WriteLineAsync(comment.ToTxt());
            }
        }

        public static async Task UpdateGroupWorks(List<GroupWork> groupWorks)
        {
            File.Delete(groupWorkPath);

            using (var file = new StreamWriter(groupWorkPath))
            {
                foreach (var groupWork in groupWorks)
                    await file.WriteLineAsync(groupWork.ToTxt());
            }
        }

        public static async Task UpdateFitnessCenters(List<FitnessCenter> fitnessCenters)
        {
            File.Delete(fitnessCenterPath);

            using (var file = new StreamWriter(fitnessCenterPath))
            {
                foreach (var fitnessCenter in fitnessCenters)
                    await file.WriteLineAsync(fitnessCenter.ToTxt());
            }
        }

        public static async Task UpdateGroupFitness(List<GroupFitness> groupFitnesses)
        {
            File.Delete(groupFitnesstPath);

            using (var file = new StreamWriter(groupFitnesstPath))
            {
                foreach (var groupFitness in groupFitnesses)
                    await file.WriteLineAsync(groupFitness.ToTxt());
            }
        }

        public static async Task UpdateGroupUser(List<GroupUser> groupUsers)
        {
            File.Delete(groupUserPath);

            using (var file = new StreamWriter(groupUserPath))
            {
                foreach (var groupUser in groupUsers)
                    await file.WriteLineAsync(groupUser.ToTxt());
            }
        }
    }
}
