import React, { useState } from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import "./styles/blackjack.css";
import NewPlayer from "./components/NewPlayer";
import GameWidget from "./components/GameWidget";
import Header from "./components/Header";
let connection;

const App = () => {
  const [connectionStatus, setConnectionStatus] = useState("Not Connected");
  const [playerName, setPlayerName] = useState("");
  const [game, setGame] = useState({});

  const connect = (playerName) => {
    connection = new HubConnectionBuilder().withUrl("/blackjackhub").build();

    connection.start().then(() => {
      setConnectionStatus("Connected");
      setPlayerName(playerName);
      connection.invoke("JoinGame", playerName);
    });

    connection.on("GameUpdate", (gameData) => {
      setGame(gameData);
    });
  };

  const doGameAction = (actionString) => {
    connection.invoke("SendGameAction", actionString);
  };

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
