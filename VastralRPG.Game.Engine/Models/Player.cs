namespace VastralRPG.Game.Engine.Models;

public class Player : LivingEntity
{
    public string CharacterClass { get; set; } = string.Empty;

    public int ExperiencePoints { get; set; }

    public IList<QuestStatus> Quests { get; set; } = new List<QuestStatus>();

    public IList<Recipe> Recipes { get; set; } = new List<Recipe>();

    public void AddExperience(int experiencePoints)
    {
        if (experiencePoints > 0)
        {
            ExperiencePoints += experiencePoints;
            SetLevelAndMaximumHitPoints();
        }
    }

    private void SetLevelAndMaximumHitPoints()
    {
        int originalLevel = Level;

        Level = (ExperiencePoints / 100) + 1;

        if (Level != originalLevel)
        {
            MaximumHitPoints = Level * 10;
        }
    }

    public void LearnRecipe(Recipe recipe)
    {
        if (!Recipes.Any(r => r.Id == recipe.Id))
        {
            Recipes.Add(recipe);
        }
    }
}