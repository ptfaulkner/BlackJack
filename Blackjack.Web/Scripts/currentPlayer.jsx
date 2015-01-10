var React = require('React/addons');
var ReactCSSTransitionGroup = React.addons.CSSTransitionGroup;
var Card = require('./Card');

var CurrentPlayer = React.createClass({
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
	
	if(player.HandStatus === 'Open' && player.IsTurnToHit && props.currentPlayerName === player.Name) {
	  return (
	    <div className="turn-buttons">
          <input type="button" value="Hit" className='form-item button' onClick={this.hit} />
          <input type="button" value="Stay" className='form-item button' onClick={this.stay} />
        </div>
	  );
	} else if(props.gameStatus !== 'Open') {
	  return (
        <div className="turn-buttons">
          <input type="button" value="Deal" className='form-item button' onClick={this.deal} />
        </div>
      );
	} else if(player.HandStatus === 'Open') {
	  return (
	    <div className="turn-buttons">
		  <span>waiting for your turn...</span>
		</div>
	  );
	} else {
	  return (
	    <div className="turn-buttons">
		  <span>waiting for other players to finish...</span>
		</div>
	  );
	}
  },

  render: function () {
    var player = this.props.player || {},
	  hand = player.Hand || [],
	  buttons = this.chooseButtons(),
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
	    <ReactCSSTransitionGroup transitionName="animate">
		  {cards}
		</ReactCSSTransitionGroup>
	  </div>
	  <br className='clear-fix' />
	  {buttons}
    </div>
	</div>
   );
  }
});

module.exports = CurrentPlayer;