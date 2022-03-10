using System;
using System.ComponentModel.DataAnnotations;

namespace CommonLayer
{
    public class UserPostModel
    {
        [RegularExpression(@"^[A-Za-z]{3,}$",
        ErrorMessage = "Please enter a valid Full name")]
        public string FullName { get; set; }

        [RegularExpression(@"^[A-Z0-9a-z]{1,}([.#$^_-][A-Za-z0-9]+)?[@][A-Za-z]{2,}[.][A-Za-z]{2,3}([.][a-zA-Z]{2})?$",
        ErrorMessage = "Please check ur  Email Address")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        ErrorMessage = "Please enter a valid Password")]
        public string Password { get; set; }
        //public string cPassword { get; set; }

        [RegularExpression(@"^[6-9]{1}[0-9]{2,}$",
        ErrorMessage = "Please enter a valid Phone number")]
        public int MobileNo { get; set; }
    }
}
