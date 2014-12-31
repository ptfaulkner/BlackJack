var React = require('React');
var Player = require('./Player');
var PlayerList = require('./PlayerList');

var GameWidget = React.createClass({

  render: function () {
    var game = this.props.game || {},
       newPlayers = game.NewPlayers || [],
	   newPlayerString = newPlayers.join(', '),
	   dealer = game.Dealer || {};

	return (
    <div>
      New Players: <span>{newPlayerString}</span>
      <br/>
      Game Status: <span>{game.GameStatus}</span>
	  <hr/>
      <Player player={dealer} gameStatus={game.GameStatus} />
	  <PlayerList players={game.Players} 
		activeSlot={game.ActiveSlot} 
		currentPlayerName={this.props.currentPlayerName}
		gameStatus={game.GameStatus}
		doGameAction={this.props.doGameAction} />
    </div>
  );
  }
});

module.exports = GameWidget;