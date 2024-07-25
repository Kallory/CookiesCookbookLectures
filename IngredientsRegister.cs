using CookiesCookBookLectureSolution.Recipes.Ingredients;
using System.ComponentModel.Design;

public class IngredientsRegister : IIngredientsRegister {
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient> {
        new Butter(),
        new Salt(),
        new Pepper()
    };

    public Ingredient GetIngredientByID(int id) {
        foreach (var ingredient in All) {
            if (ingredient.Id == id) return ingredient;
        }

        return null;
    }
}