import React, { useState } from "react";

const NewPlayer = ({ connect }) => {
  const [playerName, setPlayerName] = useState("");

  const onSubmit = (event) => {
    event.preventDefault();
    connect(playerName);
  };

  const onPlayerNameChange = (event) => {
    setPlayerName(event.target.value);
  };

  return (
    <div className="new-player">
      <label htmlFor="playerName">Enter your name: </label>
      <form onSubmit={onSubmit}>
        <input
          id="playerName"
          name="playerName"
          type="text"
          className="textbox form-item"
          onChange={onPlayerNameChange}
          value={playerName}
        />
        <input type="submit" value="Connect" className="button form-item" />
      </form>
    </div>
  );
};

export default NewPlayer;
