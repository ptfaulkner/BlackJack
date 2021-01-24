import React from "react";
import { TransitionGroup, CSSTransition } from "react-transition-group";
import Card from "./Card";

const CurrentPlayer = (props) => {
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
    return (
      <CSSTransition
        key={key}
        classNames="animate"
        timeout={{ enter: 500, exit: 300 }}
      >
        <Card suit={card.suit} number={card.number} index={index} />
      </CSSTransition>
    );
  });

  return (
    <div className="text-center">
      <div className="player">
        <span>
          {player.name} - {player.score}
        </span>
        <div>
          {player.winningStatus !== "Open" && (
            <span>{player.winningStatus}</span>
          )}
        </div>
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
