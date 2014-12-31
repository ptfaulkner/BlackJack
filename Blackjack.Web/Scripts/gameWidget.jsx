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
      <Player player={dealer} />
	  <PlayerList players={game.Players} />
    </div>
  );
  }
});

module.exports = GameWidget;