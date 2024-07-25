namespace CookiesCookBookLectureSolution.Recipes.Ingredients
{
    public abstract class Spice : Ingredient
    {
        public override string PreparationInstructions => "Add 2 teaspoons." + base.PreparationInstructions;
    }
}
