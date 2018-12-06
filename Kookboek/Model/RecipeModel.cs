using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kookboek.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Recipe name is required!")]
        public string Name { get; set; }
        [Range(1, 1000, ErrorMessage = "Preparation ime is required!")]
        [DisplayName("Preparation Time")]
        public int PrepTime { get; set; }
        [Range(1, 10000, ErrorMessage = "Number of calories is required!")]
        public int Calories { get; set; }
        [Required(ErrorMessage = "Ingredients is required!")]
        public List<string> Ingredients { get; set; }
  
        [Required(ErrorMessage = "Instructions is required!")]
        public string Instructions { get; set; }
        public double Rating { get; set; }
        public int TotalRatings { get; set; }
    }
}
