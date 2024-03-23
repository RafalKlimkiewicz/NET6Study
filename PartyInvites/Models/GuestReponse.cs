using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestReponse
    {
        [Required(ErrorMessage = "Please eneter you name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please spcify whether you'll attend")]
        public bool? WillAttend { get; set; }
    }
}