import React from "react";

const BlankCard = ({ index }) => {
  const stack = index ? "card stack" : "card";

  return (
    <div className={stack}>
      <div className="card blank-card">
        <div className="blank-card-inner"></div>
      </div>
    </div>
  );
};

export default BlankCard;
