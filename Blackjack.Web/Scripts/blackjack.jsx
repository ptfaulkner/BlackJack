var React = require('React');
var NewPlayer = require('./NewPlayer');
var GameWidget = require('./GameWidget');
var websocket;

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
      uri = (protocol === 'https:' ? 'wss' : 'ws') + '://' + host + '/api/blackjack?playerName=' + playerName;

    websocket = new WebSocket(uri);

    websocket.onopen = function () {
	  self.setState({ connectionStatus: 'Connected', playerName: playerName });
    };
    websocket.onerror = function (event) {
   	  self.setState({ connectionStatus: 'Connection Error :(' });
    }
    websocket.onmessage = function (event) {
      var dataJson = JSON.parse(event.data);
      self.setState({ game: dataJson });
    }
  },

  doGameAction: function(actionString) {
    websocket.send(actionString);
  },

  render: function() {
    var self = this;

	var gameState;
	if(this.state.connectionStatus === 'Not Connected') 
	  gameState = <NewPlayer connect={self.connect} />
	else 
      gameState = <GameWidget game={this.state.game} currentPlayerName={this.state.playerName} doGameAction={this.doGameAction} />

	return (
	  <div>
	    {this.state.connectionStatus}
		{gameState}
	  </div>
	);
  }
});

module.exports = Blackjack;