namespace TasteHub.Infrastructure.Constants
{
    /// <summary>
    /// Constants for validation properties
    /// </summary>
    public static class ValidationConstants
    {
        public const int CategoryNameMaxLength = 60;
        public const int CategoryNameMinLength = 3;

        public const int RecipeTitleMaxLength = 100;
        public const int RecipeTitleMinLength = 4;

        public const int RecipeIngredientsMaxLength = 1000;
        public const int RecipeIngredientsMinLength = 5;

        public const int RecipeInstructionsMaxLength = 5000;
        public const int RecipeInstructionsMinLength = 10;

        public const int CommentContentMaxLength = 500;
        public const int CommentContentMinLength = 3;

        public const double RatingMaxValue = 5.00;
        public const double RatingMinValue = 0;

        public const int QueryDescriptionMaxLength = 1000;
        public const int QueryDescriptionMinLength = 5;

        public const int RoleNameMaxLength = 100;
        public const int RoleNameMinLength = 4;

        public const int MaxRecipesPerPage = 1;
    }
}
