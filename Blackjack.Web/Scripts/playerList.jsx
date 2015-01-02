var React = require('React');
var Player = require('./Player');

var PlayerList = React.createClass({
  render: function () {
     var props = this.props || {},
	   players = props.players || [],
	   playersMap = players.map(function (player) {
         if(props.currentPlayerName === player.Name) {
			return;
		 }

	     return <Player player={player}		
		   activeSlot={props.activeSlot} />;
	   });

	 return (
	   <div>{playersMap}</div>
	 );
  }
});

module.exports = PlayerList;