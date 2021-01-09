import React from 'react';
import { TransitionGroup, CSSTransition } from 'react-transition-group';

const Header = (props) => {
  const newPlayersArray = props.newPlayers || [];
  const newPlayers = newPlayersArray.join(', ');
  let newPlayersSpan = (
    <CSSTransition      
      classNames="fade"
      timeout={500}
      key='no-new-players'
    >
      <span />
    </CSSTransition>
   );

  if (newPlayers.length) {
    newPlayersSpan = (
      <CSSTransition      
        classNames="fade"
        timeout={500}
        key='new-players'
      >
        <span>Joining now: {newPlayers}</span>
      </CSSTransition>
    );
  }

  return (
    <header>
      <div className='title'>
        <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
      </div>
      <div className='new-players'>
        <TransitionGroup>
          {newPlayersSpan}
        </TransitionGroup>
      </div>
      <div className='connection-status'>
        <span>{props.connectionStatus}</span>
      </div>
    </header>
  );
};

export default Header;