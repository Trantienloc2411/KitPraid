import React from 'react';
import HeroSection from './components/HeroSection';
import './Home.css';
import FeatureSection from './components/FeatureSection/FeatureSection';
import BestDealSection from './components/BestDealSection/BestDealSection';
import { CategoryListSection } from './components/CategoryListSection';

const Home: React.FC = () => {
  // Set target date to 3 days from now
  const targetDate = new Date();
  targetDate.setDate(targetDate.getDate() + 3);

   

  // Mock data for demo
  const heroData = {
    title: "Discover Amazing Products",
    subtitle: "Welcome to KitPraid",
    description: "Explore our curated collection of premium products with the best prices and quality. Shop smart, live better.",
    primaryButtonText: "Shop Now",
    secondaryButtonText: "Learn More",
    backgroundImage: "https://images.unsplash.com/photo-1441986300917-64674bd600d8?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80"
  };

  const products = [
    {
      id: "1",
      name: "Wireless Bluetooth Headphones",
      price: 299000,
      originalPrice: 399000,
      image: "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80",
      rating: 4.5,
      reviewCount: 128,
      discount: 25,
      isNew: true
    },
    {
      id: "2",
      name: "Smart Fitness Watch",
      price: 899000,
      image: "https://images.unsplash.com/photo-1523275335684-37898b6baf30?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80",
      rating: 4.8,
      reviewCount: 89,
      isSale: true
    },
    {
      id: "3",
      name: "Portable Bluetooth Speaker",
      price: 199000,
      originalPrice: 299000,
      image: "https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80",
      rating: 4.3,
      reviewCount: 156,
      discount: 33
    },
    {
      id: "4",
      name: "Wireless Charging Pad",
      price: 149000,
      image: "https://images.unsplash.com/photo-1586953208448-b95a79798f07?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80",
      rating: 4.6,
      reviewCount: 72
    }
  ];

  const categories = [
    {
      id: '1',
      name: 'Computer & Laptop',
      image: 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=300&h=200&fit=crop',
      description: 'High-performance computers and laptops'
    },
    {
      id: '2',
      name: 'SmartPhone',
      image: 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=300&h=200&fit=crop',
      description: 'Latest smartphones and mobile devices'
    },
    {
      id: '3',
      name: 'Headphones',
      image: 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=300&h=200&fit=crop',
      description: 'Premium audio headphones and earbuds'
    },
    {
      id: '4',
      name: 'Accessories',
      image: 'https://images.unsplash.com/photo-1541807084-5c52b6b3adef?w=300&h=200&fit=crop',
      description: 'Essential tech accessories and peripherals'
    },
    {
      id: '5',
      name: 'Camera & Photo',
      image: 'https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=300&h=200&fit=crop',
      description: 'Professional cameras and photography gear'
    },
    {
      id: '6',
      name: 'TV & Homes',
      image: 'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=300&h=200&fit=crop',
      description: 'Smart TVs and home entertainment systems'
    },
    {
      id: '7',
      name: 'Gaming',
      image: 'https://images.unsplash.com/photo-1542751371-adc38448a05e?w=300&h=200&fit=crop',
      description: 'Gaming consoles and accessories'
    },
    {
      id: '8',
      name: 'Wearables',
      image: 'https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=300&h=200&fit=crop',
      description: 'Smartwatches and fitness trackers'
    }
  ];

  return (
    <div className="home-page">
      <HeroSection {...heroData} />
      <BestDealSection products={products} targetDate={targetDate} />
      <CategoryListSection categories={categories} />
      <FeatureSection products={products} />
    </div>
  );
};

export default Home;
