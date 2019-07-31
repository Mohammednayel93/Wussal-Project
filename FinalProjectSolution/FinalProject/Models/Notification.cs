namespace FinalProject.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Notification")]
    public partial class Notification
    {
        public int Id { get; set; }

        public int? Fk_Order { get; set; }

        public bool? IsRead { get; set; }

        public int User_Id { get; set; }

        public int? Comment_Id { get; set; }

        public int? Like_Id { get; set; }

        public int CurrentUser { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual Like Like { get; set; }

        public virtual Order Order { get; set; }

        public virtual User User { get; set; }
    }
}
