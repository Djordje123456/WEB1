namespace Common.Models.Txt
{
    public class GroupFitness
    {
        public int GrupId { get; set; }
        public int FitnessId { get; set; }

        public string ToTxt()
        {
            return GrupId.ToString() + Constants.Separator + FitnessId.ToString();
        }
    }
}
