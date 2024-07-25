using CookiesCookBookLectureSolution.Recipes;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

const FileFormat Format = FileFormat.JSON;
IStringsRepository stringsRepository;
if (Format == FileFormat.JSON) {
    stringsRepository = new StringsJSONRepository();
} else if (Format == FileFormat.txt) {
    stringsRepository = new StringsTextualRepository();
}

const string Filename = "recipes";
string filePath;
if (Format == FileFormat.JSON) {
    filePath = Filename + "json";
} else if (Format == FileFormat.txt) {
    filePath= Filename + "text";
}

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(new RecipesRepository(stringsRepository, ingredientsRegister), 
    new RecipesConsoleUserInteraction(ingredientsRegister));
cookiesRecipesApp.Run(filePath);

public class FileMetaData {
    public string Name { get; }
    public FileFormat Format { get; }
    public FileMetaData(string name, FileFormat format)
    {
        this.Name = name;
        this.Format = format;
    }

}

public enum FileFormat {
    JSON,
    txt
}

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

    public List<string> Read(string filePath) {
        if (File.Exists(filePath)) {
            var fileContents = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<string>>(fileContents);
        }
        return new List<string>();
    }

    public void Write(string filePath, List<string> strings) {
        File.WriteAllText(filePath, JsonSerializer.Serialize(strings));
    }
}