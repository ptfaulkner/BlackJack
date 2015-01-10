var React = require('React');
var Player = require('./Player');
var PlayerList = require('./PlayerList');
var CurrentPlayer = require('./CurrentPlayer');

var GameWidget = React.createClass({

  splitTablePlayers: function (players) {
    var playerLists = {
	  left: [],
	  right: []
	};

	playerLists.left = players.splice(0, 2);
	playerLists.right = players;
	return playerLists;
  },

  render: function () {
    var game = this.props.game || {},
	   dealer = game.Dealer || {},
	   currentPlayer = game.Player || {},
	   players = game.TablePlayers || [],
	   playerLists = this.splitTablePlayers(players);

	return (
    <div className='game-container'>
	  <div className='player-list'>
	    <PlayerList players={playerLists.left} 
		  gameStatus={game.GameStatus} />
	  </div>
	  <div className='game-area'>
        <Player player={dealer} />
	    <CurrentPlayer player={currentPlayer}
			  gameStatus={game.GameStatus}
			  doGameAction={this.props.doGameAction} />
	  </div>
	  <div className='player-list'>
	    <PlayerList players={playerLists.right}
		  gameStatus={game.GameStatus} />
	  </div>
	  <br className='clear-fix' />
    </div>
  );
  }
});

module.exports = GameWidget;