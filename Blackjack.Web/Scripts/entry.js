import React from 'react';
import ReactDOM from 'react-dom';
import BlackjackClass from './blackjack';
const Blackjack = React.createElement(BlackjackClass);

ReactDOM.render(Blackjack,
  document.getElementById('content')
);