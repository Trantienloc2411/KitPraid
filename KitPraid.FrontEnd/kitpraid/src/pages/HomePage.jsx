const HomePage = ({ onSubscribe, className = "", style = {} }) => {
  return (
    <div
      className={`homepage ${className}`}
      style={{
        width: "100%",
        ...style,
      }}
    >
      {/* Main content area - can be customized */}
      <div
        style={{
          width: "100%",
          minHeight: "calc(100vh - 200px)",
        }}
      >
        {/* Add your main content here */}
      </div>
    </div>
  );
};

export default HomePage;
