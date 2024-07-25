using CookiesCookBookLectureSolution.Recipes;
using CookiesCookBookLectureSolution.Recipes.Ingredients;

public interface IRecipesUserInteraction {
    public void ShowMessage(string message);

    public void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}