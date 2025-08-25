import { Box, Heading, SimpleGrid, Text, VStack } from "@chakra-ui/react"
import ProductCard from "../../../../components/ProductCard/ProductCard";

const handleAddToCart = (id: string) => {
    console.log('Added to cart:', id);
  };

  const handleAddToWishlist = (id: string) => {
    console.log('Added to wishlist:', id);
  };

  const handleQuickView = (id: string) => {
    console.log('Quick view:', id);
  };
const FeatureSection = ({products}: {products: any[]}) => {
    return (
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
    );
};

export default FeatureSection;