import React from 'react';
import { CSSTransitionGroup } from 'react-transition-group';

const Header = (props) => {
  const newPlayersArray = props.newPlayers || [];
  const newPlayers = newPlayersArray.join(', ');
  let newPlayersSpan = <span key='no-new-players'/>;

  if (newPlayers.length) {
    newPlayersSpan = <span key='new-players'>Joining now: {newPlayers}</span>;
  }

  return (
    <header>
      <div className='title'>
        <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
      </div>
      <div className='new-players'>
        <CSSTransitionGroup
          transitionName="fade"
          transitionEnterTimeout={500}
          transitionLeaveTimeout={300}>
          {newPlayersSpan}
        </CSSTransitionGroup>
      </div>
      <div className='connection-status'>
        <span>{props.connectionStatus}</span>
      </div>
    </header>
  );
};

export default Header;