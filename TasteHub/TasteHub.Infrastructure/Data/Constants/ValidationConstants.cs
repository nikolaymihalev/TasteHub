namespace TasteHub.Infrastructure.Data.Constants
{
    public static class ValidationConstants
    {
        public const int CategoryNameMaxLength = 60;
        public const int CategoryNameMinLength = 3;
        
        public const int RecipeTitleMaxLength = 100;
        public const int RecipeTitleMinLength = 4;
        
        public const int RecipeIngredientsMaxLength = 300;
        public const int RecipeIngredientsMinLength = 5;
        
        public const int RecipeInstructionsMaxLength = 1500;
        public const int RecipeInstructionsMinLength = 10;
        
        public const int CommentContentMaxLength = 500;
        public const int CommentContentMinLength = 3;
    }
}
