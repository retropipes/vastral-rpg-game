using VastralRPG.Game.Engine.Models;
using VastralRPG.Game.Engine.ViewModels;
 
namespace VastralRPG.Game.Tests.Mocks
{
    class MockGameSession : GameSession
    {
        public MockGameSession()
        {
            this.CurrentPlayer = new Player
            {
                Name = "TestPlayer",
                CharacterClass = "TestClass",
                Level = 1,
                HitPoints = 8,
            };
        }
    }
}