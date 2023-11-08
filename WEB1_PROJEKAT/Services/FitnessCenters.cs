using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class FitnessCenters
    {
        public static List<FitnessCenter> GetAll()
        {
            var fitnessCenters = FileService.GetAllFitnessCenters();

            return fitnessCenters.Where(x => !x.Deleted).OrderBy(x => x.Name).ToList();
        }

        public static FitnessCenter GetById(int id)
        {
            var fitnessCenters = GetAll();

            return fitnessCenters.FirstOrDefault(x => x.Id == id);
        }

        public static int GetByName(string name)
        {
            var fitnessCenters = GetAll();

            var founded = fitnessCenters.FirstOrDefault(x => x.Name == name);
            if (founded != null)
                return founded.Id;
            return 0;
        }

        public static List<FitnessCenter> GetByOwnerId(int id)
        {
            var fitnessCenters = GetAll();

            return fitnessCenters.Where(x => x.OwnerId == id).ToList();
        }

        public static List<FitnessCenter> SearchBy(string name, string address, int? minYear, int? maxYear)
        {
            var fitnessCenters = GetAll();
            return fitnessCenters.Where(x =>
                (name == null || x.Name.StartsWith(name)) &&
                (address == null || x.GetAddress().StartsWith(address)) &&
                (minYear == null || x.CreatedAt >= minYear) &&
                (maxYear == null || x.CreatedAt <= maxYear)).ToList();
        }

        // 0 - rastuce // 1 - opadajuce

        public static List<FitnessCenter> SortByName(int direction)
        {
            var fitnessCenters = GetAll();
            if (direction == 1)
            {
                return fitnessCenters.OrderByDescending(x => x.Name).ToList();
            }

            return fitnessCenters.OrderBy(x => x.Name).ToList();
        }

        public static async Task<bool> UpdateAsync(FitnessCenter fitness)
        {
            var fitnesses = GetAll();
            var founded = fitnesses.FirstOrDefault(x => x.Id == fitness.Id);

            if (founded == null)
                return false;

            if(fitness.Name != "")
                founded.Name = fitness.Name;

            if(fitness.Street != "")
                founded.Street = fitness.Street;

            if(fitness.StreetNumber != 0)
                founded.StreetNumber = fitness.StreetNumber;

            if(fitness.Town != "")
                founded.Town = fitness.Town;

            if(fitness.ZipCode != 0)
                founded.ZipCode = fitness.ZipCode;

            if(fitness.CreatedAt != 0)
                founded.CreatedAt = fitness.CreatedAt;

            if(fitness.MonthPrice != 0)
                founded.MonthPrice = fitness.MonthPrice;

            if(fitness.YearPrice != 0)
                founded.YearPrice = fitness.YearPrice;

            if(fitness.OneWorkPrice != 0)
                founded.OneWorkPrice = fitness.OneWorkPrice;

            if(fitness.OneGroupPrice != 0)
                founded.OneGroupPrice = fitness.OneGroupPrice;

            if(fitness.OnePersonalPrice != 0)
                founded.OnePersonalPrice = fitness.OnePersonalPrice;


            await FileService.UpdateFitnessCenters(fitnesses);

            return true;
        }

        public static async Task<FitnessCenter> CreateAsync(int ownerId, FitnessCenter fitness)
        {
            var fitnesses = GetAll();

            fitness.Id = GenerateId();
            fitness.OwnerId = ownerId;

            fitnesses.Add(fitness);

            await FileService.UpdateFitnessCenters(fitnesses);

            return fitness;
        }

        public static async Task<object> DeleteAsync(int fitnessId)
        {
            var fitnesses = GetAll();
            var founded = fitnesses.FirstOrDefault(x => x.Id == fitnessId);

            if (founded == null)
                return false;

            var groupWorks = GroupWorks.GetAllNewByFitness(fitnessId);

            if (groupWorks.Count != 0)
            {
                return false;
            }

            var usersToDelete = Users.GetTrainersByFitnessCenter(fitnessId);

            foreach (var user in usersToDelete)
            {
                await Users.BlockUserAsync(user.Id);
            }

            founded.Deleted = true;

            await FileService.UpdateFitnessCenters(fitnesses);

            return true;
        }

        public static List<FitnessCenter> SortByAddress(int direction)
        {
            var fitnessCenters = GetAll();
            if (direction == 1)
            {
                return fitnessCenters.OrderByDescending(x => x.GetAddress()).ToList();
            }

            return fitnessCenters.OrderBy(x => x.GetAddress()).ToList();
        }

        public static List<FitnessCenter> SortByYear(int direction)
        {
            var fitnessCenters = GetAll();
            if (direction == 1)
            {
                return fitnessCenters.OrderByDescending(x => x.CreatedAt).ToList();
            }

            return fitnessCenters.OrderBy(x => x.CreatedAt).ToList();
        }

        private static int GenerateId()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }
    }
}
