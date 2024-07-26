using CookiesCookBookLectureSolution.Recipes;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

const FileFormat Format = FileFormat.txt;
IStringsRepository stringsRepository;
if (Format == FileFormat.JSON) {
    stringsRepository = new StringsJSONRepository();
} else if (Format == FileFormat.txt) {
    stringsRepository = new StringsTextualRepository();
}

const string Filename = "recipes";
var fileMetaData = new FileMetaData(Filename, Format);


var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(new RecipesRepository(stringsRepository, ingredientsRegister), 
    new RecipesConsoleUserInteraction(ingredientsRegister));
cookiesRecipesApp.Run(fileMetaData.ToPath());

public class FileMetaData {
    public string Name { get; }
    public FileFormat Format { get; }
    public FileMetaData(string name, FileFormat format)
    {
        this.Name = name;
        this.Format = format;
    }

    public string ToPath() {

        return $"{Name}.{Format.AsFileExtension()}";
    }
}

public static class FileFormatExtensions {
    public static string AsFileExtension(this FileFormat fileformat) {
        if (fileformat == FileFormat.JSON) {
            return "json";
        } else if (fileformat == FileFormat.txt) {
            return "txt";
        } else return "";
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


public abstract class StringsRepository : IStringsRepository {

    public List<string> Read(string filePath) {
        if (File.Exists(filePath)) {
            var fileContents = File.ReadAllText(filePath);
            return TextToStrings(fileContents);
        }
        return new List<string>();
    }

    protected abstract List<string> TextToStrings(string fileContents);

    public void Write(string filePath, List<string> strings) {
        File.WriteAllText(filePath, StringsToText(strings));
    }

    protected abstract string StringsToText(List<string> strings);
}



public class StringsTextualRepository : StringsRepository {
    private static readonly string Separator = Environment.NewLine;

    protected override string StringsToText(List<string> strings) {
        return string.Join(Separator, strings);
    }

    protected override List<string> TextToStrings(string fileContents) {
        return fileContents.Split(Separator).ToList();
    }
}

public class StringsJSONRepository : StringsRepository {

    protected override string StringsToText(List<string> strings) {
        return JsonSerializer.Serialize(strings);
    }

    protected override List<string> TextToStrings(string fileContents) {
        return JsonSerializer.Deserialize<List<string>>(fileContents);
    }
}