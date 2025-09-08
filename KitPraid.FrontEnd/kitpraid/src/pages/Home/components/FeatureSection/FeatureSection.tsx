import { 
  Box, 
  Heading, 
  SimpleGrid, 
  Text, 
  VStack, 
  Grid, 
  GridItem, 
  Button,
  
  Badge,
  Image,
  Flex,
  
} from "@chakra-ui/react"
import { FaArrowRight } from "react-icons/fa";
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
  // Sample promotional data
  const promotionalData = {
    title: "COMPUTER & ACCESSORIES",
    discount: "32% Discount",
    subtitle: "For all electronics products",
    endDate: "ENDS OF CHRISTMAS",
    buttonText: "SHOP NOW"
  };

  // Sample product categories
  const categories = [
    { name: "All Product", products: products.slice(0, 8) },
    { name: "Smart Phone", products: products.filter(p => p.category === "smartphone").slice(0, 8) },
    { name: "Laptop", products: products.filter(p => p.category === "laptop").slice(0, 8) },
    { name: "Headphone", products: products.filter(p => p.category === "headphone").slice(0, 8) },
    { name: "TV", products: products.filter(p => p.category === "tv").slice(0, 8) }
  ];

  // For now, show all products in the first category
  const currentProducts = categories[0].products;

  return (
    <section className="featured-products">
      <Box maxW="1200px" mx="auto" py={{ base: 8, md: 12 }} px={{ base: 4, md: 0 }}>
        <VStack gap={{ base: 6, md: 8 }} mb={{ base: 6, md: 8 }} align="flex-start">
          <Heading 
            size={{ base: "lg", md: "xl" }} 
            fontWeight="bold" 
            color="gray.800"
            textAlign={{ base: "center", md: "left" }}
            w={{ base: "100%", md: "auto" }}
          >
            Featured Products
          </Heading>
        </VStack>

        {/* Layout: Left promotional image + Right products */}
        <Grid
          templateColumns={{ base: '1fr', lg: '1fr 2fr' }}
          gap={{ base: 6, md: 8 }}
          alignItems="start"
        >
          {/* Left promotional section */}
          <GridItem>
            <Box
              bg="yellow.400"
              p={{ base: 6, md: 8 }}
              borderRadius="xl"
              position="relative"
              overflow="hidden"
              minH={{ base: "400px", md: "500px" }}
            >
              {/* Promotional content */}
              <VStack align="flex-start" gap={{ base: 4, md: 6 }} position="relative" zIndex={2}>
                <Badge 
                  colorScheme="orange" 
                  px={{ base: 2, md: 3 }} 
                  py={{ base: 1, md: 1 }} 
                  borderRadius="full"
                  fontSize={{ base: "2xs", md: "xs" }}
                >
                  {promotionalData.title}
                </Badge>
                
                <Heading 
                  size={{ base: "md", md: "lg" }} 
                  color="black" 
                  fontWeight="bold"
                  lineHeight="tight"
                >
                  {promotionalData.discount}
                </Heading>
                
                <Text 
                  fontSize={{ base: "sm", md: "md" }} 
                  color="black"
                  lineHeight="tight"
                >
                  {promotionalData.subtitle}
                </Text>
                
                <Box
                  bg="orange.200"
                  px={{ base: 3, md: 4 }}
                  py={{ base: 2, md: 2 }}
                  borderRadius="md"
                  color="black"
                  fontWeight="semibold"
                  fontSize={{ base: "xs", md: "sm" }}
                  textAlign="center"
                  w="fit-content"
                >
                  Offers ends in {promotionalData.endDate}
                </Box>
                
                <Button
                  colorScheme="orange"
                  size={{ base: "sm", md: "md" }}
                  _hover={{ transform: "translateY(-2px)" }}
                  transition="all 0.3s"
                  fontSize={{ base: "xs", md: "sm" }}
                  px={{ base: 3, md: 4 }}
                >
                  {promotionalData.buttonText} <FaArrowRight />
                </Button>
              </VStack>

              {/* Decorative electronics image */}
              <Box
                position="absolute"
                bottom={0}
                right={0}
                opacity={0.3}
                zIndex={1}
                display={{ base: "none", md: "block" }}
              >
                <Image
                  src="https://images.unsplash.com/photo-1468495244123-6c6c332eeece?w=300&h=300&fit=crop&crop=center"
                  alt="Electronics"
                  w={{ base: "150px", md: "200px" }}
                  h={{ base: "150px", md: "200px" }}
                  objectFit="cover"
                  borderRadius="lg"
                />
              </Box>
            </Box>
          </GridItem>

          {/* Right products section */}
          <GridItem>
            {/* Category navigation */}
            <Flex 
              gap={{ base: 2, md: 4 }} 
              mb={{ base: 4, md: 6 }} 
              flexWrap="wrap" 
              align="center"
              justify={{ base: "center", md: "flex-start" }}
            >
              {categories.map((category, index) => (
                <Button
                  key={index}
                  variant={index === 0 ? "solid" : "ghost"}
                  colorScheme={index === 0 ? "orange" : "gray"}
                  size={{ base: "xs", md: "sm" }}
                  borderRadius="full"
                  fontSize={{ base: "2xs", md: "xs" }}
                  px={{ base: 2, md: 3 }}
                >
                  {category.name}
                </Button>
              ))}
              <Button
                ml={{ base: 0, md: "auto" }}
                colorScheme="blue"
                variant="ghost"
                size={{ base: "xs", md: "sm" }}
                fontSize={{ base: "2xs", md: "xs" }}
                px={{ base: 2, md: 3 }}
                mt={{ base: 2, md: 0 }}
              >
                <Text display={{ base: "none", sm: "inline" }}>Browse All Product</Text>
                <Text display={{ base: "inline", sm: "none" }}>Browse All</Text>
                <FaArrowRight />
              </Button>
            </Flex>

            {/* Products grid */}
            <SimpleGrid 
              columns={{ base: 1, sm: 2, md: 3, lg: 4 }} 
              gap={{ base: 3, md: 4 }}
            >
              {currentProducts.length > 0 ? (
                currentProducts.map((product) => (
                  <ProductCard
                    key={product.id}
                    {...product}
                    onAddToCart={handleAddToCart}
                    onAddToWishlist={handleAddToWishlist}
                    onQuickView={handleQuickView}
                  />
                ))
              ) : (
                <Text 
                  color="gray.500" 
                  gridColumn="span 4" 
                  textAlign="center" 
                  py={8}
                  fontSize={{ base: "sm", md: "md" }}
                >
                  No products found in this category
                </Text>
              )}
            </SimpleGrid>
          </GridItem>
        </Grid>
      </Box>
    </section>
  );
};

export default FeatureSection;