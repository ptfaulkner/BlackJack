import React from 'react';

export default class Card extends React.Component {
  getSuit(suitName) {
    if (suitName === 'Spades') {
      return <span>&spades;</span>;
    }
    else if (suitName === 'Hearts') {
      return <span>&hearts;</span>;
    }
    else if (suitName === 'Clubs') {
      return <span>&clubs;</span>;
    }
    else {
      return <span>&diams;</span>;
    }
  }

  buildCard(number) {
    var cardDirectory = 'Content/CardAssets/',
      svgName = cardDirectory + number + '_of_' + this.props.suit + '.svg';

    return (
      <object className='card' data={svgName} type='image/svg+xml'>
        <span>{number} - {this.props.suit}</span>
      </object>);
  }

  render() {
    var suit = this.getSuit(this.props.suit),
      spots = this.buildCard(this.props.number, suit),
      stack = this.props.index ? 'card stack' : 'card';

    return (
      <div className={stack}>
        {spots}
      </div>
    );
  }
}