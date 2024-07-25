using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesCookBookLectureSolution.Recipes.Ingredients;

namespace CookiesCookBookLectureSolution.Recipes
{
    public class Recipe {
        public IEnumerable<Ingredient> Ingredients { get; }
        public Recipe(IEnumerable<Ingredient>  ingredients)
        {
            this.Ingredients = ingredients;
        }

        public override string ToString() {
            var steps = new List<string>();
            foreach (var ingredient in Ingredients) {
                steps.Add($"{ingredient.Name}. {ingredient.PreparationInstructions}");
            }
            return string.Join(Environment.NewLine, steps);
        }
    }
}
