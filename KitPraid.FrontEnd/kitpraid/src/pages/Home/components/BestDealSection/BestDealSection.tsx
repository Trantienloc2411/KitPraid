import { Box, Grid, GridItem, Heading, HStack, Link, SimpleGrid, Text, VStack } from "@chakra-ui/react";
import { FaArrowRight } from "react-icons/fa";
import CountdownTimer from "../../../../components/CountdownTimer/CountdownTimer";
import ProductCard from "../../../../components/ProductCard/ProductCard";

const BestDealSection = ({products, targetDate}: {products: any[], targetDate: Date}) => {
    
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
    );
};

export default BestDealSection;