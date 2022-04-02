using System.Collections.Generic;

namespace VastralRPG.Game.Engine.Models;

public class DisplayMessage
{
    public DisplayMessage(string title, IList<string> messages)
    {
        Title = title;
        Messages = messages;
    }

    public DisplayMessage(string title, string message)
    {
        Title = title;
        Messages = new List<string> { message };
    }

    public string Title { get; } = string.Empty;

    public IList<string> Messages { get; }
}
