using VastralRPG.Game.Engine.Factories;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.Services;

namespace VastralRPG.Game.Engine.ViewModels;

public class GameSession : IGameSession
{
    private readonly World _currentWorld;
    private readonly Battle _battle;
    private readonly int _maximumMessagesCount = 100;
    private readonly Dictionary<string, Action> _userInputActions = new();
    private readonly IDiceService _diceService = DiceService.Instance;

    public Player CurrentPlayer { get; private set; }

    public Location CurrentLocation { get; private set; }

    public Monster? CurrentMonster { get; private set; }

    public Trader? CurrentTrader { get; private set; }

    public bool HasMonster => CurrentMonster != null;

    public MovementUnit Movement { get; private set; }

    public IList<DisplayMessage> Messages { get; } = new List<DisplayMessage>();

    public GameSession(int maxMessageCount, IDiceService? diceService = null)
        : this()
    {
        _maximumMessagesCount = maxMessageCount;
        _diceService = diceService ?? DiceService.Instance;
    }

    public GameSession()
    {
        InitializeUserInputActions();
        _currentWorld = WorldFactory.CreateWorld();
        _battle = new Battle(
                () => OnLocationChanged(_currentWorld.GetHomeLocation()),  // Return to Player's home
                () => GetMonsterAtCurrentLocation(),  // Gets another monster
                _diceService);
        CurrentPlayer = new Player
        {
            Name = "RetroPipes",
            CharacterClass = "Fighter",
            CurrentHitPoints = 10,
            MaximumHitPoints = 10,
            Gold = 1000,
            ExperiencePoints = 0,
            Level = 1,
            Dexterity = _diceService.Roll(6, 3).Value,
            Strength = _diceService.Roll(6, 3).Value,
            ArmorClass = 10
        };
        Movement = new MovementUnit(_currentWorld);
        this.CurrentLocation = this.Movement.CurrentLocation;
        GetMonsterAtCurrentLocation();
        if (!CurrentPlayer.Inventory.Weapons.Any())
        {
            CurrentPlayer.Inventory.AddItem(ItemFactory.CreateGameItem(1001));
        }
        CurrentPlayer.Inventory.AddItem(ItemFactory.CreateGameItem(2001));
        CurrentPlayer.LearnRecipe(RecipeFactory.GetRecipeById(1));
        CurrentPlayer.Inventory.AddItem(ItemFactory.CreateGameItem(3001));
        CurrentPlayer.Inventory.AddItem(ItemFactory.CreateGameItem(3002));
        CurrentPlayer.Inventory.AddItem(ItemFactory.CreateGameItem(3003));
    }

    public void OnLocationChanged(Location newLocation)
    {
        _ = newLocation ?? throw new ArgumentNullException(nameof(newLocation));
        CurrentLocation = newLocation;
        Movement.UpdateLocation(CurrentLocation);
        GetMonsterAtCurrentLocation();
        CompleteQuestsAtLocation();
        GetQuestsAtLocation();
        CurrentTrader = CurrentLocation.TraderHere;
    }

    public void AttackCurrentMonster(GameItem? currentWeapon)
    {
        if (CurrentMonster != null)
        {
            CurrentPlayer.CurrentWeapon = currentWeapon;
            _battle.Attack(CurrentPlayer, CurrentMonster);
        }
    }

    public void ConsumeCurrentItem(GameItem? item)
    {
        if (item is null || item.Category != GameItem.ItemCategory.Consumable)
        {
            AddDisplayMessage("Item Warning", "You must select a consumable item to use.");
            return;
        }
        // player uses consumable item to heal themselves and item is removed from inventory.
        CurrentPlayer.CurrentConsumable = item;
        var message = CurrentPlayer.UseCurrentConsumable(CurrentPlayer);
        AddDisplayMessage(message);
    }

