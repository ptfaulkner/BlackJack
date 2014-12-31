var React = require('React');

var NewPlayer = React.createClass({
  render: function() {
    return (
	  <div>
	    <label htmlFor="playerName">Enter your name: </label>
        <input id="playerName" name="playerName" type="text" data-bind="value: playerName"/>
        <input type="button" value="Connect" data-bind="click: connect"/>
	  </div>
    );
  }
});

module.exports = NewPlayer;