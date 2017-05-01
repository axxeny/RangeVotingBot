using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeVotingBot.Models;
using Telegram.Bot;

namespace RangeVotingBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var telegramBotClient = new TelegramBotClient("345790125:AAEgm_MGus5jxJClUFtq7W3UbDLJXb6Q2x4");
            telegramBotClient.StartReceiving();

            var poll = new OpenPoll(0, 6);
        }
    }
}
