namespace Common.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public int FitnessCenterId { get; set; }

        public string Content { get; set; }

        public int Grade { get; set; }

        public bool Approval { get; set; }

        public string ToTxt()
        {
            return Id.ToString() + Constants.Separator +
                VisitorId.ToString() + Constants.Separator +
                FitnessCenterId.ToString() + Constants.Separator +
                Content + Constants.Separator +
                Grade.ToString() + Constants.Separator +
                Approval.ToString();
        }
    }
}
