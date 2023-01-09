using System;
using GameReplayer.Application;

namespace GameReplayer
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("NflLoadTester Main!");
            // var handler = new LoadTester();
            var handler = new Replayer();
            handler.Handle();
        }
    }
}