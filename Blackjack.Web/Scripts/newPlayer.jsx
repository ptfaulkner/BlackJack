var React = require('React');

var NewPlayer = React.createClass({
  connect: function() {
    var playerName = this.refs.playerName.getDOMNode().value.trim();
    this.props.connect(playerName);
  },
  
  render: function() {
    return (
	  <div>
	    <label htmlFor="playerName">Enter your name: </label>
        <input id="playerName" name="playerName" ref="playerName" type="text" />
        <input type="button" value="Connect" onClick={this.connect}/>
	  </div>
    );
  }
});

module.exports = NewPlayer;