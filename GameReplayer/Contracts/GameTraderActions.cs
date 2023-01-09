using System;
using System.Collections.Generic;

namespace GameReplayer.Contracts
{
    public class GameTraderActions
    {
        public Guid GameId { get; set; }
        public List<Action> Actions { get; set; }
    }
}