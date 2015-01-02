var React = require('React');
var Card = require('./Card');

var Player = React.createClass({

  render: function () {
    var player = this.props.player || {},
	  hand = player.Hand || [],
	  cards = hand.map(function (card, index) {
	    return <Card suit={card.Suit} number={card.Number} index={index} />;
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
	    {cards}
	  </div>
	  <br className='clear-fix' />
    </div>
	</div>
   );
  }
});

module.exports = Player;