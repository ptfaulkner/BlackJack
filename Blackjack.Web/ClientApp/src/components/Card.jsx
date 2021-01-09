import React from "react";

const Card = (props) => {
  const cardDirectory = "cardsvgs/";
  const svgName = `${cardDirectory}${props.number}_of_${props.suit}.svg`;
  const stack = props.index ? "card stack" : "card";

  return (
    <div className={stack}>
      <object className="card" data={svgName} type="image/svg+xml">
        <span>
          {props.number} - {props.suit}
        </span>
      </object>
    </div>
  );
};

export default Card;
