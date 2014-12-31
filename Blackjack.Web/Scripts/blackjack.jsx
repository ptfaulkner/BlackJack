var React = require('React');
var NewPlayer = require('./NewPlayer');

var Blackjack = React.createClass({
  render: function() {
    return (
	  <NewPlayer />
	);
  }
});

module.exports = Blackjack;