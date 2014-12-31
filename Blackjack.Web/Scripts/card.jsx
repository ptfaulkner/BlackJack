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

  buildCard: function(number, suit, color) {
    if(number === 'Two') {
	  return [
	      <div className='spotB1'>{suit}</div>,
	      <div className='spotB5'>{suit}</div>
	  ];
	}

	if(number === 'Three') {
	  return [
	      <div className='spotB1'>{suit}</div>,
	      <div className='spotB3'>{suit}</div>,
	      <div className='spotB5'>{suit}</div>
	  ];
	}

	if(number === 'Four') {
	  return [
	      <div className='spotA1'>{suit}</div>,
	      <div className='spotA5'>{suit}</div>,
	      <div className='spotC1'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}
	
	if(number === 'Five') {
	  return [
	      <div className='spotA1'>{suit}</div>,
	      <div className='spotA5'>{suit}</div>,
		  <div className='spotB3'>{suit}</div>,
	      <div className='spotC1'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}

	if(number === 'Six') {
	  return [
	      <div className='spotA1'>{suit}</div>,
		  <div className='spotA3'>{suit}</div>,
	      <div className='spotA5'>{suit}</div>,
		  <div className='spotC1'>{suit}</div>,
	      <div className='spotC3'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}
	
	if(number === 'Seven') {
	  return [
	      <div className='spotA1'>{suit}</div>,
		  <div className='spotA3'>{suit}</div>,
	      <div className='spotA5'>{suit}</div>,
		  <div className='spotB2'>{suit}</div>,
		  <div className='spotC1'>{suit}</div>,
	      <div className='spotC3'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}

	if(number === 'Eight') {
	  return [
	      <div className='spotA1'>{suit}</div>,
		  <div className='spotA3'>{suit}</div>,
	      <div className='spotA5'>{suit}</div>,
		  <div className='spotB2'>{suit}</div>,
		  <div className='spotB4'>{suit}</div>,
		  <div className='spotC1'>{suit}</div>,
	      <div className='spotC3'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}

	if(number === 'Nine') {
	  return [
	      <div className='spotA1'>{suit}</div>,
		  <div className='spotA2'>{suit}</div>,
	      <div className='spotA4'>{suit}</div>,
		  <div className='spotA5'>{suit}</div>,
		  <div className='spotB3'>{suit}</div>,
		  <div className='spotC1'>{suit}</div>,
	      <div className='spotC2'>{suit}</div>,
	      <div className='spotC4'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}

	if(number === 'Ten') {
	  return [
	      <div className='spotA1'>{suit}</div>,
		  <div className='spotA2'>{suit}</div>,
	      <div className='spotA4'>{suit}</div>,
		  <div className='spotA5'>{suit}</div>,
		  <div className='spotB2'>{suit}</div>,
		  <div className='spotB4'>{suit}</div>,
		  <div className='spotC1'>{suit}</div>,
	      <div className='spotC2'>{suit}</div>,
	      <div className='spotC4'>{suit}</div>,
	      <div className='spotC5'>{suit}</div>
	  ];
	}
	
	return <span>{number} - {suit}</span>;
  },

  render: function () {
    var suit = this.getSuit(this.props.suit),
	  color = 'card ' + (this.props.suit === 'Spades' || this.props.suit === 'Clubs' ? 'black' : 'red'),s
	  spots = this.buildCard(this.props.number, suit);

	return (
	 <div className={color}>
	   {spots}
	 </div>
	 );
  }
});

module.exports = Card;