var React = require('React');
var NewPlayer = require('Scripts/NewPlayer');

var Blackjack = React.createClass({
  render: function() {
    return (
	  <NewPlayer />
	);
  }
});

React.render(
  <NewPlayer />,
  document.getElementById('content')
);