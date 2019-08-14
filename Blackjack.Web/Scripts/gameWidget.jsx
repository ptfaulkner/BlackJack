import React from 'react';
import Player from './player';
import PlayerList from './playerList';
import CurrentPlayer from './currentPlayer';

const GameWidget = (props) => {

  const splitTablePlayers = (p) => {
    const playerLists = {
      left: [],
      right: []
    };

    playerLists.left = p.splice(0, 2);
    playerLists.right = p;
    return playerLists;
  };

  const game = props.game || {};
  const dealer = game.dealer || {};
  const currentPlayer = game.player || {};
  const players = game.tablePlayers || [];
  const splitPlayerLists = splitTablePlayers(players);

  return (
    <div className='game-container'>
      <div className='player-list'>
        <PlayerList players={splitPlayerLists.left}
            gameStatus={game.gameStatus}
        />
      </div>
      <div className='game-area'>
        <Player player={dealer} />
        <CurrentPlayer player={currentPlayer}
            gameStatus={game.gameStatus}
            doGameAction={props.doGameAction}
        />
      </div>
      <div className='player-list'>
        <PlayerList players={splitPlayerLists.right}
            gameStatus={game.gameStatus}
        />
      </div>
      <br className='clear-fix' />
    </div>
  );
};

export default GameWidget;