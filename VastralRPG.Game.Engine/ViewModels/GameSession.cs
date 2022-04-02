using VastralRPG.Game.Engine.Factories;
using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.Services;

namespace VastralRPG.Game.Engine.ViewModels;

public class GameSession : IGameSession
{
    private readonly World _currentWorld;
    private readonly IDiceService _diceService;
    private readonly int _maximumMessagesCount = 100;

    public Player CurrentPlayer { get; private set; }

    public Location CurrentLocation { get; private set; }

    public Monster? CurrentMonster { get; private set; }

    public Trader? CurrentTrader { get; private set; }

    public bool HasMonster => CurrentMonster != null;

    public MovementUnit Movement { get; private set; }

    public IList<DisplayMessage> Messages { get; } = new List<DisplayMessage>();

    public GameSession(int maxMessageCount)
        : this()
    {
        _maximumMessagesCount = maxMessageCount;
    }

    public GameSession(IDiceService? diceService = null)
    {
        _diceService = diceService ?? DiceService.Instance;
        CurrentPlayer = new Player
        {
            Name = "RetroPipes",
            CharacterClass = "Fighter",
            CurrentHitPoints = 10,
            MaximumHitPoints = 10,
            Gold = 1000,
            ExperiencePoints = 0,
            Level = 1
        };
        _currentWorld = WorldFactory.CreateWorld();
        Movement = new MovementUnit(_currentWorld);
        this.CurrentLocation = this.Movement.CurrentLocation;
        GetMonsterAtCurrentLocation();
        if (!CurrentPlayer.Inventory.Weapons.Any())
        {
            var pointyStick = ItemFactory.CreateGameItem(1001);
            if (pointyStick != null)
            {
                CurrentPlayer.Inventory.AddItem(pointyStick);
            }
        }
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
        if (CurrentMonster is null)
        {
            return;
        }
        if (currentWeapon is null)
        {
            AddDisplayMessage("Combat Warning", "You must select a weapon, to attack.");
            return;
        }
        // player acts monster with weapon
        var message = currentWeapon.PerformAction(CurrentPlayer, CurrentMonster);
        Messages.Add(message);
        // If monster if killed, collect rewards and loot
        if (CurrentMonster.IsDead)
        {
            var messageLines = new List<string>();
            messageLines.Add($"You defeated the {CurrentMonster.Name}!");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);
            messageLines.Add($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);
            messageLines.Add($"You receive {CurrentMonster.Gold} gold.");
            foreach (GameItem item in CurrentMonster.Inventory.Items)
            {
                CurrentPlayer.Inventory.AddItem(item);
                messageLines.Add($"You received {item.Name}.");
            }
            AddDisplayMessage("Monster Defeated", messageLines);
            // Get another monster to fight
            GetMonsterAtCurrentLocation();
        }
        else
        {
            // If monster is still alive, let the monster attack
            int damageToPlayer = _diceService.Roll(CurrentMonster.DamageRoll).Value;
            if (damageToPlayer == 0)
            {
                AddDisplayMessage("Monster Combat", "The monster attacks, but misses you.");
            }
            else
            {
                CurrentPlayer.TakeDamage(damageToPlayer);
                AddDisplayMessage("Monster Combat", $"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
            }
            // If player is killed, move them back to their home.
            if (CurrentPlayer.IsDead)
            {
                AddDisplayMessage("Player Defeated", $"The {CurrentMonster.Name} killed you.");
                CurrentPlayer.CompletelyHeal();  // Completely heal the player
                this.OnLocationChanged(_currentWorld.LocationAt(0, -1));  // Return to Player's home
            }
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
                var messageLines = new List<string>
                    {
                        quest.Description,
                        "Items to complete the quest:"
                    };
                foreach (ItemQuantity q in quest.ItemsToComplete)
                {
                    var item = ItemFactory.CreateGameItem(q.ItemId);
                    if (item != null)
                    {
                        messageLines.Add($"{item.Name} (x{q.Quantity})");
                    }
                }
                messageLines.Add("Rewards for quest completion:");
                messageLines.Add($"   {quest.RewardExperiencePoints} experience points");
                messageLines.Add($"   {quest.RewardGold} gold");
                foreach (ItemQuantity itemQuantity in quest.RewardItems)
                {
                    var item = ItemFactory.CreateGameItem(itemQuantity.ItemId);
                    if (item != null)
                    {
                        messageLines.Add($"   {itemQuantity.Quantity} {item.Name} (x{itemQuantity.Quantity})");
                    }
                }
                AddDisplayMessage($"Quest Added - {quest.Name}", messageLines);
            }
        }
    }

    private void CompleteQuestsAtLocation()
    {
        foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
        {
            QuestStatus? questToComplete =
                CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.Id == quest.Id &&
                                                         !q.IsCompleted);
            if (questToComplete != null)
            {
                if (CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemsToComplete))
                {
                    // Remove the quest completion items from the player's inventory
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        for (int i = 0; i < itemQuantity.Quantity; i++)
                        {
                            CurrentPlayer.Inventory.RemoveItem(
                                CurrentPlayer.Inventory.Items.First(
                                    item => item.ItemTypeID == itemQuantity.ItemId));
                        }
                    }
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

    private void AddDisplayMessage(string title, string message) =>
        AddDisplayMessage(title, new List<string> { message });

    private void AddDisplayMessage(string title, IList<string> messages)
    {
        var message = new DisplayMessage(title, messages);
        this.Messages.Insert(0, message);
        if (Messages.Count > _maximumMessagesCount)
        {
            Messages.Remove(Messages.Last());
        }
    }
}