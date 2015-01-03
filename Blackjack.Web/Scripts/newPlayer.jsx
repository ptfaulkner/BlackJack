var React = require('React');

var NewPlayer = React.createClass({
  connect: function() {
    var playerName = this.refs.playerName.getDOMNode().value.trim();
    this.props.connect(playerName);
  },
  
  render: function() {
    return (
	  <div className='new-player'>
	    <label htmlFor='playerName'>Enter your name: </label>
        <input id='playerName' name='playerName' ref='playerName' type='text' className='textbox form-item' />
        <input type='button' value='Connect' className='button form-item' onClick={this.connect}/>
		<br />
		<span>{this.props.message}</span>
	  </div>
    );
  }
});

module.exports = NewPlayer;