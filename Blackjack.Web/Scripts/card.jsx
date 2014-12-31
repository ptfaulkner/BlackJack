var React = require('React');

var Card = React.createClass({
  getSuit: function (suitName) {
    if(suitName === 'Spades') {
	  return <span>&spades;</span>;
    }
	else if(suitName === 'Hearts') {
	  return <span>&hearts;</span>;
    }
	else if(suitName === 'Clubs') {
      return <span>&clubs;</span>;
	}
	else { 
	  return <span>&diams;</span>;
	}
  },

  render: function () {
    var suit = this.getSuit(this.props.suit),
	  color = (this.props.suit === 'Spades' || this.props.suit === 'Clubs' ? 'black' : 'red');

	return (
	  <div className={color}>
	    {suit}-{this.props.number}
	  </div>
	);
  }
});

module.exports = Card;