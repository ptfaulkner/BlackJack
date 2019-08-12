var React = require('react');
var CSSTransitionGroup = require('react-transition-group/CSSTransitionGroup');
var Card = require('./card');

class Player extends React.Component {

  render() {
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
            <CSSTransitionGroup
              transitionName="fade"
              transitionEnterTimeout={500}
              transitionLeaveTimeout={300}>
              {cards}
            </CSSTransitionGroup>
          </div>
          <br className='clear-fix' />
        </div>
      </div>
    );
  }
}

module.exports = Player;