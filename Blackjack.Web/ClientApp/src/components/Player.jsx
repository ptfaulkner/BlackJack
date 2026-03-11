import { useRef } from "react";
import { TransitionGroup, CSSTransition } from "react-transition-group";
import Card from "./Card";

const Player = (props) => {
  const nodeRefs = useRef({});
  const player = props.player || {};
  const hand = player.hand || [];
  const cards = hand.map((card, index) => {
    const key = `${card.suit}-${card.number}`;
    if (!nodeRefs.current[key]) {
      nodeRefs.current[key] = { current: null };
    }
    return (
      <CSSTransition
        key={key}
        nodeRef={nodeRefs.current[key]}
        classNames="fade"
        timeout={{ enter: 500, exit: 300 }}
      >
        <div ref={nodeRefs.current[key]} style={{ float: 'left' }}>
          <Card suit={card.suit} number={card.number} index={index} />
        </div>
      </CSSTransition>
    );
  });

  return (
    <div className="text-center">
      <div className={`player${player.winningStatus && player.winningStatus !== "Open" ? ` result-${player.winningStatus.toLowerCase()}` : ''}`}>
        <div className="player-info">
          <span className="player-name">{player.name}</span>
          <span className="player-score">{player.score}</span>
        </div>
        {player.winningStatus !== "Open" && (
          <div className={`player-status status-${player.winningStatus?.toLowerCase()}`}>
            {player.winningStatus}
          </div>
        )}
        <div className="hand-container">
          <TransitionGroup>{cards}</TransitionGroup>
        </div>
        <br className="clear-fix" />
      </div>
    </div>
  );
};

export default Player;
