import React from 'react';
import Player from './player';
import PlayerList from './playerList';
import CurrentPlayer from './currentPlayer';

export default class GameWidget extends React.Component {

  splitTablePlayers(players) {
    const playerLists = {
      left: [],
      right: []
    };

    playerLists.left = players.splice(0, 2);
    playerLists.right = players;
    return playerLists;
  }

  render() {
    const game = this.props.game || {};
    const dealer = game.dealer || {};
    const currentPlayer = game.player || {};
    const players = game.tablePlayers || [];
    const playerLists = this.splitTablePlayers(players);

    return (
      <div className='game-container'>
        <div className='player-list'>
          <PlayerList players={playerLists.left}
              gameStatus={game.gameStatus}
          />
        </div>
        <div className='game-area'>
          <Player player={dealer} />
          <CurrentPlayer player={currentPlayer}
              gameStatus={game.gameStatus}
              doGameAction={this.props.doGameAction}
          />
        </div>
        <div className='player-list'>
          <PlayerList players={playerLists.right}
              gameStatus={game.gameStatus}
          />
        </div>
        <br className='clear-fix' />
      </div>
    );
  }
}