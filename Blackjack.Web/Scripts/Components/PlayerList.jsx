import React from 'react';
import { TransitionGroup, CSSTransition } from 'react-transition-group';
import Player from './Player';

const PlayerList = (props) => {
  const players = props.players || [];
  const playersMap = players.map(function(player) {
    return (
      <CSSTransition
        key={player.name}
        classNames="fade"
        timeout={{ enter: 500, exit: 300 }}
      >
        <Player player={player} key={player.name}/>
      </CSSTransition>
    ) ;
  });

  return (
    <div>
      <h6 className='player-header'>Players</h6>
      <TransitionGroup>
        {playersMap}
      </TransitionGroup>
    </div>
  );
};

export default PlayerList;