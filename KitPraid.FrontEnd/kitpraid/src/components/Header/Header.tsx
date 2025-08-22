import React, { useState, useEffect } from 'react';
import { FaUser, FaChevronDown, FaBars } from 'react-icons/fa';
import './Header.css';

const Header: React.FC = () => {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 50);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const closeDropdown = () => {
    setIsDropdownOpen(false);
  };

  return (
    <header className={`header ${isScrolled ? 'scrolled' : ''}`}>
      <div className="header-content">
        {/* Logo */}
        <a href="/" className="logo">
          KitPraid
        </a>

        {/* Navigation */}
        <nav className="nav">
          <a href="/store" className="nav-link">Store</a>
          <a href="/category" className="nav-link">Category</a>
          <a href="/contact" className="nav-link">Contact</a>
          <a href="/about" className="nav-link">About Us</a>
        </nav>

        {/* User Section */}
        <div className="user-section">
          <div className="dropdown">
            <button 
              className="user-button" 
              onClick={toggleDropdown}
              onBlur={closeDropdown}
            >
              <FaUser className="user-icon" />
              <span>User</span>
              <FaChevronDown className="user-icon" />
            </button>
            
            {isDropdownOpen && (
              <div className="dropdown-menu show">
                <a href="/login" className="dropdown-item">Đăng nhập</a>
                <a href="/register" className="dropdown-item">Đăng ký</a>
              </div>
            )}
          </div>
        </div>

        {/* Mobile Menu Toggle */}
        <button className="mobile-menu-toggle">
          <FaBars />
        </button>
      </div>
    </header>
  );
};

export default Header;
