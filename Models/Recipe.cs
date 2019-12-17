using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 charecters")]
        public string Name { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public Ingredient? Ingredient { get; set; }
        public string Quantity { get; set; }
        public int Time { get; set; }
        public string Preperation { get; set; }
        public string PhotoPath { get; set; }
    }
}
