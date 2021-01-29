using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DWA_Assignment1.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class FamilyGroupViewModel
    {
        [Display(Name = "Group Id")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "The group name is required")]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "The parent email is required")]
        [Display(Name = "Parent Email")]
        public string ParentEmail { get; set; }

        [Required(ErrorMessage = "This child email is required")]
        [Display(Name = "Child 1 Email")]
        public string Child1Email { get; set; }

        [Display(Name = "Child 2 Email")]
        public string Child2Email { get; set; }

        [Display(Name = "Child 3 Email")]
        public string Child3Email { get; set; }

        [Display(Name = "Child 4 Email")]
        public string Child4Email { get; set; }

        [Display(Name = "Child 5 Email")]
        public string Child5Email { get; set; }

        [Required(ErrorMessage = "The phone number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class LaneViewModel : IValidatableObject
    {
        [Display(Name = "Lane Id")]
        public int LaneId { get; set; }

        [Required(ErrorMessage = "The Event Id is required")]
        [Display(Name = "Event Id")]
        public int? EventId { get; set; }

        [Required(ErrorMessage = "Lane Number is required")]
        [Display(Name = "Lane Number")]
        public int LaneNumber { get; set; }

        [Required(ErrorMessage = "Swimmer's email is required")]
        [Display(Name = "Swimmer's email")]
        public string SwimmerEmail { get; set; }

        [Display(Name = "Swimmer Time")]
        public string SwimmerTime { get; set; }

        [Required(ErrorMessage = "The Heat is required")]
        [Display(Name = "Heat")]
        public string Heat { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IRepository<Event> eveRP = new EventRepository();
            var manager = eveRP.CreateUserStore();

            var eve = eveRP.Find(EventId);

            if (eve == null)
            {
                yield return new ValidationResult("This event does not exist");
            }

            if (eve != null)
            {
                var swimmer = manager.FindByEmail(SwimmerEmail);

                if (swimmer == null)
                {
                    yield return new ValidationResult("No swimmer was found with this email");
                }

                if (swimmer != null)
                {
                    DateTime now = DateTime.Today;
                    int age = now.Year - swimmer.DateOfBirth.Year;

                    if (eve.AgeRange == "Junior" && age > 14)
                    {
                        yield return new ValidationResult("Swimmer is over 14");
                    }

                    if (eve.AgeRange == "Senior" && age < 15)
                    {
                        yield return new ValidationResult("Swimmer is under 15");
                    }

                    if (eve.AgeRange == "Senior" && age > 16)
                    {
                        yield return new ValidationResult("Swimmer is over 16");
                    }

                    if (eve.Gender == "Male" && swimmer.Gender == "Female")
                    {
                        yield return new ValidationResult("This event requires: " + eve.Gender + " participants. Swimmer is " + swimmer.Gender);
                    }

                    if (eve.Gender == "Female" && swimmer.Gender == "Male")
                    {
                        yield return new ValidationResult("This event requires: " + eve.Gender + " participants. Swimmer is " + swimmer.Gender);
                    }
                }
            }

            eveRP.Dispose();
        }
    }

    public class EventViewModel : IValidatableObject
    {
        [Display(Name = "Event Id")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "The Id of the meet this event is for is required")]
        [Display(Name = "Meet Id")]
        public int? MeetId { get; set; }

        [Required(ErrorMessage = "The age range is required")]
        [Display(Name = "Age Range")]
        public string AgeRange { get; set; }

        [Required(ErrorMessage = "The gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The distance is required")]
        [Display(Name = "Distance")]
        public string Distance { get; set; }

        [Required(ErrorMessage = "The stroke is required")]
        [Display(Name = "Stroke")]
        public string Stroke { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IRepository<Meet> meetRP = new MeetRepository();

            var meet = meetRP.Find(MeetId);

            if (meet == null)
            {
                yield return new ValidationResult("The meet Id " + MeetId + " is not a valid meet");
            }

            meetRP.Dispose();
        }
    }

    public class MeetViewModel : IValidatableObject
    {
        [Display(Name = "Meet Id")]
        public int MeetId { get; set; }

        [Required(ErrorMessage = "The name of the meet is required")]
        [Display(Name = "Meet Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The date of the meet is required")]
        [Display(Name = "Meet Date")]
        public string DateString { get; set; }

        [Required(ErrorMessage = "Provided date could not be parsed. Please try another format")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The venue of the meet is required")]
        [Display(Name = "Meet Venue")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "The pool length is required")]
        [Display(Name = "Pool Length")]
        public string PoolLength { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime d;
            if (!DateTime.TryParse(DateString, out d))
            {
                yield return new ValidationResult("Date format could not be parsed");
            }
            else
            {
                Date = d;
            }
        }
    }

    public class SearchViewModel : IValidatableObject
    {
        //Meet Search Attributes
        [Display(Name = "Meet Name")]
        public string MeetName { get; set; }

        [Display(Name = "Meet Venue Name")]
        public string MeetVenue { get; set; }

        [Display(Name = "Meet Start Date")]
        public string MeetStartDateString { get; set; }
        public DateTime? MeetStartDateDT { get; set; }

        [Display(Name = "Meet End Date")]
        public string MeetEndDateString { get; set; }
        public DateTime? MeetEndDateDT { get; set; }

        //Event Search Attributes
        [Display(Name = "Event Age Range")]
        public string EventAgeRange { get; set; }
        
        [Display(Name = "Event Gender")]
        public string EventGender { get; set; }

        [Display(Name = "Distance")]
        public string EventDistance { get; set; }

        [Display(Name = "Event Swim Stroke")]
        public string EventSwimStroke { get; set; }

        //Swimmer Search Attributes
        [Display(Name = "Swimmer Id")]
        public string SwimmerId { get; set; }

        [Display(Name = "Swimmer First Name")]
        public string SwimmerFirstName { get; set; }

        [Display(Name = "Swimmer Last Name")]
        public string SwimmerLastName { get; set; }

        [Display(Name = "Swimmer Date of Birth Start Date")]
        public string SwimmerDOBStartDateString { get; set; }
        public DateTime? SwimmerDOBStartDateDT { get; set; }

        [Display(Name = "Swimmer Date of Birth End Date")]
        public string SwimmerDOBEndDateString { get; set; }
        public DateTime? SwimmerDOBEndDateDT { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MeetStartDateString != null)
            {
                if (!DateTime.TryParse(MeetStartDateString, out DateTime tempStartDate))
                {
                    yield return new ValidationResult("Start date format could not be parsed");
                }
                else
                {
                    MeetStartDateDT = tempStartDate;
                }
            }

            if(MeetEndDateString != null)
            {
                if (!DateTime.TryParse(MeetEndDateString, out DateTime tempEndDate))
                {
                    yield return new ValidationResult("End date format could not be parsed");
                }
                else
                {
                    MeetEndDateDT = tempEndDate;
                }
            }

            if (MeetStartDateDT != null && MeetEndDateDT == null)
            {
                yield return new ValidationResult("Please enter an end date");
            }
            if(MeetEndDateDT != null && MeetStartDateDT == null)
            {
                yield return new ValidationResult("Please enter a start date");
            }

            if(SwimmerDOBStartDateString != null)
            {
                if (!DateTime.TryParse(SwimmerDOBStartDateString, out DateTime tempDOBStartDate))
                {
                    yield return new ValidationResult("Swimer DOB start date could not be parsed");
                }
                else
                {
                    SwimmerDOBStartDateDT = tempDOBStartDate;
                }
            }

            if(SwimmerDOBEndDateString != null)
            {
                if(!DateTime.TryParse(SwimmerDOBEndDateString, out DateTime tempDOBEndDate))
                {
                    yield return new ValidationResult("Swimmer DOB end date could not be parsed");
                }
                else
                {
                    SwimmerDOBEndDateDT = tempDOBEndDate;
                }
            }

            if(SwimmerDOBStartDateDT != null && SwimmerDOBEndDateDT == null)
            {
                yield return new ValidationResult("Please enter an end date");
            }
            if(SwimmerDOBEndDateDT != null && SwimmerDOBStartDateDT == null)
            {
                yield return new ValidationResult("Please enter a start date");
            }
        }
    }
}