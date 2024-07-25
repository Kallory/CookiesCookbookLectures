
using CookiesCookBookLectureSolution.Recipes;

public interface IStringsRepository {

    List<string> Read(string filepath);
    void Write(string filepath, List<String> strings);
    //string StringsToText(List<string> strings);
    //List<string> TextToStrings(string fileContents);
}