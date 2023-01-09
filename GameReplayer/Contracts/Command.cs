using System;

namespace GameReplayer.Contracts
{
    public class Command
    {
        public Guid GameId { get; set; }

        public int SecondsElapsedInPeriod { get; set; }

        public int Result { get; set; }

        public int UpdatedLineOfScrimmage { get; set; }
    }
}