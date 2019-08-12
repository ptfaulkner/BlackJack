const React = require('react');
const Player = require('./player');
const PlayerList = require('./playerList');
const CurrentPlayer = require('./currentPlayer');

class GameWidget extends React.Component {

  splitTablePlayers(players) {
    var playerLists = {
      left: [],
      right: []
    };

    playerLists.left = players.splice(0, 2);
    playerLists.right = players;
    return playerLists;
  }

  render() {
    var game = this.props.game || {},
      dealer = game.dealer || {},
      currentPlayer = game.player || {},
      players = game.tablePlayers || [],
      playerLists = this.splitTablePlayers(players);

    return (
      <div className='game-container'>
        <div className='player-list'>
          <PlayerList players={playerLists.left}
            gameStatus={game.gameStatus}
          />
        </div>
        <div className='game-area'>
          <Player player={dealer} />
          <CurrentPlayer player={currentPlayer}
            gameStatus={game.gameStatus}
            doGameAction={this.props.doGameAction} />
        </div>
        <div className='player-list'>
          <PlayerList players={playerLists.right}
            gameStatus={game.gameStatus} />
        </div>
        <br className='clear-fix' />
      </div>
    );
  }
}

module.exports = GameWidget;