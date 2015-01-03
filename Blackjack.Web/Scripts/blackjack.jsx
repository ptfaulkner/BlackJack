﻿var React = require('React');
var NewPlayer = require('./NewPlayer');
var GameWidget = require('./GameWidget');
var Header = require('./Header');
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
	websocket.onclose = function (event) {
	  self.setState({ message: event.reason });
	}
  },

  doGameAction: function(actionString) {
    websocket.send(actionString);
  },

  render: function() {
    var self = this,
	  game = this.state.game || {},
	  newPlayers = game.NewPlayers || [];

	var gameState;
	if(this.state.connectionStatus !== 'Connected') 
	  gameState = <NewPlayer connect={self.connect} message={this.state.message} />
	else 
      gameState = <GameWidget game={this.state.game} currentPlayerName={this.state.playerName} doGameAction={this.doGameAction} />

	return (
	  <div>
	    <Header connectionStatus={this.state.connectionStatus} newPlayers={newPlayers} />
		<br className='clear-fix' />
		<div className='game-widget'>
		  {gameState}
		</div>
	  </div>
	);
  }
});

module.exports = Blackjack;