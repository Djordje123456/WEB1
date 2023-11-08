namespace Common.Entities
{
    public class FitnessCenter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string Town { get; set; }

        public int ZipCode { get; set; }

        public int CreatedAt { get; set; }

        public int OwnerId { get; set; }

        public int MonthPrice { get; set; }

        public int YearPrice { get; set; }

        public int OneWorkPrice { get; set; }

        public int OneGroupPrice { get; set; }

        public int OnePersonalPrice { get; set; }

        public bool Deleted { get; set; }

        public string ToTxt()
        {
            return Id.ToString() + Constants.Separator +
                Name.ToString() + Constants.Separator +
                Street.ToString() + Constants.Separator +
                StreetNumber.ToString() + Constants.Separator +
                Town.ToString() + Constants.Separator +
                ZipCode.ToString() + Constants.Separator +
                CreatedAt.ToString() + Constants.Separator +
                OwnerId.ToString() + Constants.Separator +
                MonthPrice.ToString() + Constants.Separator +
                YearPrice.ToString() + Constants.Separator +
                OneWorkPrice.ToString() + Constants.Separator +
                OneGroupPrice.ToString() + Constants.Separator +
                OnePersonalPrice.ToString() + Constants.Separator +
                Deleted.ToString();
        }

        public string GetAddress() => Street + " " + StreetNumber + " " + Town + " " + ZipCode;
    }
}
