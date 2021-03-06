﻿using System.ComponentModel.DataAnnotations;

namespace H_04_EntityFramework_CodeFirst.Models
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
