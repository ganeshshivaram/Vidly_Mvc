using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Validators
{
    public class Min18YearsForAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.BirthDate == null)
                return new ValidationResult("Please enter the birthdate");


            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be atleast 18 years old to have a membership");

        }
    }
}