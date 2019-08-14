import React from 'react';
import { CSSTransitionGroup } from 'react-transition-group';
import Player from './player';

const PlayerList = (props) => {
  const players = props.players || [];
  const playersMap = players.map(function(player) {
    return <Player player={player} key={player.name}/>;
  });

  return (
    <div>
      <h6 className='player-header'>Players</h6>
      <CSSTransitionGroup
        transitionName="fade"
        transitionEnterTimeout={500}
        transitionLeaveTimeout={300}>
        {playersMap}
      </CSSTransitionGroup>
    </div>
  );
};

export default PlayerList;