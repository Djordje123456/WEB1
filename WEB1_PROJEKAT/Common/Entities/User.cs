using Common.Enums;
using System;
using System.Collections.Generic;

namespace Common.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public DateTime BornAt { get; set; }

        public Role Role { get; set; }

        public List<int> GroupWorks { get; set; }

        public int FitnessCenterId { get; set; }

        public List<int> FitnessCenters { get; set; }

        public bool Deleted { get; set; }

        public string ToTxt()
        {
            return Id.ToString() + Constants.Separator +
                Username.ToString() + Constants.Separator +
                Password.ToString() + Constants.Separator +
                Name.ToString() + Constants.Separator +
                LastName.ToString() + Constants.Separator +
                Gender.ToString() + Constants.Separator +
                Email.ToString() + Constants.Separator +
                BornAt.ToString() + Constants.Separator +
                Role.ToString() + Constants.Separator +
                FitnessCenterId.ToString() + Constants.Separator +
                Deleted.ToString();
        }
    }
}
