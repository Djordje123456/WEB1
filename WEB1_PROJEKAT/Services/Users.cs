using Common.Entities;
using Common.Enums;
using Common.Models.Txt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class Users
    {
        public static List<User> GetAll()
        {
            var users = FileService.GetAllUsers();

            return users.Where(x => !x.Deleted).ToList();
        }

        public static List<User> GetRegisteredVisitorsByGroupWork(int groupWorkId)
        {
            var userIds = FileService.GetAllGroupkUser().Where(x => x.GrupId == groupWorkId).Select(x => x.UserId).ToList();
            if(userIds.Count == 0)
            {
                return null;
            }

            var users = new List<User>();
            foreach (var userId in userIds)
            {
                users.Add(GetById(userId));
            }

            return users;
        }

        public static User Login(string username, string password)
        {
            return GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        public static async Task<bool> Update(int id, User user)
        {
            var users = GetAll();
            var founded = users.FirstOrDefault(x => x.Id == id);

            if (founded == null)
                return false;

            if(user.Name != null) founded.Name = user.Name;
            if(user.LastName != "") founded.LastName = user.LastName;
            if(user.Gender != founded.Gender && user.Gender != 0) founded.Gender = user.Gender;
            if(user.Email != "") founded.Email = user.Email;
            if(user.BornAt != new DateTime()) founded.BornAt = user.BornAt;

            await FileService.UpdateUsers(users);

            return true;
        }

        public static async Task<User> RegisterUser(User user)
        {
            var users = GetAll();
            if (users.Any(x => x.Username == user.Username))
            {
                return null;
            }
            else
            {
                user.Id = GenerateNextUserId();
                if(user.Role == 0)
                    user.Role = Common.Enums.Role.Visitor;
                users.Add(user);
                await FileService.UpdateUsers(users);

                return user;
            }
        }

        public static async Task<bool> AssigneUser(int id, int groupWorkId)
        {
            var users = GetAll();
            var founded = users.FirstOrDefault(x => x.Id == id);

            if (founded == null || founded.Role != Role.Visitor)
                return false;

            var groupWork = GroupWorks.GetById(groupWorkId);
            if (groupWork == null)
                return false;

            var groupUsers = FileService.GetAllGroupkUser();
            if(groupUsers.Any(x => x.GrupId == groupWorkId && x.UserId == id))
            {
                return false;
            }
            if (FileService.CountVisitors(groupWorkId) >= groupWork.MaxVisitors)
            {
                return false;
            }

            groupUsers.Add(new GroupUser()
            {
                UserId = id,
                GrupId = groupWorkId
            });

            await FileService.UpdateGroupUser(groupUsers);

            return true;
        }

        public static async Task<object> BlockUserAsync(int userId)
        {
            var users = GetAll();
            var founded = users.FirstOrDefault(x => x.Id == userId);

            if (founded == null)
                return false;

            founded.Deleted = true;

            await FileService.UpdateUsers(users);

            return true;
        }

        internal static List<User> GetTrainersByFitnessCenter(int fitnessId)
        {
            var users = GetAll();

            return users.Where(x => x.FitnessCenterId == fitnessId && x.Role == Common.Enums.Role.Trainer).ToList();
        }

        public static List<User> GetTrainersByOwner(int ownerId)
        {
            var owner = GetById(ownerId);

            if (owner == null)
            {
                return null;
            }

            var fitnesses = FitnessCenters.GetByOwnerId(ownerId);

            if(fitnesses == null)
            {
                return null;
            }

            var trainers = new List<User>();
            foreach(var fitness in fitnesses)
            {
                var temp = GetTrainersByFitnessCenter(fitness.Id);
                trainers.AddRange(temp);
            }

            return trainers;
        }

        public static List<GroupWork> GetHistoryOfWorkOuts(int id)
        {
            var groupUsers = FileService.GetAllGroupkUser();
            var groupKeys = groupUsers.Where(x => x.UserId == id).Select(x => x.GrupId).ToList();
            var groupWorks = new List<GroupWork>();
            foreach (var key in groupKeys)
            {
                var groupWork = GroupWorks.GetById(key);
                if (groupWork.PlanTime <= DateTime.Now)
                {
                    groupWorks.Add(groupWork);
                }
            }
            return groupWorks;
        }

        public static List<GroupWork> GetUserHistoryOfWorkOutsByFitness(int userId, int fitnessId)
        {
            var groupUsers = FileService.GetAllGroupkUser();
            var groupKeys = groupUsers.Where(x => x.UserId == userId).Select(x => x.GrupId).ToList();
            var groupWorks = new List<GroupWork>();
            foreach (var key in groupKeys)
            {
                var groupWork = GroupWorks.GetById(key);
                if (groupWork.FitnessCenterId == fitnessId)
                {
                    groupWorks.Add(groupWork);
                }
            }
            return groupWorks;
        }

        public static int GenerateNextUserId() => Math.Abs(Guid.NewGuid().GetHashCode());

        public static User GetById(int id)
        {
            var users = GetAll();
            return users.FirstOrDefault(x => x.Id == id);
        }

        public static bool Exists(int id)
        {
            var users = GetAll();
            return users.Any(x => x.Id == id);
        }
    }
}