    public void CraftItemUsing(Recipe recipe)
    {
        _ = recipe ?? throw new ArgumentNullException(nameof(recipe));
        var lines = new List<string>();
        if (CurrentPlayer.Inventory.HasAllTheseItems(recipe.Ingredients))
        {
            CurrentPlayer.Inventory.RemoveItems(recipe.Ingredients);
            foreach (ItemQuantity itemQuantity in recipe.OutputItems)
            {
                for (int i = 0; i < itemQuantity.Quantity; i++)
                {
                    GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemId);
                    CurrentPlayer.Inventory.AddItem(outputItem);
                    lines.Add($"You craft 1 {outputItem.Name}");
                }
            }
            AddDisplayMessage("Item Creation", lines);
        }
        else
        {
            lines.Add("You do not have the required ingredients:");
            foreach (ItemQuantity itemQuantity in recipe.Ingredients)
            {
                lines.Add($"  {itemQuantity.Quantity} {ItemFactory.GetItemName(itemQuantity.ItemId)}");
            }
            AddDisplayMessage("Item Creation", lines);
        }
    }

    public void ProcessKeyPress(KeyProcessingEventArgs args)
    {
        _ = args ?? throw new ArgumentNullException(nameof(args));
        var key = args.Key.ToUpper();
        if (_userInputActions.TryGetValue(key, out Action? value))
        {
            value?.Invoke();
        }
    }

    private void GetMonsterAtCurrentLocation()
    {
        CurrentMonster = CurrentLocation.HasMonster() ? CurrentLocation.GetMonster() : null;
        if (CurrentMonster != null)
        {
            AddDisplayMessage("Monster Encountered:", $"You see a {CurrentMonster.Name} here!");
        }
    }

    private void GetQuestsAtLocation()
    {
        foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
        {
            if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.Id == quest.Id))
            {
                CurrentPlayer.Quests.Add(new QuestStatus(quest));
                AddDisplayMessage(quest.ToDisplayMessage());
            }
        }
    }

    private void CompleteQuestsAtLocation()
    {
        foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
        {
            QuestStatus? questToComplete =
                CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.Id == quest.Id && !q.IsCompleted);
            if (questToComplete != null)
            {
                if (CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemsToComplete))
                {
                    // Remove the quest completion items from the player's inventory
                    CurrentPlayer.Inventory.RemoveItems(quest.ItemsToComplete);
                    // give the player the quest rewards
                    var messageLines = new List<string>();
                    CurrentPlayer.AddExperience(quest.RewardExperiencePoints);
                    messageLines.Add($"You receive {quest.RewardExperiencePoints} experience points");
                    CurrentPlayer.ReceiveGold(quest.RewardGold);
                    messageLines.Add($"You receive {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        GameItem? rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemId);
                        if (rewardItem != null)
                        {
                            CurrentPlayer.Inventory.AddItem(rewardItem);
                            messageLines.Add($"You receive a {rewardItem.Name}");
                        }
                    }
                    AddDisplayMessage($"Quest Completed - {quest.Name}", messageLines);
                    // mark the quest as completed
                    questToComplete.IsCompleted = true;
                }
            }
        }
    }

    private void InitializeUserInputActions()
    {
        _userInputActions.Add("W", () => Movement.MoveNorth());
        _userInputActions.Add("A", () => Movement.MoveWest());
        _userInputActions.Add("S", () => Movement.MoveSouth());
        _userInputActions.Add("D", () => Movement.MoveEast());
        _userInputActions.Add("ARROWUP", () => Movement.MoveNorth());
        _userInputActions.Add("ARROWLEFT", () => Movement.MoveWest());
        _userInputActions.Add("ARROWDOWN", () => Movement.MoveSouth());
        _userInputActions.Add("ARROWRIGHT", () => Movement.MoveEast());
    }

    private void AddDisplayMessage(string title, string message) =>
        AddDisplayMessage(title, new List<string> { message });

    private void AddDisplayMessage(string title, IList<string> messages)
    {
        AddDisplayMessage(new DisplayMessage(title, messages));
    }

    public void AddDisplayMessage(DisplayMessage message)
    {
        this.Messages.Insert(0, message);
        if (Messages.Count > _maximumMessagesCount)
        {
            Messages.Remove(Messages.Last());
        }
    }
}