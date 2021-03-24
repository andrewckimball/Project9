using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewHomework3.Models
{
    public class MovieModel
    {
        //Creating movie model, with each category and the corresponding attributes
        [Key]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "Please insert the category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter a movie title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please insert a valid year")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please input the director's name")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Please choose a rating")]
        public string Rating { get; set; }

        public bool? Edited { get; set; }
 
        public string LentTo { get; set; }

        [StringLength(25, ErrorMessage = "Limit notes to 25 characters long")]
        public string Notes { get; set; }
    }
}
