using Common.Entities;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class GroupWorks
    {
        public static List<GroupWork> GetAll()
        {
            var groupWorks = FileService.GetAllGroupWorks();

            return groupWorks.Where(x => !x.Deleted).ToList();
        }

        public static List<GroupWork> GetAllNewByFitness(int id)
        {
            var groupWorks = GetAll();

            var newGroupWorks = groupWorks.Where(x => x.PlanTime > DateTime.Now && x.FitnessCenterId == id).ToList();

            foreach (var x in newGroupWorks)
            {
                x.CurrentNumberOfVisitors = FileService.CountVisitors(x.Id);
            }

            return newGroupWorks;
        }

        public static List<GroupWork> GetAllOldByTrainer(int tranerId)
        {
            var groupWorks = GetAll();

            var newGroupWorks = groupWorks.Where(x => x.PlanTime < DateTime.Now && x.TrainerId == tranerId).ToList();

            foreach (var x in newGroupWorks)
            {
                x.CurrentNumberOfVisitors = FileService.CountVisitors(x.Id);
            }

            return newGroupWorks;
        }

        public static List<GroupWork> GetAllByTrainer(int tranerId)
        {
            var groupWorks = GetAll();

            var newGroupWorks = groupWorks.Where(x => x.TrainerId == tranerId).ToList();

            foreach (var x in newGroupWorks)
            {
                x.CurrentNumberOfVisitors = FileService.CountVisitors(x.Id);
            }

            return newGroupWorks;
        }

        public static List<GroupWork> GetAllByVisitor(int userId)
        {
            var groupWorks = GetAll();

            var groupWorksIdsByUser = FileService.GetAllGroupkUser().Where(x => x.UserId == userId).Select(x => x.GrupId).ToList();

            var newGroupWorks = groupWorks.Where(x => groupWorksIdsByUser.Contains(x.Id)).ToList();

            foreach (var x in newGroupWorks)
            {
                x.CurrentNumberOfVisitors = FileService.CountVisitors(x.Id);
            }

            return newGroupWorks;
        }

        public static object HistorySortBy(int userId, int direction, string type)
        {
            var groupWorks = GetAllByVisitor(userId);
            if (direction == 1)
            {
                if (type == "type")
                {
                    return groupWorks.OrderByDescending(x => x.WorkType).ToList();

                }
                if (type == "date")
                {
                    return groupWorks.OrderByDescending(x => x.PlanTime).ToList();


                }
                if (type == "name")
                {
                    return groupWorks.OrderByDescending(x => x.Name).ToList();
                }
            }
            if (type == "type")
            {
                return groupWorks.OrderBy(x => x.WorkType).ToList();
            }
            if (type == "date")
            {
                return groupWorks.OrderBy(x => x.PlanTime).ToList();

            }
            if (type == "name")
            {
                return groupWorks.OrderBy(x => x.Name).ToList();
            }

            return groupWorks;
        }

        public static object TrainerHistorySortBy(int trainerId, int direction, string type)
        {
            var groupWorks = GetAllOldByTrainer(trainerId);
            if (direction == 1)
            {
                if (type == "type")
                {
                    return groupWorks.OrderByDescending(x => x.WorkType).ToList();

                }
                if (type == "date")
                {
                    return groupWorks.OrderByDescending(x => x.PlanTime).ToList();


                }
                if (type == "name")
                {
                    return groupWorks.OrderByDescending(x => x.Name).ToList();
                }
            }
            if (type == "type")
            {
                return groupWorks.OrderBy(x => x.WorkType).ToList();
            }
            if (type == "date")
            {
                return groupWorks.OrderBy(x => x.PlanTime).ToList();

            }
            if (type == "name")
            {
                return groupWorks.OrderBy(x => x.Name).ToList();
            }

            return groupWorks;
        }

        public static object HistorySearchBy(int userId, string name, WorkType type, string fitnessCenter)
        {
            var groupWorks = GetAllByVisitor(userId);
            var fitnessCenterNames = new List<string>();
            foreach (var groupWork in groupWorks)
            {
                fitnessCenterNames.Add(FitnessCenters.GetById(groupWork.FitnessCenterId).Name);
            }
            var retVal = groupWorks.Where(x =>
                (name == null || x.Name.StartsWith(name)) &&
                (type == 0 || x.WorkType == type) &&
                (fitnessCenter == null || fitnessCenterNames.Any(y => y.StartsWith(fitnessCenter)))).ToList();

            return retVal;
        }

        public static object TrainerHistorySearchBy(int trainerId, string name, WorkType type, DateTime minTime, DateTime maxTime)
        {
            var groupWorks = GetAllByTrainer(trainerId);

            var retVal = groupWorks.Where(x =>
                (name == null || x.Name.StartsWith(name)) &&
                (type == 0 || x.WorkType == type) &&
                (minTime == new DateTime() || maxTime == new DateTime() || (minTime <= x.PlanTime && x.PlanTime <= maxTime)) &&
                x.PlanTime < DateTime.Now).ToList();

            return retVal;
        }

        internal static bool Exists(int groupWorkId)
        {
            var groupWorks = GetAll();

            return groupWorks.Any(x => x.Id == groupWorkId);
        }

        public static GroupWork GetById(int groupWorkId)
        {
            var groupWorks = GetAll();

            return groupWorks.FirstOrDefault(x => x.Id == groupWorkId);
        }

        public static async Task<bool> CreateAsync(int trainerId, GroupWork groupWork)
        {
            var trainer = Users.GetById(trainerId);
            if(trainer == null || trainer.Role != Role.Trainer)
            {
                return false;
            }

            var differenceInDates = groupWork.PlanTime - DateTime.Now;
            if (differenceInDates.Days < 3) {
                return false;
            }

            groupWork.PlanTime = new DateTime(year:groupWork.PlanTime.Year, month: groupWork.PlanTime.Month, day: groupWork.PlanTime.Day);
            groupWork.Id = GenerateId();
            groupWork.TrainerId = trainerId;
            groupWork.FitnessCenterId = trainer.FitnessCenterId;

            var groupWorks = GetAll();
            groupWorks.Add(groupWork);

            await FileService.UpdateGroupWorks(groupWorks);

            return true;
        }

        public static async Task<bool> UpdateAsync(GroupWork groupWork)
        {
            var groupWorks = GetAll();
            var founded = groupWorks.FirstOrDefault(x => x.Id == groupWork.Id);
            if (founded == null)
            {
                return false;
            }

            if(groupWork.Name != "")
                founded.Name = groupWork.Name;
            if(groupWork.MaxVisitors != 0 && groupWork.MaxVisitors > FileService.CountVisitors(founded.Id))
                founded.MaxVisitors = groupWork.MaxVisitors;
            if(groupWork.Time != 0)
                founded.Time = groupWork.Time;
            if (groupWork.WorkType != founded.WorkType)
                founded.WorkType = groupWork.WorkType;
            if (groupWork.PlanTime != new DateTime() && (groupWork.PlanTime - DateTime.Now).Days >= 3)
                founded.PlanTime = groupWork.PlanTime;

            await FileService.UpdateGroupWorks(groupWorks);

            return true;
        }

        public static async Task<bool> DeleteAsync(int groupWorkId)
        {
            var groupWorks = GetAll();

            var founded = groupWorks.FirstOrDefault(x => x.Id == groupWorkId);
            if (founded == null)
            {
                return false;
            }

            founded.CurrentNumberOfVisitors = FileService.CountVisitors(founded.Id);
            if(founded.CurrentNumberOfVisitors > 0)
            {
                return false;
            }

            founded.Deleted = true;
            await FileService.UpdateGroupWorks(groupWorks);

            return true;
        }

        private static int GenerateId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}
