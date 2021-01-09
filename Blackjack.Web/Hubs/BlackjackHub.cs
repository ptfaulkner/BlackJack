using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Blackjack.Game;
using Microsoft.AspNetCore.SignalR;

namespace Blackjack.Web.Hubs
{
    public class BlackjackHub : Hub
    {
        private readonly GameManager _gameManager;

        public BlackjackHub(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public async Task JoinGame([NotNull] string playerName)
        { 
            string message = _gameManager.AddPlayer(playerName, Context.ConnectionId);

            await BroadcastGameStatus();
        }

        public async Task SendGameAction([NotNull] string message)
        {
            _gameManager.ProcessPlayerAction(message, Context.ConnectionId);
            await BroadcastGameStatus();
        }

        public override async Task OnDisconnectedAsync([AllowNull] Exception exception)
        {
            _gameManager.RemovePlayer(Context.ConnectionId);
            await BroadcastGameStatus(); 
            await base.OnDisconnectedAsync(exception);
        }

        private async Task BroadcastGameStatus()
        {
            foreach (PlayerManager pm in _gameManager.PlayerManagers)
            {
                var gameStatus = pm.GetCurrentPlayerDto(_gameManager.Game);
                await Clients.Client(pm.PlayerId).SendAsync("GameUpdate", gameStatus);
            }
        }
    }
}