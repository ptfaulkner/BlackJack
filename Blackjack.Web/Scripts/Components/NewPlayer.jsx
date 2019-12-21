import React from 'react';

export default class NewPlayer extends React.Component {
  constructor(props) {
    super(props);

    this.connect = this.connect.bind(this);
  }
  
  connect() {
    const playerName = this.refs.playerName.value.trim();
    this.props.connect(playerName);
  }

  render () {
    return (
      <div className='new-player'>
        <label htmlFor='playerName'>Enter your name: </label>
        <input id='playerName' name='playerName' ref='playerName' type='text' className='textbox form-item' />
        <input type='button' value='Connect' className='button form-item' onClick={this.connect} />
        <br />
        <span>{this.props.message}</span>
      </div>
    );
  }
}