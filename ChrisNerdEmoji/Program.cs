using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ChrisNerdEmoji
{
    public class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            // Discord shit
            _client = new DiscordSocketClient();
            _client.Log += Log;

            var token = "MTA3Nzc0MDk4MDQwMjQwOTU3Mw.GdtHNN.h9cm9USGtSuVNUNmTAKMGyj_NWKdsJ85R7ozsA";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += Client_Ready;
            _client.MessageReceived += OnMessageReceived;
            _client.SlashCommandExecuted += OnCommandRecieved;

            // just gotta have it
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task Client_Ready()
        {
            await _client.SetGameAsync("the game where you just mess with chris");
        }

        private async Task OnCommandRecieved(SocketSlashCommand socketSlashCommand)
        {
            switch (socketSlashCommand.CommandName)
            {
                case "StartServer":
                    await StartServer();
                    break;
            }
        }

        private async Task StartServer()
        {

        }




        private async Task OnMessageReceived(SocketMessage socketMessage)
        {
            Task.Run(async () => React(socketMessage));
        }

        private async Task React(SocketMessage socketMessage)
        {
            var messages = await socketMessage.Channel.GetMessagesAsync(100).FlattenAsync();

            foreach (var message in messages)
            {
                if (message.Author.Id == 454654913675395072)
                {
                    var reactions = await message.GetReactionUsersAsync(Emote.Parse("<:chris:1049359310167220306>"), 3).FlattenAsync();
                    if (reactions.Contains(_client.CurrentUser))
                        break;

                    await message.AddReactionAsync(Emote.Parse("<:chris:1049359310167220306>"));
                    await message.AddReactionAsync(new Emoji("☝️"));
                }
            }
        }
    }
}