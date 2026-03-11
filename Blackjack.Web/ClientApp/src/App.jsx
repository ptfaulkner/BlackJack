import { useState, useRef, useCallback } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import "./styles/blackjack.css";
import NewPlayer from "./components/NewPlayer";
import GameWidget from "./components/GameWidget";
import Header from "./components/Header";

const App = () => {
  const [connectionStatus, setConnectionStatus] = useState("Not Connected");
  const [playerName, setPlayerName] = useState("");
  const [game, setGame] = useState({});
  const connectionRef = useRef(null);

  const connect = useCallback((name) => {
    const connection = new HubConnectionBuilder().withUrl("/blackjackhub").build();
    connectionRef.current = connection;

    connection.start().then(() => {
      setConnectionStatus("Connected");
      setPlayerName(name);
      connection.invoke("JoinGame", name);
    });

    connection.on("GameUpdate", (gameData) => {
      setGame(gameData);
    });
  }, []);

  const doGameAction = useCallback((actionString) => {
    connectionRef.current?.invoke("SendGameAction", actionString);
  }, []);

  const newPlayers = game?.newPlayers || [];

  return (
    <div>
      <Header connectionStatus={connectionStatus} newPlayers={newPlayers} />
      <br className="clear-fix" />
      <div className="game-widget">
        {connectionStatus !== "Connected" ? (
          <NewPlayer connect={connect} />
        ) : (
          <GameWidget
            game={game}
            currentPlayerName={playerName}
            doGameAction={doGameAction}
          />
        )}
      </div>
    </div>
  );
};

export default App;
