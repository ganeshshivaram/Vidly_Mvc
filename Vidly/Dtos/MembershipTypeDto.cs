﻿
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MembershipTypeDto
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}