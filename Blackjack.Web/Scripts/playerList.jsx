var React = require('React');
var Player = require('./Player');

var PlayerList = React.createClass({
  render: function () {
     var props = this.props || {},
	   players = props.players || [],
	   playersMap = players.map(function (player) {
	     return <Player player={player}		
		   activeSlot={props.activeSlot} />;
	   });

	 return (
	   <div>
	     <h6 className='player-header'>Players</h6>
	     {playersMap}
	   </div>
	 );
  }
});

module.exports = PlayerList;