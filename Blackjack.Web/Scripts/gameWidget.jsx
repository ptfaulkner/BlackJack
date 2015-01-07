var React = require('React');
var Player = require('./Player');
var PlayerList = require('./PlayerList');
var CurrentPlayer = require('./CurrentPlayer');

var GameWidget = React.createClass({

  getCurrentPlayers: function (players) {
    var playerLists = {
	  currentPlayer: {},
	  left: [],
	  right: []
	};

	for(var i = 0; i < players.length; i++) {
	  if(this.props.currentPlayerName === players[i].Name) {
	    playerLists.currentPlayer = players[i];
	  }
	  else if(playerLists.left.length < 2) {
	    playerLists.left.push(players[i]);
	  }
	  else {
	    playerLists.right.push(players[i]);
	  }
	}
	return playerLists;
  },

  render: function () {
    var game = this.props.game || {},
	   dealer = game.Dealer || {},
	   players = game.Players || [],
	   playerLists = this.getCurrentPlayers(players);

	return (
    <div className='game-container'>
	  <div className='player-list'>
	    <PlayerList players={playerLists.left} 
		  activeSlot={game.ActiveSlot} 
		  currentPlayerName={this.props.currentPlayerName}
		  gameStatus={game.GameStatus} />
	  </div>
	  <div className='game-area'>
        <Player player={dealer} />
	    <CurrentPlayer player={playerLists.currentPlayer}
			  activeSlot={game.ActiveSlot}
			  currentPlayerName={this.props.currentPlayerName}
			  gameStatus={game.GameStatus}
			  doGameAction={this.props.doGameAction} />
	  </div>
	  <div className='player-list'>
	    <PlayerList players={playerLists.right} 
		  activeSlot={game.ActiveSlot} 
		  currentPlayerName={this.props.currentPlayerName}
		  gameStatus={game.GameStatus} />
	  </div>
	  <br className='clear-fix' />
    </div>
  );
  }
});

module.exports = GameWidget;