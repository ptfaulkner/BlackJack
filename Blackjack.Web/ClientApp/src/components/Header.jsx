import { useRef } from "react";
import { TransitionGroup, CSSTransition } from "react-transition-group";

const Header = ({ newPlayers, connectionStatus }) => {
  const noPlayersRef = useRef(null);
  const playersRef = useRef(null);
  const newPlayersArray = newPlayers || [];
  const newPlayersString = newPlayersArray.join(", ");
  let newPlayersSpan = (
    <CSSTransition nodeRef={noPlayersRef} classNames="fade" timeout={500} key="no-new-players">
      <span ref={noPlayersRef} />
    </CSSTransition>
  );

  if (newPlayers.length) {
    newPlayersSpan = (
      <CSSTransition nodeRef={playersRef} classNames="fade" timeout={500} key="new-players">
        <span ref={playersRef}>Joining now: {newPlayersString}</span>
      </CSSTransition>
    );
  }

  return (
    <header>
      <div className="title">
        <span>Blackjack &spades;&diams;&hearts;&clubs;</span>
      </div>
      <div className="new-players">
        <TransitionGroup>{newPlayersSpan}</TransitionGroup>
      </div>
      <div className="connection-status">
        <span>{connectionStatus}</span>
      </div>
    </header>
  );
};

export default Header;
