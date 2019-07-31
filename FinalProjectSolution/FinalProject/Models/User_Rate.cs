namespace FinalProject.Models
{
    public partial class User_Rate
    {
        public int Id { get; set; }

        public string Rate { get; set; }

        public int User_Id { get; set; }
        public int ProductUser_Id { get; set; }

        public virtual User User { get; set; }
    }
}
