import {
  FaShippingFast,
  FaHeadphones,
  FaShieldAlt,
  FaMoneyBill,
} from "react-icons/fa";

const Feature = ({
  columns = { mobile: 1, tablet: 2, desktop: 4 },
  gap = 20,
  className = "",
  style = {},
}) => {
  const featuresList = [
    {
      icon: <FaShippingFast />,
      title: "Free Shipping",
      description: "Free shipping on all orders over $100",
    },
    {
      icon: <FaHeadphones />,
      title: "24/7 Support",
      description: "24/7 support by our team",
    },
    {
      icon: <FaShieldAlt />,
      title: "Secure Checkout",
      description: "Secure checkout with SSL encryption",
    },
    {
      icon: <FaMoneyBill />,
      title: "Money Back Guarantee",
      description: "100% money back guarantee",
    },
  ];

  const containerStyle = {
    width: "100%",
    maxWidth: "100%",
    padding: "clamp(1rem, 4vw, 2rem)",
    boxSizing: "border-box",
    ...style,
  };

  const itemStyle = {
    display: "flex",
    alignItems: "flex-start",
    justifyContent: "flex-start",
    gap: "clamp(8px, 2vw, 12px)",
    padding: "clamp(12px, 3vw, 20px)",
    borderRadius: "3px",
    width: "100%",
    boxSizing: "border-box",
  };

  const iconStyle = {
    fontSize: "clamp(24px, 5vw, 32px)",
    color: "#000",
    flexShrink: 0,
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    marginTop: "4px",
  };

  const contentStyle = {
    display: "flex",
    flexDirection: "column",
    justifyContent: "flex-start",
    alignItems: "flex-start",
    minWidth: 0,
    flex: 1,
  };

  const titleStyle = {
    fontSize: "clamp(16px, 3vw, 18px)",
    fontWeight: "bold",
    color: "#000",
    margin: 0,
    lineHeight: 1.3,
    wordBreak: "break-word",
  };

  const descriptionStyle = {
    fontSize: "clamp(13px, 2.5vw, 14px)",
    color: "#000",
    margin: 0,
    marginTop: "4px",
    lineHeight: 1.5,
    wordBreak: "break-word",
  };

  return (
    <>
      <style>{`
        .feature-container {
          display: grid;
          grid-template-columns: repeat(${columns.mobile || 1}, 1fr);
          gap: ${gap}px;
        }
        @media (min-width: 640px) {
          .feature-container {
            grid-template-columns: repeat(${columns.tablet || 2}, 1fr);
          }
        }
        @media (min-width: 1024px) {
          .feature-container {
            grid-template-columns: repeat(${columns.desktop || 4}, 1fr);
          }
        }
      `}</style>
      <div className={`feature-container ${className}`} style={containerStyle}>
        {featuresList.map((feature) => (
          <div className="feature-item" key={feature.title} style={itemStyle}>
            <div style={iconStyle}>{feature.icon}</div>
            <div style={contentStyle}>
              <h3 style={titleStyle}>{feature.title}</h3>
              <p style={descriptionStyle}>{feature.description}</p>
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default Feature;
