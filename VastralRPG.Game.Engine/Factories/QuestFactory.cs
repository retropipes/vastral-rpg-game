using VastralRPG.Game.Engine.Models;

namespace VastralRPG.Game.Engine.Factories;

internal static class QuestFactory
{
    private static readonly IList<Quest> _quests = new List<Quest>();

    static QuestFactory()
    {
        // Declare the items need to complete the quest, and its reward items
        List<ItemQuantity> itemsToComplete = new();
        List<ItemQuantity> rewardItems = new();
        itemsToComplete.Add(new ItemQuantity { ItemId = 9001, Quantity = 5 });
        rewardItems.Add(new ItemQuantity { ItemId = 1002, Quantity = 1 });
        // Create the quest
        _quests.Add(new Quest
        {
            Id = 1,
            Name = "Clear the herb garden",
            Description = "Defeat the rabbits in the Herbalist's garden",
            ItemsToComplete = itemsToComplete,
            RewardGold = 25,
            RewardExperiencePoints = 10,
            RewardItems = rewardItems
        });
    }

    public static Quest GetQuestById(int id)
    {
        return _quests.First(quest => quest.Id == id);
    }
}