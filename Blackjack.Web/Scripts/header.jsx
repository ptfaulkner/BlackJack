var React = require('react');
var CSSTransitionGroup = require('react-transition-group/CSSTransitionGroup');

class Header extends React.Component {
  render() {
    var newPlayersArray = this.props.newPlayers || [],
      newPlayers = newPlayersArray.join(', '),
      newPlayersSpan = <span key='no-new-players' />;

    if (newPlayers.length) {
      newPlayersSpan = <span key='new-players'>Joining now: {newPlayers}</span>;
    }

    return (
      <header>
        <div className='title'>
          <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
        </div>
        <div className='new-players'>
          <CSSTransitionGroup
            transitionName="fade"
            transitionEnterTimeout={500}
            transitionLeaveTimeout={300}>
            {newPlayersSpan}
          </CSSTransitionGroup>
        </div>
        <div className='connection-status'>
          <span>{this.props.connectionStatus}</span>
        </div>
      </header>
    );
  }
}

module.exports = Header;