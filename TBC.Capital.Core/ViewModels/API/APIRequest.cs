using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TBC.Capital.Core.ViewModels.APIRequest
{
    public class ContactParamsVM
    {
        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        [EmailAddress(ErrorMessage = "ელ. ფოსტა გასასწორებელია")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        [Phone(ErrorMessage = "ტელეფონის ნომერი გასასწორებელია")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Message { get; set; }
    }
    public class VacancyParamsVM
    {
        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        [EmailAddress(ErrorMessage = "ელ. ფოსტა გასასწორებელია")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Position { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "ველი სავალდებულოა")]
        public HttpPostedFile File { get; set; }

        public string FileName { get; set; }
    }
    public class SubscriberVM
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public bool IsSubscribedToResearches { get; set; }

        public int[] TagsId { get; set; }

        public int SubscriptionTime { get; set; }
    }
}
