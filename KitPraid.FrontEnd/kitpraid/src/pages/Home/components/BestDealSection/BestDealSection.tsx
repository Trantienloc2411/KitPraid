import { Box, Grid, GridItem, Heading, HStack, Button, SimpleGrid, Text, VStack } from "@chakra-ui/react";
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
      <Box maxW="1200px" mx="auto" alignContent={"center"} px={{ base: 4, md: 0 }}>
        <VStack gap={{ base: 6, md: 8 }} mb={{ base: 4, md: 2 }} align="flex-start">
          <HStack 
            justify={{ base: "center", md: "flex-start" }} 
            w="100%" 
            align="center"
            flexWrap={{ base: "wrap", md: "nowrap" }}
            gap={{ base: 2, md: 4 }}
          >
            <Heading 
              size={{ base: "lg", md: "xl" }} 
              pr={{ base: 0, md: "0.4rem" }} 
              fontWeight="bold" 
              color="gray.800"
              textAlign={{ base: "center", md: "left" }}
              w={{ base: "100%", md: "auto" }}
            >
              Best Deals
            </Heading>
            <CountdownTimer className="countdown-timer" targetDate={targetDate} />
            <Button
                ml={{ base: 0, md: "auto" }}
                colorScheme="blue"
                variant="ghost"
                size={{ base: "xs", md: "sm" }}
                fontSize={{ base: "xs", md: "sm" }}
                mt={{ base: 2, md: 0 }}
                w={{ base: "100%", md: "auto" }}
            >
              <Text display={{ base: "none", sm: "inline" }}>Browse All Deals</Text>
              <Text display={{ base: "inline", sm: "none" }}>Browse All</Text>
              <FaArrowRight />
            </Button>
          </HStack>
          <Text 
            fontSize={{ base: "sm", md: "md" }} 
            color="gray.600" 
            textAlign={{ base: "center", md: "left" }}
            w={{ base: "100%", md: "auto" }}
            px={{ base: 4, md: 0 }}
          >
            Discover our best deals on premium products that combine quality, style, and innovation.
          </Text>
        </VStack>

        {/* Layout: 1 big card (left) + 8 small cards (right) */}
        <Grid
          templateColumns={{ base: '1fr', md: 'repeat(5, 1fr)' }}
          gap={{ base: 4, md: 2 }}
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
            <SimpleGrid columns={{ base: 1, sm: 2, md: 4 }} gap={{ base: 4, md: 6 }}>
              {products.slice(0, 5).map(p => (
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
            <SimpleGrid columns={{ base: 1, sm: 2, md: 4 }} gap={{ base: 4, md: 6 }}>
              {products.slice(0, 5).map(p => (
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