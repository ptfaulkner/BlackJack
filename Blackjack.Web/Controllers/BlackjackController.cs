using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;
using Blackjack.Web.WebSockets;

namespace Blackjack.Web.Controllers
{
    [RoutePrefix("api/blackjack")]
    public class BlackjackController : ApiController
    {
        private readonly Handler _handler;

        public BlackjackController(Handler handler)
        {
            _handler = handler;
        }

        [HttpGet, Route]
        public HttpResponseMessage Get(string playerName)
        {
            HttpContext currentContext = HttpContext.Current;
            if (currentContext.IsWebSocketRequest ||
                currentContext.IsWebSocketRequestUpgrading)
            {
                currentContext.AcceptWebSocketRequest(_handler.ProcessWebsocketSession);
                return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Need a websocket connection.");
        }  
    }
}
