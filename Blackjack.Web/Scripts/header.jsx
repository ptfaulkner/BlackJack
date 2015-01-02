var React = require('React');

var Header = React.createClass({
  render: function() {
    var newPlayersArray = this.props.newPlayers || [],
	  newPlayers = newPlayersArray.join(', ');

	if(newPlayers.length) {
	  newPlayers = 'Joining now: ' + newPlayers;
	}

    return (
	  <header>
	    <div className='title'>
		  <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
		</div>
		<div className='new-players'>
		  <span>{newPlayers}</span>
		</div>
		<div className='connection-status'>
		  <span>{this.props.connectionStatus}</span>
		</div>
	  </header>
	);
  }
});

module.exports = Header;