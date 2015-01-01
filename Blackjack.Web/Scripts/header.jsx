var React = require('React');

var Header = React.createClass({
  render: function() {
    return (
	  <header>
	    <div className='title'>
		  <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
		</div>
		<div className='connection-status'>
		  <span>{this.props.connectionStatus}</span>
		</div>
	  </header>
	);
  }
});

module.exports = Header;