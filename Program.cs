using CookiesCookBookLectureSolution.Recipes;
using System.Net;
using System.Runtime.CompilerServices;

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(new RecipesRepository(new StringsTextualRepository(), ingredientsRegister), 
    new RecipesConsoleUserInteraction(ingredientsRegister));
cookiesRecipesApp.Run("recipes.txt");

public class CookiesRecipesApp {

    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;
    }
    public void Run(string filePath) {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);

        _recipesUserInteraction.PromptToCreateRecipe();
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Count() > 0) {
            var recipe = new Recipe(ingredients);
            allRecipes.Add(recipe);
            _recipesRepository.Write(filePath, allRecipes);

            _recipesUserInteraction.ShowMessage("Recipe added: ");
            _recipesUserInteraction.ShowMessage(recipe.ToString());
        } else {
            _recipesUserInteraction.ShowMessage("No ingredients selected, Recipe will not be saved.");
        }

        _recipesUserInteraction.Exit();
    }
}


public class StringsTextualRepository : IStringsRepository {
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filePath) {
        if (File.Exists(filePath)) {
            var fileContents = File.ReadAllText(filePath);
            return fileContents.Split(Separator).ToList();
        }
        return new List<string>();
    }

    public void Write(string filePath, List<string> strings) {
        File.WriteAllText(filePath, string.Join(Separator, strings));
    }
}

public class StringsJSONRepository : IStringsRepository {
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filePath) {
        if (File.Exists(filePath)) {
            var fileContents = File.ReadAllText(filePath);
            return fileContents.Split(Separator).ToList();
        }
        return new List<string>();
    }

    public void Write(string filePath, List<string> strings) {
        File.WriteAllText(filePath, string.Join(Separator, strings));
    }
}