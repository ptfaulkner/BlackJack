var React = require('React');
var Player = require('./Player');

var PlayerList = React.createClass({
  render: function () {
     var players = this.props.players || [],
	   playersMap = players.map(function (player) {
	     return <span><Player player={player} /></span>;
	   });

	 return (
	   <div>{playersMap}</div>
	 );
  }
});

module.exports = PlayerList;