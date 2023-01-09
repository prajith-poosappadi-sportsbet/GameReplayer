using System;
using System.IO;
using System.Linq;
using System.Threading;
using GameReplayer.Contracts;
using Newtonsoft.Json;
using GameReplayer.Helpers;

namespace GameReplayer.Application
{
    public class Replayer
    {
        public void Handle()
        {
            const string filePath = @"C:\source\Temp\NflLoadTester\NflLoadTester\NflLoadTester\Application\Dummy.json";
            using var r = new StreamReader(filePath);
            var json = r.ReadToEnd();
            var actions = JsonConvert.DeserializeObject<GameTraderActions>(json);

            // Sort the actions according to the timestamp in ascending order
            actions.Actions = actions.Actions.OrderBy(z => z.TimestampUtc).ToList();

            DateTime? prevTimestamp = null;
            foreach (var action in actions.Actions)
            {
                var command = action.Command;
                var actionType = action.ActionType;
                var timestampUtc = action.TimestampUtc;
                
                Console.WriteLine($"{actionType} is going to get fired");

                if (prevTimestamp.HasValue)
                {
                    var duration = timestampUtc - prevTimestamp;
                    if (duration != null)
                    {
                        var seconds = duration.Value.Seconds;
                        Console.WriteLine($"{seconds} sec from last action");
                        Thread.Sleep(seconds * 1000);
                    }
                }

                HttpHelper.PostJson($"/api/bir/{actionType}", GameHelper.BaseAddress, command);
                
                Console.WriteLine($"{actionType} is fired");

                prevTimestamp = timestampUtc;
            }
        }
    }
}