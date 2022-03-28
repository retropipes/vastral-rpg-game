using VastralRPG.Game.Engine.Models;
using System;

namespace VastralRPG.Game.Engine.Actions;

public interface IAction
{
    DisplayMessage Execute(LivingEntity actor, LivingEntity target);
}
