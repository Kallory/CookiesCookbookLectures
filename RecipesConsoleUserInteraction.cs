using CookiesCookBookLectureSolution.Recipes;
using CookiesCookBookLectureSolution.Recipes.Ingredients;

public class RecipesConsoleUserInteraction : IRecipesUserInteraction {
    private readonly IIngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }
    public void ShowMessage(string message) {
        Console.WriteLine(message);
    }

    public void Exit() {
        Console.WriteLine("Press any key to close");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes) {
        if (allRecipes.Count() > 0) {
            Console.WriteLine("Existing Recipes: ");

            var counter = 1;
            foreach (var recipe in allRecipes) {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }
    }

    public void PromptToCreateRecipe() {
        Console.WriteLine("Create a new recipe!");
        Console.WriteLine("Available ingredients:");
        foreach (var ingredient in _ingredientsRegister.All) {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUser() {
        bool shallStop = false;
        var ingredients = new List<Ingredient>();

        while (!shallStop) {
            Console.WriteLine("Add an ingredient by its ID, or type 0 if finished");
            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int id)) {
                var selectedIngredient = _ingredientsRegister.GetIngredientByID(id);
                if (selectedIngredient != null) {
                    ingredients.Add(selectedIngredient);
                }
            } else {
                shallStop = true;
            }
        }

        return ingredients;
    }
}