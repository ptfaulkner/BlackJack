import React from 'react';
import { CSSTransitionGroup } from 'react-transition-group';
import Card from './card';

const CurrentPlayer = (props) => {

  const hit = () => {
    props.doGameAction('Hit');
  };

  const stay = () => {
    props.doGameAction('Stay');
  };

  const deal = () => {
    props.doGameAction('Deal');
  };

  const chooseButtons = () => {
    const player = props.player || {};

    if (player.handStatus === 'Open' && player.isTurnToHit) {
      return (
        <div className="turn-buttons">
          <input type="button" value="Hit" className='form-item button' onClick={hit}/>
          <input type="button" value="Stay" className='form-item button' onClick={stay}/>
        </div>
      );
    } else if (props.gameStatus !== 'Open') {
      return (
        <div className="turn-buttons">
          <input type="button" value="Deal" className='form-item button' onClick={deal}/>
        </div>
      );
    } else if (player.handStatus === 'Open') {
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
    return <Card key={key} suit={card.suit} number={card.number} index={index}/>;
  });

  return (
    <div className='text-center'>
      <div className='player'>
        <span>{player.name}</span>
        <div>
          Winning Status: <span>{player.winningStatus}</span><br/>
          Hand Status: <span>{player.handStatus}</span>
        </div>
        <div className='hand-container'>
          <CSSTransitionGroup
            transitionName="animate"
            transitionEnterTimeout={500}
            transitionLeaveTimeout={300}>
            {cards}
          </CSSTransitionGroup>
        </div>
        <br className='clear-fix'/>
        {buttons}
      </div>
    </div>
  );
};

export default CurrentPlayer;