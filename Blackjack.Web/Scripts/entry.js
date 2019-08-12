const React = require('react');
const ReactDOM = require('react-dom');
const Blackjack = React.createElement(require('./blackjack'));

ReactDOM.render(Blackjack,
  document.getElementById('content')
);