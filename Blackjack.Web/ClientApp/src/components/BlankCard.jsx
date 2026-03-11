const BlankCard = ({ index }) => {
  const stack = index ? "card stack" : "card";

  return (
    <div className={stack}>
      <object data="cardsvgs/card_back.svg" type="image/svg+xml" style={{ width: '100%', height: '100%' }}>
        <div className="card blank-card">
          <div className="blank-card-inner"></div>
        </div>
      </object>
    </div>
  );
};

export default BlankCard;
