var React = require('react');
var CSSTransitionGroup = require('react-transition-group/CSSTransitionGroup');
var Player = require('./player');

class PlayerList extends React.Component {
  render() {
  var props = this.props || {},
    players = props.players || [],
    playersMap = players.map(function (player) {
      return <Player player={player} key={player.name} />;
    });

  return (
    <div>
      <h6 className='player-header'>Players</h6>
      <CSSTransitionGroup
        transitionName="fade"
        transitionEnterTimeout={500}
        transitionLeaveTimeout={300}>
        {playersMap}
      </CSSTransitionGroup>
    </div>
  );
}
}

module.exports = PlayerList;