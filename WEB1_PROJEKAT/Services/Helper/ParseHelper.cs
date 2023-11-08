using Common;
using Common.Entities;
using Common.Enums;
using Common.Models.Txt;
using System;
using System.Linq;

namespace Services.Helper
{
    public static class ParseHelper
    {
        public static Comment CommentParse(string line)
        {
            var fields = line.Split(Constants.Separator);
            return new Comment()
            {
                Id = int.Parse(fields[0]),
                VisitorId = int.Parse(fields[1]),
                FitnessCenterId = int.Parse(fields[2]),
                Content = fields[3],
                Grade = int.Parse(fields[4]),
                Approval = bool.Parse(fields[5])
            };
        }

        public static User UserParse(string line)
        {
            var fields = line.Split(Constants.Separator);

            Gender gender = Gender.Male;
            if(fields[5] == "Female") gender = Gender.Female;

            Role role = Role.Visitor;
            if(fields[8] == "Owner") role = Role.Owner;
            else if(fields[8] == "Trainer") role = Role.Trainer;

            var newUser = new User()
            {
                Id = int.Parse(fields[0]),
                Username = fields[1],
                Password = fields[2],
                Name = fields[3],
                LastName = fields[4],
                Gender = gender,
                Email = fields[6],
                BornAt = ParseDate(fields[7]),
                Role = role,
                FitnessCenterId = int.Parse(fields[9]),
                Deleted = Boolean.Parse(fields[10])
            };

            return newUser;
        }

        public static GroupWork GroupWorkParse(string line)
        {
            var fields = line.Split(Constants.Separator);

            var workType = WorkType.Yoga;
            if (fields[2] == "Legs") workType = WorkType.Legs;

            var newGroupWork = new GroupWork()
            {
                Id = int.Parse(fields[0]),
                Name = fields[1],
                WorkType = workType,
                FitnessCenterId = int.Parse(fields[3]),
                Time = int.Parse(fields[4]),
                PlanTime = ParseDate(fields[5]),
                MaxVisitors = int.Parse(fields[6]),
                TrainerId = int.Parse(fields[7]),
                Deleted = Boolean.Parse(fields[8]),
                CurrentNumberOfVisitors = FileService.CountVisitors(int.Parse(fields[0]))
            };

            return newGroupWork;
        }

        public static FitnessCenter FitnessCenterParse(string line)
        {
            var fields = line.Split(Constants.Separator);
            return new FitnessCenter()
            {
                Id = int.Parse(fields[0]),
                Name = fields[1],
                Street = fields[2],
                StreetNumber = int.Parse(fields[3]),
                Town = fields[4],
                ZipCode = int.Parse(fields[5]),
                CreatedAt = int.Parse(fields[6]),
                OwnerId = int.Parse(fields[7]),
                MonthPrice = int.Parse(fields[8]),
                YearPrice = int.Parse(fields[9]),
                OneWorkPrice = int.Parse(fields[10]),
                OneGroupPrice = int.Parse(fields[11]),
                OnePersonalPrice = int.Parse(fields[12]),
                Deleted = bool.Parse(fields[13])
            };
        }

        public static GroupFitness GroupFitnessParse(string line)
        {
            var fields = line.Split(Constants.Separator);
            var newGroupFitness = new GroupFitness()
            {
                GrupId = int.Parse(fields[0]),
                FitnessId = int.Parse(fields[1])
            };

            return newGroupFitness;
        }

        public static GroupUser GroupUserParse(string line)
        {
            var fields = line.Split(Constants.Separator);
            var newGroupUser = new GroupUser()
            {
                GrupId = int.Parse(fields[0]),
                UserId = int.Parse(fields[1])
            };

            return newGroupUser;
        }

        public static DateTime ParseDate(string date)
        {
            DateTime dateTime;

            try
            {
                dateTime = DateTime.Parse(date);
            }
            catch (Exception)
            {
                var data = date.Split(new char[] { '.', ' ', ':', '/'}).ToList();
                dateTime = new DateTime(int.Parse(data[2]), int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5]));
            }

            return dateTime;
        }
    }
}