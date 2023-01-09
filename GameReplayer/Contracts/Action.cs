using System;

namespace NflLoadTester.Contracts
{
    public class Action
    {
        public string ActionType { get; set; }
        public Command Command { get; set; }
        public DateTime? TimestampUtc { get; set; }
    }
}