import React from "react";
import BlankCard from "./BlankCard";

const Card = ({ suit, number, index }) => {
  const cardDirectory = "cardsvgs/";
  const svgName = `${cardDirectory}${number}_of_${suit}.svg`;
  const stack = index ? "card stack" : "card";

  if (!suit && !number) {
    return <BlankCard index={index} />;
  }

  return (
    <div className={stack}>
      <object className="card" data={svgName} type="image/svg+xml">
        <span>
          {number} - {suit}
        </span>
      </object>
    </div>
  );
};

export default Card;
