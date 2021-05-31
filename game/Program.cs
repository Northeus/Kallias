﻿using System;
using System.Threading.Tasks;
using Kallias.Bot;

namespace game
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var bot = new Bot(new Game());

            await bot.StartUp();
        }
    }
}