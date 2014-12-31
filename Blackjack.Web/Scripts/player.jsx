var React = require('React');

var Player = React.createClass({

  render: function () {
    var player = this.props.player || {},
	  hand = player.Hand || [],
	  cards = hand.map(function (card) {
	    return <span key='{card.Number}-{card.Suit}'><span>{card.Number}</span>-<span>{card.Suit}</span></span>;
	  });

   return (
     <div>
      <span>{player.Name}</span>
      <div>
        Winning Status: <span>{player.WinningStatus}</span><br />
        Hand Status: <span>{player.HandStatus}</span>
      </div>
	  {cards}
    </div>
   );
  }
});

module.exports = Player;