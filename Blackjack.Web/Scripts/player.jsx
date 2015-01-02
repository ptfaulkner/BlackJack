var React = require('React/addons');
var ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
var Card = require('./Card');

var Player = React.createClass({

  render: function () {
    var player = this.props.player || {},
	  hand = player.Hand || [],
	  cards = hand.map(function (card, index) {
	    var key = card.Suit + '-' + card.Number;
	    return <Card key={key} suit={card.Suit} number={card.Number} index={index} />;
	  });

   return (
     <div className='text-center'>
     <div className='player'>
      <span>{player.Name}</span>
      <div>
        Winning Status: <span>{player.WinningStatus}</span><br />
        Hand Status: <span>{player.HandStatus}</span>
      </div>
	  <div className='hand-container'>
	    <ReactCSSTransitionGroup transitionName="fade">
		  {cards}
		</ReactCSSTransitionGroup>
	  </div>
	  <br className='clear-fix' />
    </div>
	</div>
   );
  }
});

module.exports = Player;