import {
  FaFacebook,
  FaInstagram,
  FaTwitter,
  FaYoutube,
  FaLinkedin,
  FaShoppingCart,
  FaSearch,
  FaChevronRight,
  FaChevronDown,
  FaHeadphones,
  FaQuestion,
  FaTruck,
  FaBalanceScale,
  FaPhone,
} from "react-icons/fa";
import { FaHeart, FaUser } from "react-icons/fa6";
import Input from "./Input";
import Button from "./Button";
import LanguageSwitcher from "./LanguageSwitcher";
import { useTranslation } from "react-i18next";
import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import defaultCategories from "../data/categories";
import { authService } from "../services/auth";
const normalizeCategoryKey = (value = "") =>
  value
    .toString()
    .trim()
    .toLowerCase()
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/^-+|-+$/g, "");
const Navigation = ({ categoriesData }) => {
  const { t } = useTranslation();
  // const navigate = useNavigate();
  const categoriesView = useMemo(() => {
    if (Array.isArray(categoriesData) && categoriesData.length > 0) {
      return categoriesData;
    }
    return defaultCategories;
  }, [categoriesData]);
  const [isMobile, setIsMobile] = useState(false);
  const [menuOpen, setMenuOpen] = useState(false);
  const [categoryOpen, setCategoryOpen] = useState(false);
  const [activeCategoryId, setActiveCategoryId] = useState(
    () => categoriesView[0]?.Id ?? null
  );
  const [activeSubcategoryId, setActiveSubcategoryId] = useState(
    () => categoriesView[0]?.categoriesSub1?.[0]?.Id ?? null
  );
  const categoryRef = useRef(null);
  useEffect(() => {
    const onResize = () => setIsMobile(window.innerWidth < 768);
    onResize();
    window.addEventListener("resize", onResize);
    return () => window.removeEventListener("resize", onResize);
  }, []);
  useEffect(() => {
    if (!menuOpen) {
      setCategoryOpen(false);
    }
  }, [menuOpen]);
  useEffect(() => {
    if (!categoryOpen) {
      return;
    }
    const handleClickOutside = (event) => {
      if (categoryRef.current && !categoryRef.current.contains(event.target)) {
        setCategoryOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [categoryOpen]);
  useEffect(() => {
    if (!categoriesView.some((cat) => cat.Id === activeCategoryId)) {
      setActiveCategoryId(categoriesView[0]?.Id ?? null);
      return;
    }
    const selectedCategory =
      categoriesView.find((cat) => cat.Id === activeCategoryId) ??
      categoriesView[0];
    if (
      !selectedCategory?.categoriesSub1?.some(
        (sub) => sub.Id === activeSubcategoryId
      )
    ) {
      setActiveSubcategoryId(selectedCategory?.categoriesSub1?.[0]?.Id ?? null);
    }
  }, [categoriesView, activeCategoryId, activeSubcategoryId]);
  const selectedCategory =
    categoriesView.find((cat) => cat.Id === activeCategoryId) ??
    categoriesView[0];
  const subCategories = selectedCategory?.categoriesSub1 ?? [];
  const selectedSubcategory =
    subCategories.find((sub) => sub.Id === activeSubcategoryId) ??
    subCategories[0];
  const subSubCategories = selectedSubcategory?.categoriesSub2 ?? [];
  const translateCategoryLabel = useCallback(
    (label, level = "leaf") => {
      if (!label) return "";
      const prefixMap = {
        root: "navigation.categoriesLevel0",
        sub: "navigation.categoriesLevel1",
        leaf: "navigation.categoriesLevel2",
      };
      const prefix = prefixMap[level] ?? prefixMap.leaf;
      const key = `${prefix}.${normalizeCategoryKey(label)}`;
      const translated = t(key, { defaultValue: label });
      return translated || label;
    },
    [t]
  );
  const renderCategoriesDropdown = () => {
    if (!categoryOpen || categoriesView.length === 0) {
      return null;
    }
    return (
      <div
        style={{
          position: isMobile ? "relative" : "absolute",
          left: 0,
          top: isMobile ? "0.75rem" : "calc(100% + 0.5rem)",
          width: isMobile ? "100%" : "min(720px, 90vw)",
          background: "white",
          border: "1px solid #E5E7EB",
          borderRadius: 12,
          padding: "1.25rem",
          boxShadow: isMobile
            ? "0 6px 20px rgba(15, 23, 42, 0.08)"
            : "0 16px 40px rgba(15, 23, 42, 0.1)",
          zIndex: 120,
        }}
      >
        <div
          style={{
            display: "grid",
            gridTemplateColumns: isMobile ? "1fr" : "200px 220px 1fr",
            gap: "1.5rem",
          }}
        >
          <div>
            <div
              style={{
                fontSize: "0.8rem",
                marginBottom: "0.75rem",
                textTransform: "uppercase",
                color: "#6B7280",
                letterSpacing: "0.08em",
              }}
            >
              {t("navigation.categories")}
            </div>
            <ul
              style={{
                margin: 0,
                padding: 0,
                listStyle: "none",
                display: "flex",
                flexDirection: "column",
                gap: "0.25rem",
              }}
            >
              {categoriesView.map((category) => {
                const isActive = category.Id === activeCategoryId;
                return (
                  <li
                    key={category.Id}
                    style={{
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "space-between",
                      gap: "0.5rem",
                      padding: "0.5rem 0.75rem",
                      borderRadius: 8,
                      backgroundColor: isActive ? "#EEF5FF" : "transparent",
                      color: isActive ? "#1B6392" : "#111827",
                      fontWeight: isActive ? 600 : 500,
                      cursor: "pointer",
                      transition: "background-color 0.2s ease, color 0.2s ease",
                    }}
                    onMouseEnter={
                      !isMobile
                        ? () => setActiveCategoryId(category.Id)
                        : undefined
                    }
                    onClick={
                      isMobile
                        ? () => setActiveCategoryId(category.Id)
                        : undefined
                    }
                  >
                    <span>{translateCategoryLabel(category.Name, "root")}</span>
                    {!isMobile && isActive && (
                      <FaChevronRight style={{ fontSize: "0.75rem" }} />
                    )}
                  </li>
                );
              })}
            </ul>
          </div>
          <div>
            <div
              style={{
                fontSize: "0.8rem",
                marginBottom: "0.75rem",
                textTransform: "uppercase",
                color: "#6B7280",
                letterSpacing: "0.08em",
              }}
            >
              {translateCategoryLabel(selectedCategory?.Name, "root")}
            </div>
            <ul
              style={{
                margin: 0,
                padding: 0,
                listStyle: "none",
                display: "flex",
                flexDirection: "column",
                gap: "0.25rem",
              }}
            >
              {subCategories.length > 0 ? (
                subCategories.map((subcategory) => {
                  const isActive = subcategory.Id === activeSubcategoryId;
                  return (
                    <li
                      key={subcategory.Id}
                      style={{
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "space-between",
                        gap: "0.5rem",
                        padding: "0.5rem 0.75rem",
                        borderRadius: 8,
                        backgroundColor: isActive ? "#E0F2FE" : "transparent",
                        color: isActive ? "#0C4A6E" : "#111827",
                        fontWeight: isActive ? 600 : 500,
                        cursor: "pointer",
                        transition:
                          "background-color 0.2s ease, color 0.2s ease",
                      }}
                      onMouseEnter={
                        !isMobile
                          ? () => setActiveSubcategoryId(subcategory.Id)
                          : undefined
                      }
                      onClick={
                        isMobile
                          ? () => setActiveSubcategoryId(subcategory.Id)
                          : undefined
                      }
                    >
                      <span>
                        {translateCategoryLabel(subcategory.Name, "sub")}
                      </span>
                      {!isMobile && isActive && (
                        <FaChevronRight style={{ fontSize: "0.65rem" }} />
                      )}
                    </li>
                  );
                })
              ) : (
                <li style={{ color: "#6B7280", fontSize: "0.9rem" }}>
                  {t("navigation.noCategories", "No sub categories")}
                </li>
              )}
            </ul>
          </div>
          <div>
            <div
              style={{
                fontSize: "0.8rem",
                marginBottom: "0.75rem",
                textTransform: "uppercase",
                color: "#6B7280",
                letterSpacing: "0.08em",
              }}
            >
              {translateCategoryLabel(selectedSubcategory?.Name, "sub")}
            </div>
            <ul
              style={{
                margin: 0,
                padding: 0,
                listStyle: "none",
                display: "grid",
                gridTemplateColumns: isMobile
                  ? "repeat(2, minmax(0, 1fr))"
                  : "repeat(2, minmax(0, 1fr))",
                gap: "0.5rem",
              }}
            >
              {subSubCategories.length > 0 ? (
                subSubCategories.map((leaf) => (
                  <li
                    key={leaf.Id}
                    style={{
                      padding: "0.6rem 0.75rem",
                      borderRadius: 8,
                      backgroundColor: "#F9FAFB",
                      color: "#111827",
                      fontWeight: 500,
                      cursor: "pointer",
                      border: "1px solid #E5E7EB",
                      transition:
                        "background-color 0.2s ease, color 0.2s ease, border-color 0.2s ease",
                    }}
                  >
                    {translateCategoryLabel(leaf.Name, "leaf")}
                  </li>
                ))
              ) : (
                <li style={{ color: "#6B7280", fontSize: "0.9rem" }}>
                  {t("navigation.noCategories", "No sub categories")}
                </li>
              )}
            </ul>
          </div>
        </div>
      </div>
    );
  };
  return (
    <>
      <nav
        className="navigation-header-top"
        style={{
          padding: "0.8rem clamp(1rem, 6vw, 10rem)",
          backgroundColor: "#1B6392",
          color: "white",
        }}
      >
        <ul
          className="navigation-header-list"
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            textDecoration: "none",
            listStyleType: "none",
            flexWrap: "wrap",
            gap: "0.5rem",
          }}
        >
          <li
            className="navigation-header-item-welcome"
            style={{ textDecoration: "none", listStyleType: "none" }}
          >
            {t("navigation.welcomeMessage")}
          </li>
          <li className="navigation-header-item-socials">
            <div style={{ marginRight: "0.5rem" }}>
              {t("navigation.followUs")}{" "}
            </div>
            <div
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                paddingTop: "0.3rem",
                gap: "0.5rem",
                flexWrap: "wrap",
              }}
            >
              <a
                href="https://www.facebook.com/kitpraid"
                target="_blank"
                rel="noopener noreferrer"
              >
                <FaFacebook />
              </a>
              <a
                href="https://www.instagram.com/kitpraid"
                target="_blank"
                rel="noopener noreferrer"
              >
                <FaInstagram />
              </a>
              <a
                href="https://www.twitter.com/kitpraid"
                target="_blank"
                rel="noopener noreferrer"
              >
                <FaTwitter />
              </a>
              <a
                href="https://www.youtube.com/kitpraid"
                target="_blank"
                rel="noopener noreferrer"
              >
                <FaYoutube />
              </a>
              <a
                href="https://www.linkedin.com/kitpraid"
                target="_blank"
                rel="noopener noreferrer"
              >
                <FaLinkedin />
              </a>
            </div>
          </li>
        </ul>
      </nav>
      <nav
        className="navigation-header-middle"
        style={{
          padding: "1.3rem clamp(1rem, 6vw, 10rem)",
          backgroundColor: "#1B6392",
          color: "white",
          borderTop: "1px solid #2DA5F3",
        }}
      >
        <ul
          className="navigation-header-middle-list"
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            textDecoration: "none",
            listStyleType: "none",
            flexWrap: "wrap",
            gap: "1rem",
          }}
        >
          <li className="navigation-header-middle-item-logo">
            <a
              href="/"
              style={{ fontSize: "1.5rem", fontWeight: "bold", color: "white" }}
            >
              KITPRAID
            </a>
          </li>
          <li
            className="navigation-header-middle-item-search"
            style={{ flex: "1 1 320px", minWidth: "260px" }}
          >
            <Input
              style={{ width: "100%" }}
              type="text"
              placeholder={t("form.search")}
              hasBackground={true}
              hasBorder={true}
              hasBackgroundTint={true}
              rightIcon={<FaSearch />}
            />
          </li>
          <li
            className="navigation-header-middle-item-language"
            style={{ display: "flex", alignItems: "center", gap: "0.5rem" }}
          >
            <LanguageSwitcher size="sm" />
          </li>
          <li className="navigation-header-middle-item-user">
            <ul
              className="navigation-header-middle-item-user-list"
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                textDecoration: "none",
                listStyleType: "none",
                fontSize: "clamp(1.2rem, 3vw, 1.6rem)",
                gap: "1rem",
              }}
              onClick={() => {}}
            >
              <a href="/cart">
                <FaShoppingCart />
              </a>
              <a href="/wishlist">
                <FaHeart />
              </a>
              <a
                href="#"
                onClick={(e) => {
                  e.preventDefault();
                  authService.redirectToLogin();
                }}
              >
                <FaUser />
              </a>
            </ul>
          </li>
        </ul>
      </nav>

      <nav
        className="navigation-header-bottom"
        style={{
          padding: "0.8rem clamp(0.5rem, 5vw, 9rem)",
          backgroundColor: "white",
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          flexWrap: "wrap",
          gap: "0.75rem",
          position: "relative",
        }}
      >
        {(() => {
          const menuItems = (
            <ul
              className="navigation-header-bottom-list"
              style={{
                display: "flex",
                flexDirection: isMobile ? "column" : "row",
                alignItems: isMobile ? "stretch" : "center",
                justifyContent: isMobile ? "flex-start" : "space-between",
                textDecoration: "none",
                listStyleType: "none",
                flexWrap: isMobile ? "nowrap" : "wrap",
                gap: isMobile ? "0.75rem" : "0.5rem",
              }}
            >
              <li
                ref={categoryRef}
                className="navigation-header-bottom-item-category"
                style={{
                  position: "relative",
                  width: isMobile ? "100%" : "auto",
                }}
              >
                <Button
                  label={t("navigation.categories")}
                  size="sm"
                  hasBackground={false}
                  hasBorder={false}
                  color="black"
                  rightIcon={<FaChevronDown />}
                  onClick={() => setCategoryOpen((prev) => !prev)}
                  style={isMobile ? { width: "100%" } : undefined}
                />
                {renderCategoriesDropdown()}
              </li>
              <li className="navigation-header-bottom-item-brands">
                <Button
                  label={t("navigation.trackOrders")}
                  size="sm"
                  hasBackground={false}
                  hasBorder={false}
                  color="black"
                  leftIcon={<FaTruck />}
                  style={isMobile ? { width: "100%" } : undefined}
                />
              </li>
              <li className="navigation-header-bottom-item-new-products">
                <Button
                  label={t("navigation.compareProducts")}
                  size="sm"
                  hasBackground={false}
                  hasBorder={false}
                  color="black"
                  leftIcon={<FaBalanceScale />}
                  style={isMobile ? { width: "100%" } : undefined}
                />
              </li>
              <li className="navigation-header-bottom-item-contact-us">
                <Button
                  label={t("navigation.contactSupport")}
                  size="sm"
                  hasBackground={false}
                  hasBorder={false}
                  color="black"
                  leftIcon={<FaHeadphones />}
                  style={isMobile ? { width: "100%" } : undefined}
                />
              </li>
              <li className="navigation-header-bottom-item-help">
                <Button
                  label={t("navigation.needHelp")}
                  size="sm"
                  hasBackground={false}
                  hasBorder={false}
                  color="black"
                  leftIcon={<FaQuestion />}
                  style={isMobile ? { width: "100%" } : undefined}
                />
              </li>
            </ul>
          );
          if (!isMobile) {
            return (
              <>
                <div
                  className="navigation-header-bottom-left"
                  style={{
                    display: "flex",
                    alignItems: "flex-start",
                    justifyContent: "flex-start",
                    position: "relative",
                    gap: "0.5rem",
                  }}
                >
                  {menuItems}
                </div>
                <div
                  className="navigation-header-bottom-right"
                  style={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                  }}
                >
                  <ul
                    className="navigation-header-bottom-right-list"
                    style={{
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "space-between",
                      textDecoration: "none",
                      listStyleType: "none",
                      gap: "0.5rem",
                      flexWrap: "wrap",
                    }}
                  >
                    <li style={{ fontSize: "clamp(0.9rem, 2.5vw, 1rem)" }}>
                      <FaPhone style={{ marginRight: "0.5rem" }} />
                      <span style={{ fontWeight: "bold" }}>
                        +234 812 345 6789
                      </span>
                    </li>
                  </ul>
                </div>
              </>
            );
          }
          return (
            <div
              style={{
                width: "100%",
                display: "flex",
                alignItems: "center",
                justifyContent: "space-between",
                gap: "0.75rem",
              }}
            >
              <Button
                label={t("navigation.menu")}
                size="sm"
                hasBackground={false}
                hasBorder
                variant="secondary"
                rightIcon={<FaChevronDown />}
                onClick={() => setMenuOpen((prev) => !prev)}
                style={{ flexShrink: 0 }}
              />
              <div style={{ position: "relative", width: "100%" }}>
                {menuOpen && (
                  <div
                    style={{
                      position: "relative",
                      zIndex: 60,
                      background: "white",
                      border: "1px solid #E5E7EB",
                      borderRadius: 8,
                      padding: "0.75rem",
                      width: "100%",
                      boxShadow: "0 12px 32px rgba(15, 23, 42, 0.12)",
                    }}
                  >
                    {menuItems}
                  </div>
                )}
              </div>
              <div className="navigation-header-bottom-right">
                <span style={{ fontSize: "0.9rem", fontWeight: "bold" }}>
                  <FaPhone style={{ marginRight: "0.5rem" }} />
                  +234 812 345 6789
                </span>
              </div>
            </div>
          );
        })()}
      </nav>
    </>
  );
};

export default Navigation;
