import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
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
        <Link to="/" className="logo">
          KitPraid
        </Link>

        {/* Navigation */}
        <nav className="nav">
          <Link to="/store" className="nav-link">Store</Link>
          <Link to="/category" className="nav-link">Category</Link>
          <Link to="/contact" className="nav-link">Contact</Link>
          <Link to="/about" className="nav-link">About Us</Link>
        </nav>

        {/* User Section */}
        <div className="user-section">
          <div className="dropdown">
         
            <button
              className="user-button"
              onClick={toggleDropdown}
            
            
            >
              <FaUser className="user-icon" />
              <span>User</span>
              <FaChevronDown className="user-icon" />
            </button>


            {isDropdownOpen && (
              <div className="dropdown-menu show">
                <Link
                  to="/auth/signin"
                  className="dropdown-item"
                  onClick={(e) => {
                    console.log('Sign in link clicked!', e); // Debug line
                    console.log('Current location:', window.location.pathname); // Debug line
                    closeDropdown();
                  }}
                >
                  Đăng nhập
                </Link>
                <Link
                  to="/auth/signup"
                  className="dropdown-item"
                  onClick={closeDropdown}
                >
                  Đăng ký
                </Link>
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
