using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace Blackjack.Web.Controllers
{
    [RoutePrefix("api/blackjack")]
    public class BlackjackController : ApiController
    {
        [HttpGet, Route]
        public HttpResponseMessage Get()
        {
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest ||
                currentContext.IsWebSocketRequestUpgrading)
            {
                currentContext.AcceptWebSocketRequest(ProcessWebsocketSession);
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Need a websocket connection.");
        }

        private async Task ProcessWebsocketSession(AspNetWebSocketContext context)
        {
            WebSocket ws = context.WebSocket;

            new Task(() =>
            {
                ArraySegment<byte> inputSegment = new ArraySegment<byte>(new byte[1024]);

                while (true)
                {
                    // MUST read if we want the state to get updated...
                    WebSocketReceiveResult result = ws.ReceiveAsync(inputSegment, CancellationToken.None).Result;
                    
                    if (ws.State != WebSocketState.Open)
                    {
                        break;
                    }
                }
            }).Start();

            while (true)
            {
                if (ws.State != WebSocketState.Open)
                {
                    break;
                }

                byte[] binaryData = { 0xde, 0xad, 0xbe, 0xef, 0xca, 0xfe };
                var segment = new ArraySegment<byte>(binaryData);
                await ws.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);
            }
        }
    }
}
