var React = require('React/addons');
var ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;

var Header = React.createClass({
  render: function() {
    var newPlayersArray = this.props.newPlayers || [],
	  newPlayers = newPlayersArray.join(', '),
	  newPlayersSpan = {};

	if(newPlayers.length) {
	  newPlayersSpan = <span key='new-players'>Joining now: {newPlayers}</span>;
	}

    return (
	  <header>
	    <div className='title'>
		  <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
		</div>
		<div className='new-players'>
		  <ReactCSSTransitionGroup transitionName="fade">
		    {newPlayersSpan}
		  </ReactCSSTransitionGroup>
		</div>
		<div className='connection-status'>
		  <span>{this.props.connectionStatus}</span>
		</div>
	  </header>
	);
  }
});

module.exports = Header;