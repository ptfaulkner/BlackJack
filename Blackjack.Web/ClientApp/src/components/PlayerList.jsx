import { useRef } from 'react';
import { TransitionGroup, CSSTransition } from 'react-transition-group';
import Player from './Player';

const PlayerList = (props) => {
  const nodeRefs = useRef({});
  const players = props.players || [];
  const playersMap = players.map(function(player) {
    if (!nodeRefs.current[player.name]) {
      nodeRefs.current[player.name] = { current: null };
    }
    return (
      <CSSTransition
        key={player.name}
        nodeRef={nodeRefs.current[player.name]}
        classNames="fade"
        timeout={{ enter: 500, exit: 300 }}
      >
        <div ref={nodeRefs.current[player.name]}>
          <Player player={player} />
        </div>
      </CSSTransition>
    );
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