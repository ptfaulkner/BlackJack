var React = require('React/addons');
var ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
var Player = require('./Player');

var PlayerList = React.createClass({
  render: function () {
     var props = this.props || {},
	   players = props.players || [],
	   playersMap = players.map(function (player) {
	     return <Player player={player}	key={player.Name}
		   activeSlot={props.activeSlot} />;
	   });

	 return (
	   <div>
	     <h6 className='player-header'>Players</h6>
		 <ReactCSSTransitionGroup transitionName="fade">
	       {playersMap}
		 </ReactCSSTransitionGroup>
	   </div>
	 );
  }
});

module.exports = PlayerList;