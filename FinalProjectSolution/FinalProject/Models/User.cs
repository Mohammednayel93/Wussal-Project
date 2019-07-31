namespace FinalProject.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Comments = new HashSet<Comment>();
            Contacts = new HashSet<Contact>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            User_Rate = new HashSet<User_Rate>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]

        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "*")]

        [StringLength(200)]
        public string Image { get; set; }

        [Required(ErrorMessage = "*")]

        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]

        [StringLength(50)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Gender { get; set; }

        public bool? Status { get; set; }

        public int Role_Id { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        //[Phone(ErrorMessage = "Invalid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01[0-5][0-9]{8}$", ErrorMessage = "Invalid Phone Number")]

        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contact> Contacts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rate> User_Rate { get; set; }
        [StringLength(200)]
        public string Bio { get; set; }


        //[NotMapped]
        //[Required(ErrorMessage = "*")]
        //[StringLength(50)]

        //public string OldPassword { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "*")]
        //[StringLength(50)]

        //public string NewPassword { get; set; }

        //[NotMapped]
        //[Required(ErrorMessage = "*")]
        //[StringLength(50)]
        //[Compare("NewPassword", ErrorMessage = "Password Not Match")]
        //public string ConfirmNewPassword { get; set; }

    }
}

