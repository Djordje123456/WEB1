using Common.Enums;
using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class GroupWork
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public WorkType WorkType { get; set; }

        public int Time { get; set; }

        public DateTime PlanTime { get; set; }

        public int MaxVisitors { get; set; }

        public List<int> Visitors { get; set; }

        public bool Deleted { get; set; }

        public int FitnessCenterId { get; set; }
        public int CurrentNumberOfVisitors { get; set; }
        public int TrainerId { get; set; }

        public string ToTxt()
        {
            return Id.ToString() + Constants.Separator +
                Name.ToString() + Constants.Separator +
                WorkType.ToString() + Constants.Separator +
                FitnessCenterId.ToString() + Constants.Separator +
                Time.ToString() + Constants.Separator +
                PlanTime.ToString() + Constants.Separator +
                MaxVisitors.ToString() + Constants.Separator +
                TrainerId.ToString() + Constants.Separator +
                Deleted.ToString();
        }
    }
}
