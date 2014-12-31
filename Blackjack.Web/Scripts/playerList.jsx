var React = require('React');
var Player = require('./Player');

var PlayerList = React.createClass({
  render: function () {
     var props = this.props || {},
	   players = props.players || [],
	   playersMap = players.map(function (player) {
	     return <span><Player player={player}		
		   activeSlot={props.activeSlot} 
		   currentPlayerName={props.currentPlayerName}
		   gameStatus={props.gameStatus}
		   doGameAction={props.doGameAction} /></span>;
	   });

	 return (
	   <div>{playersMap}</div>
	 );
  }
});

module.exports = PlayerList;