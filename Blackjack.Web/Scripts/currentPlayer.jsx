import React from 'react';
import { CSSTransitionGroup } from 'react-transition-group';
import Card from './card';

export default class CurrentPlayer extends React.Component {
  constructor(props) {
    super(props);

    this.hit = this.hit.bind(this);
    this.stay = this.stay.bind(this);
    this.deal = this.deal.bind(this);
    this.chooseButtons = this.chooseButtons.bind(this);
  }

  hit() {
    this.props.doGameAction('Hit');
  }

  stay() {
    this.props.doGameAction('Stay');
  }

  deal() {
    this.props.doGameAction('Deal');
  }

  chooseButtons() {
    var props = this.props || {},
      player = props.player || {};

    if (player.handStatus === 'Open' && player.isTurnToHit) {
      return (
        <div className="turn-buttons">
          <input type="button" value="Hit" className='form-item button' onClick={this.hit} />
          <input type="button" value="Stay" className='form-item button' onClick={this.stay} />
        </div>
      );
    } else if (props.gameStatus !== 'Open') {
      return (
        <div className="turn-buttons">
          <input type="button" value="Deal" className='form-item button' onClick={this.deal} />
        </div>
      );
    } else if (player.handStatus === 'Open') {
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
  }

  render() {
    var player = this.props.player || {},
      hand = player.hand || [],
      buttons = this.chooseButtons(),
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
              transitionName="animate"
              transitionEnterTimeout={500}
              transitionLeaveTimeout={300}>
              {cards}
            </CSSTransitionGroup>
          </div>
          <br className='clear-fix' />
          {buttons}
        </div>
      </div>
    );
  }
}