import { useRef } from "react";
import { TransitionGroup, CSSTransition } from "react-transition-group";
import Card from "./Card";

const CurrentPlayer = (props) => {
  const nodeRefs = useRef({});

  const hit = () => {
    props.doGameAction("Hit");
  };

  const stay = () => {
    props.doGameAction("Stay");
  };

  const deal = () => {
    props.doGameAction("Deal");
  };

  const chooseButtons = () => {
    const player = props.player || {};

    if (player.handStatus === "Open" && player.isTurnToHit) {
      return (
        <div className="turn-buttons">
          <input
            type="button"
            value="Hit"
            className="form-item button"
            onClick={hit}
          />
          <input
            type="button"
            value="Stay"
            className="form-item button"
            onClick={stay}
          />
        </div>
      );
    } else if (props.gameStatus !== "Open") {
      return (
        <div className="turn-buttons">
          <input
            type="button"
            value="Deal"
            className="form-item button"
            onClick={deal}
          />
        </div>
      );
    } else if (player.handStatus === "Open") {
      return (
        <div className="turn-buttons">
          <span>waiting for your turn...</span>
        </div>
      );
    } else {
      return (
        <div className="turn-buttons">
          <span>waiting for other players to finish...</span>
        </div>
      );
    }
  };

  const player = props.player || {};
  const hand = player.hand || [];
  const buttons = chooseButtons();
  const cards = hand.map((card, index) => {
    const key = `${card.suit}-${card.number}`;
    if (!nodeRefs.current[key]) {
      nodeRefs.current[key] = { current: null };
    }
    return (
      <CSSTransition
        key={key}
        nodeRef={nodeRefs.current[key]}
        classNames="animate"
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
        {buttons}
      </div>
    </div>
  );
};

export default CurrentPlayer;
