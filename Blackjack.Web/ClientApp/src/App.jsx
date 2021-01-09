import React from 'react';
import {
  HubConnectionBuilder
} from '@microsoft/signalr';
import './styles/blackjack.css';
import NewPlayer from './components/NewPlayer';
import GameWidget from './components/GameWidget';
import Header from './components/Header';
let connection;

export default class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      connectionStatus: 'Not Connected',
      playerName: '',
      game: {}
    };

    this.connect = this.connect.bind(this);
  }

  connect(playerName) {
    connection = new HubConnectionBuilder()
      .withUrl("/blackjackhub")
      .build();
    
    connection.start().then(() => {
      this.setState({ connectionStatus: 'Connected', playerName: playerName });
      connection.invoke("JoinGame", playerName);
    });

    connection.on("GameUpdate",
      (gameData) => {
        this.setState({ game: gameData });
      });
  }

  doGameAction(actionString) {
    connection.invoke("SendGameAction", actionString);
  }

  render() {
    const game = this.state.game || {};
    const newPlayers = game.newPlayers || [];

    let gameState;
    if (this.state.connectionStatus !== 'Connected')
      gameState = <NewPlayer connect={this.connect} message={this.state.message} />;
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
