﻿var React = require('React');
var Card = require('./Card');

var Player = React.createClass({

  hit: function() {
    this.props.doGameAction('Hit');
  },

  stay: function() {
    this.props.doGameAction('Stay');
  },

  deal: function() {
    this.props.doGameAction('Deal');
  },

  chooseButtons: function () {
    var props = this.props || {},
      player = props.player || {};
	
	if(player.HandStatus === 'Open' && player.Position === props.activeSlot && props.currentPlayerName === player.Name) {
	  return (
	    <div className="turn-buttons">
          <input type="button" value="Hit" onClick={this.hit} />
          <input type="button" value="Stay" onClick={this.stay} />
        </div>
	  );
	}

	if(props.gameStatus !== 'Open') {
	  return (
        <div className="turn-buttons">
          <input type="button" value="Deal" onClick={this.deal} />
        </div>
      );
	}
  },

  render: function () {
    var player = this.props.player || {},
	  buttons = this.chooseButtons(),
	  hand = player.Hand || [],
	  cards = hand.map(function (card) {
	    return <Card suit={card.Suit} number={card.Number} />;
	  });

   return (
     <div>
      <span>{player.Name}</span>
      <div>
        Winning Status: <span>{player.WinningStatus}</span><br />
        Hand Status: <span>{player.HandStatus}</span>
      </div>
	  {cards}
	  {buttons}
    </div>
   );
  }
});

module.exports = Player;