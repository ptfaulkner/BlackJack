import React from 'react';
import NewPlayer from './newPlayer';
import GameWidget from './gameWidget';
import Header from './header';
let websocket;

export default class Blackjack extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      connectionStatus: 'Not Connected',
      playerName: '',
      game: {}
    };

    this.connect = this.connect.bind(this);
  }

  connect (playerName) {
    const host = window.location.host;
    const protocol = window.location.protocol;
    const uri = (protocol === 'https:' ? 'wss' : 'ws') + '://' + host + '/api/blackjack?playerName=' + playerName;

    websocket = new WebSocket(uri);

    websocket.onopen = () => {
      this.setState({ connectionStatus: 'Connected', playerName: playerName });
    };
    websocket.onerror = (event) => {
      this.setState({ connectionStatus: 'Connection Error :(' });
    };
    websocket.onmessage = (event) => {
      var dataJson = JSON.parse(event.data);
      this.setState({ game: dataJson });
    };
    websocket.onclose = (event) => {
      this.setState({ message: event.reason });
    };
  }

  doGameAction (actionString) {
    websocket.send(actionString);
  }

  render() {
    const game = this.state.game || {};
    const newPlayers = game.newPlayers || [];

    let gameState;
    if (this.state.connectionStatus !== 'Connected')
      gameState = <NewPlayer connect={this.connect} message={this.state.message}/>;
    else
      gameState = <GameWidget game={this.state.game} currentPlayerName={this.state.playerName} doGameAction={this.doGameAction} />;

    return (
      <div>
        <Header connectionStatus={this.state.connectionStatus} newPlayers={newPlayers} />
        <br className='clear-fix' />
        <div className='game-widget'>
          {gameState}
        </div>
      </div>
    );
  }
}