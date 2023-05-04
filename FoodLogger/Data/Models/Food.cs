﻿using FoodLogger.Data.Enum;
using System.ComponentModel.DataAnnotations;
using static FoodLogger.Data.DataConstants;

namespace FoodLogger.Data.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Range(GramsMinValue, GramsMaxValue)]
        public double Grams { get; set; }

        [Range(CaloriesMinValue, CaloriesMaxValue)]
        public double Calories { get; set; }

        [Range(ProteinMinValue, ProteinMaxValue)]
        public double Protein { get; set; }

        [Range(CarbsMinValue, CarbsMaxValue)]
        public double Carbs { get; set; }

        [Range(FatMinValue, FatMaxValue)]
        public double Fat { get; set; }

        public int CategoryId { get; set; }

        public FoodCategory FoodCategory { get; set; }
    }
}
