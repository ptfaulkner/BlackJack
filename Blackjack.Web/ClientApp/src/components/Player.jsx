import React from "react";
import { TransitionGroup, CSSTransition } from "react-transition-group";
import Card from "./Card";

const Player = (props) => {
  const player = props.player || {};
  const hand = player.hand || [];
  const cards = hand.map((card, index) => {
    const key = `${card.suit}-${card.number}`;
    return (
      <CSSTransition
        key={key}
        classNames="fade"
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
      </div>
    </div>
  );
};

export default Player;
