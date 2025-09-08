import React, { useState, useRef, useEffect } from 'react';
import { FaChevronLeft, FaChevronRight } from 'react-icons/fa';
import './CategoryListSection.css';

interface Category {
  id: string;
  name: string;
  image: string;
  description?: string;
}

interface CategoryListSectionProps {
  categories: Category[];
  title?: string;
  itemsPerView?: number;
}

export const CategoryListSection: React.FC<CategoryListSectionProps> = ({
  categories,
  title = "Shop with Categories",
  itemsPerView = 6
}) => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [canScrollLeft, setCanScrollLeft] = useState(false);
  const [canScrollRight, setCanScrollRight] = useState(true);
  const carouselRef = useRef<HTMLDivElement>(null);

  const maxIndex = Math.max(0, categories.length - itemsPerView);

  const updateScrollButtons = () => {
    if (carouselRef.current) {
      const { scrollLeft, scrollWidth, clientWidth } = carouselRef.current;
      setCanScrollLeft(scrollLeft > 0);
      setCanScrollRight(scrollLeft < scrollWidth - clientWidth - 1);
    }
  };

  useEffect(() => {
    updateScrollButtons();
    const carousel = carouselRef.current;
    if (carousel) {
      carousel.addEventListener('scroll', updateScrollButtons);
      return () => carousel.removeEventListener('scroll', updateScrollButtons);
    }
  }, [categories]);

  const scrollToIndex = (index: number) => {
    if (carouselRef.current) {
      const itemWidth = carouselRef.current.clientWidth / itemsPerView;
      carouselRef.current.scrollTo({
        left: index * itemWidth,
        behavior: 'smooth'
      });
      setCurrentIndex(index);
    }
  };

  const scrollLeft = () => {
    const newIndex = Math.max(0, currentIndex - 1);
    scrollToIndex(newIndex);
  };

  const scrollRight = () => {
    const newIndex = Math.min(maxIndex, currentIndex + 1);
    scrollToIndex(newIndex);
  };

  const handleWheel = (e: React.WheelEvent) => {
    e.preventDefault();
    if (e.deltaY > 0) {
      scrollRight();
    } else {
      scrollLeft();
    }
  };

  if (!categories || categories.length === 0) {
    return null;
  }

  return (
    <section className="category-list-section">
      <div className="category-list-container">
        <h2 className="category-list-title">{title}</h2>
        
        <div className="category-carousel-wrapper">
          {/* Left Navigation Arrow */}
          <button
            className={`carousel-nav-btn carousel-nav-left ${!canScrollLeft ? 'disabled' : ''}`}
            onClick={scrollLeft}
            disabled={!canScrollLeft}
            aria-label="Previous categories"
          >
            <FaChevronLeft />
          </button>

          {/* Carousel Container */}
          <div 
            className="category-carousel"
            ref={carouselRef}
            onWheel={handleWheel}
          >
            <div className="category-carousel-track">
              {categories.map((category) => (
                <div key={category.id} className="category-card">
                  <div className="category-image-container">
                    <img 
                      src={category.image} 
                      alt={category.name} 
                      className="category-image"
                      loading="lazy"
                    />
                  </div>
                  <h3 className="category-name">{category.name}</h3>
                  {category.description && (
                    <p className="category-description">{category.description}</p>
                  )}
                </div>
              ))}
            </div>
          </div>

          {/* Right Navigation Arrow */}
          <button
            className={`carousel-nav-btn carousel-nav-right ${!canScrollRight ? 'disabled' : ''}`}
            onClick={scrollRight}
            disabled={!canScrollRight}
            aria-label="Next categories"
          >
            <FaChevronRight />
          </button>
        </div>

        {/* Dots Indicator */}
        {maxIndex > 0 && (
          <div className="carousel-dots">
            {Array.from({ length: maxIndex + 1 }, (_, index) => (
              <button
                key={index}
                className={`carousel-dot ${index === currentIndex ? 'active' : ''}`}
                onClick={() => scrollToIndex(index)}
                aria-label={`Go to slide ${index + 1}`}
              />
            ))}
          </div>
        )}
      </div>
    </section>
  );
};