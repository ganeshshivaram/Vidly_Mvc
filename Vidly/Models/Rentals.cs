using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Rentals
    {
        public int Id { get; set; }

        [Required]
        public Movie Movie { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public DateTime RentedDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}