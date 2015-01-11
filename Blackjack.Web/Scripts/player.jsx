var React = require('React/addons');
var ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
var Card = require('./Card');

var Player = React.createClass({

  render: function () {
    var player = this.props.player || {},
	  hand = player.hand || [],
	  cards = hand.map(function (card, index) {
	    var key = card.suit + '-' + card.number;
	    return <Card key={key} suit={card.suit} number={card.number} index={index} />;
	  });

   return (
     <div className='text-center'>
     <div className='player'>
      <span>{player.name}</span>
      <div>
        Winning Status: <span>{player.winningStatus}</span><br />
        Hand Status: <span>{player.handStatus}</span>
      </div>
	  <div className='hand-container'>
		  {cards}
	  </div>
	  <br className='clear-fix' />
    </div>
	</div>
   );
  }
});

module.exports = Player;