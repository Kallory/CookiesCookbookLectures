using CookiesCookBookLectureSolution.Recipes;

public interface IRecipesRepository {
    List<Recipe> Read(string filepath);
    void Write(string filepath, List<Recipe> strings);

}