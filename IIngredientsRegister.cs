using CookiesCookBookLectureSolution.Recipes.Ingredients;

public interface IIngredientsRegister {
    IEnumerable<Ingredient> All { get; }

    Ingredient GetIngredientByID(int id);
}