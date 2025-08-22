import React from 'react';
import { VStack, SimpleGrid, Box, Heading, Text, HStack, Link, Grid, GridItem } from '@chakra-ui/react';
import HeroSection from '../../components/HeroSection';
import ProductCard from '../../components/ProductCard';
import CountdownTimer from '../../components/CountdownTimer/CountdownTimer';
import './Home.css';
import { FaArrowRight } from 'react-icons/fa';

const Home: React.FC = () => {
  // Set target date to 3 days from now
  const targetDate = new Date();
  targetDate.setDate(targetDate.getDate() + 3);

  console.log('Home component - Target date set to:', targetDate);

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

  const handleAddToCart = (id: string) => {
    console.log('Added to cart:', id);
  };

  const handleAddToWishlist = (id: string) => {
    console.log('Added to wishlist:', id);
  };

  const handleQuickView = (id: string) => {
    console.log('Quick view:', id);
  };

  return (
    <div className="home-page">
      <HeroSection {...heroData} />

      {/* Featured Products Section */}
      <section className="featured-products">
        <Box maxW="1200px" mx="auto">
          <VStack gap={8} mb={12}>
            <Heading size="2xl" textAlign="center" color="gray.800">
              Featured Products
            </Heading>
            <Text fontSize="lg" color="gray.600" textAlign="center" maxW="600px">
              Discover our handpicked selection of premium products that combine quality, style, and innovation.
            </Text>
          </VStack>

          <SimpleGrid columns={{ base: 1, md: 2, lg: 4 }} gap={8}>
            {products.map((product) => (
              <ProductCard
                key={product.id}
                {...product}
                onAddToCart={handleAddToCart}
                onAddToWishlist={handleAddToWishlist}
                onQuickView={handleQuickView}
              />
            ))}
          </SimpleGrid>
        </Box>
      </section>

      {/* Best Deals Section */}
      {/* Best Deals Section */}
      <section className="best-deals">
        <Box maxW="1200px" mx="auto" alignContent={"center"}>
          <VStack gap={8} mb={4} align="flex-start">
            <HStack justify="flex-start" w="100%" align="center">
              <Heading size="2xl" pr="0.4rem" fontWeight="bold" color="gray.800">
                Best Deals
              </Heading>
              <CountdownTimer className="countdown-timer" targetDate={targetDate} />
              <Link ml="auto" gap={4} fontWeight="bold" href="/deals" color="blue.500" fontSize="lg">
                Browse All Deals <FaArrowRight />
              </Link>
            </HStack>
            <Text fontSize="lg" color="gray.600" textAlign="left">
              Discover our best deals on premium products that combine quality, style, and innovation.
            </Text>
          </VStack>

          {/* Layout: 1 big card (left) + 8 small cards (right) */}
          <Grid
            templateColumns={{ base: '1fr', md: 'repeat(5, 1fr)' }}
            gap={2}
            alignItems="stretch"
            autoRows="1fr"
          >
            {/* Left featured – spans 2 rows */}
            <GridItem colSpan={{ base: 1, md: 1 }} rowSpan={{ base: 1, md: 2 }}>
              <ProductCard
                {...products[0]}
                onAddToCart={handleAddToCart}
                onAddToWishlist={handleAddToWishlist}
                onQuickView={handleQuickView}
                className="h-full"
              />
            </GridItem>

            {/* Right grid – 4 cols × 2 rows */}
            <GridItem colSpan={{ base: 1, md: 4 }}>
              <SimpleGrid columns={{ base: 1, sm: 2, md: 4 }} gap={6}>
                {products.slice(1, 5).map(p => (
                  <ProductCard
                    key={p.id}
                    {...p}
                    onAddToCart={handleAddToCart}
                    onAddToWishlist={handleAddToWishlist}
                    onQuickView={handleQuickView}
                  />
                ))}
              </SimpleGrid>
            </GridItem>

            <GridItem colSpan={{ base: 1, md: 4 }}>
              <SimpleGrid columns={{ base: 1, sm: 2, md: 4 }} gap={6}>
                {products.slice(1, 5).map(p => (
                  <ProductCard
                    key={p.id}
                    {...p}
                    onAddToCart={handleAddToCart}
                    onAddToWishlist={handleAddToWishlist}
                    onQuickView={handleQuickView}
                  />
                ))}
              </SimpleGrid>
            </GridItem>
          </Grid>
        </Box>
      </section>
    </div>
  );
};

export default Home;
