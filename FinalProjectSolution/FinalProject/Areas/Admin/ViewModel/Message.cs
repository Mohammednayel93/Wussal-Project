using System.ComponentModel.DataAnnotations;

namespace FinalProject.Areas.Admin.ViewModel
{
    public class Message
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "*")]

        public string MessageContent { get; set; }

    }
}