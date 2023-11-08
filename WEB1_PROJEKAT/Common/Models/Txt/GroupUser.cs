namespace Common.Models.Txt
{
    public class GroupUser
    {
        public int GrupId { get; set; }
        public int UserId { get; set; }

        public string ToTxt()
        {
            return GrupId.ToString() + Constants.Separator + UserId.ToString();
        }
    }
}
