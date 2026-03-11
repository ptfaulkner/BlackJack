import BlankCard from "./BlankCard";

const Card = ({ suit, number, index }) => {
  if (!suit || !number) {
    return <BlankCard index={index} />;
  }

  const cardDirectory = "cardsvgs/";
  const svgName = `${cardDirectory}${number.toLowerCase()}_of_${suit.toLowerCase()}.svg`;
  const stack = index ? "card stack" : "card";

  return (
    <div className={stack}>
      <object data={svgName} type="image/svg+xml" style={{ width: '100%', height: '100%' }}>
        <span>
          {number} - {suit}
        </span>
      </object>
    </div>
  );
};

export default Card;
