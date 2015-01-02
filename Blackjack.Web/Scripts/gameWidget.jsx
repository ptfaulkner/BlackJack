var React = require('React');
var Player = require('./Player');
var PlayerList = require('./PlayerList');
var CurrentPlayer = require('./CurrentPlayer');

var GameWidget = React.createClass({

  getCurrentPlayer: function (players) {
    var player = {};
	for(var i = 0; i < players.length; i++) {
	  if(this.props.currentPlayerName === players[i].Name) {
	    player = players[i];
		break;
	  }
	}
	return player;
  },

  render: function () {
    var game = this.props.game || {},
	   dealer = game.Dealer || {},
	   players = game.Players || [],
	   player = this.getCurrentPlayer(players);

	return (
    <div className='game-container'>
	  <div className='game-area'>
      <Player player={dealer} gameStatus={game.GameStatus} />
	  <CurrentPlayer player={player}
			  activeSlot={game.ActiveSlot}
			  currentPlayerName={this.props.currentPlayerName}
			  gameStatus={this.props.gameStatus}
			  doGameAction={this.props.doGameAction} />
	  </div>
	  <PlayerList players={game.Players} 
		activeSlot={game.ActiveSlot} 
		currentPlayerName={this.props.currentPlayerName}
		gameStatus={game.GameStatus}
		doGameAction={this.props.doGameAction} />
	  <br className='clear-fix' />
    </div>
  );
  }
});

module.exports = GameWidget;