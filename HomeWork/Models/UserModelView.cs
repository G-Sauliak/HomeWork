using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace HomeWork.Models
{
    public class IndexViewModel
    {
        public int MaxShowUser { get; set; }
        public List<UserInfo> listusers { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int id { get; set; }
        public int TotalCountUser { get; set; }
        public int CurrentPage { get; set; }
        public int Counter { get; set; }
    }

    public class EditViewModel
    {
        /// public string JsonActionUrl { get; set; }
        public string RedirectUrl { get; set; }
        public SelectList listCountries { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        //MiddleName 
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        //LastName 
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Email 
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Password 
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //Age 
        [Required]
        [Range(18, 99)]
        [Display(Name = "Age")]
        public string Age { get; set; }

        //PhoneNumber 
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$", ErrorMessage = "Phone format is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //Country 
        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }
        //City 
        [Required]
        [Display(Name = "Citys")]
        public int City { get; set; }

        //Details 
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 5)]
        [Display(Name = "Detalis")]
        public string Detalis { get; set; }
    }
    public class RegisterViewModel
    {
        public string JsonActionUrl { get; set; }
        public string RedirectUrl { get; set; }
        public SelectList listCountry { get; set; }

        public RegisterViewModel()
        { }

        //Name 
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        //MiddleName 
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        //LastName 
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Email 
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //Password 
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //ConfirmPassword 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //Age 
        [Required]
        [Range(18, 99)]
        [Display(Name = "Age")]
        public string Age { get; set; }

        //PhoneNumber 
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$", ErrorMessage = "Phone format is not valid.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //Country 
        [Required]
        [Display(Name = "Country")]
        public int Country { get; set; }
        //City 
        [Required]
        [Display(Name = "Citys")]
        public int City { get; set; }

        //Details 
        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 2)]
        [Display(Name = "Detalis")]
        public string Detalis { get; set; }

    }
}