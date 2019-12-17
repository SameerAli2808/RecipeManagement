using RecipeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeManagement.ViewModels
{
    public class RecipeCreateViewModel
    {
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
        public List<IFormFile> Photos { get; set; }
    }
}
