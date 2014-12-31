var React = require('React');
var Blackjack = React.createFactory(require('./Blackjack'));

React.render(Blackjack(),
  document.getElementById('content')
);