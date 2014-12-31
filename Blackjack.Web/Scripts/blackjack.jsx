var React = require('React');
var NewPlayer = require('./NewPlayer');

var Blackjack = React.createClass({
  getInitialState: function () {
	return { 
	  connectionStatus: 'Not Connected',
	  playerName: '',
	  game: {}
	};
  },

  connect: function(playerName) {
    var self = this,
	  host = window.location.host,
      protocol = window.location.protocol,
      uri = (protocol === 'https:' ? 'wss' : 'ws') + '://' + host + '/api/blackjack?playerName=' + playerName,
      websocket = new WebSocket(uri);

    websocket.onopen = function () {
	  self.setState({ connectionStatus: 'Connected' });
    };
    websocket.onerror = function (event) {
   	  self.setState({ connectionStatus: 'Connection Error :(' });
    }
    websocket.onmessage = function (event) {
      var dataJson = JSON.parse(event.data);
      self.setState({ game: dataJson });
    }
  },

  render: function() {
    var self = this;

	var gameState;
	if(this.state.connectionStatus === 'Not Connected') 
	  gameState = <NewPlayer connect={self.connect} />
	else 
      gameState = <div>{this.state.connectionStatus}</div>

	return (
	  <div>
	    {this.state.connectionStatus}
		{gameState}
	  </div>
	);
  }
});

module.exports = Blackjack;