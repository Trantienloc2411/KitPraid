import { Box, Button, Heading, Input, VStack, Text } from "@chakra-ui/react";
import { useState } from "react";
import { FaArrowRight } from "react-icons/fa";

export const Newsletter = () => {
    const [email, setEmail] = useState("");
    const [error, setError] = useState("");
    const [success, setSuccess] = useState("");

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (email.trim() === "") {
            setError("Please enter your email");
            return;
        }
        // Handle subscription logic here
        setSuccess("Thank you for subscribing!");
        setEmail("");
        setError("");
    }

    return (
        <Box bg="#DE732D;" py={{ base: 8, md: 16 }}>
            <Box maxW="1200px" mx="auto" px={{ base: 4, md: 8 }} alignContent={"center"}>
                <VStack gap={{ base: 6, md: 8 }} mb={{ base: 6, md: 8 }} align="center">
                    <Heading 
                        size={{ base: "lg", md: "xl" }} 
                        fontWeight="bold" 
                        color="white"
                        textAlign="center"
                        px={2}
                    >
                        Subscribe to our newsletter
                    </Heading>
                    <Text 
                        color="white" 
                        fontSize={{ base: "sm", md: "md" }} 
                        textAlign="center" 
                        opacity={0.9}
                        px={4}
                        maxW="600px"
                    >
                        Get the latest news and updates from our store. Stay informed about new products, exclusive deals, and special offers.
                    </Text>
                    
                    {/* Integrated Input + Button */}
                    <form onSubmit={handleSubmit} style={{ width: '100%', maxWidth: '500px' }}>
                        <Box position="relative" px={{ base: 2, md: 0 }}>
                            <Input
                                placeholder="Email address"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                bg="white"
                                color="gray.800"
                                borderRadius="full"
                                border="none"
                                pr={{ base: "120px", md: "140px" }} // Responsive padding for button
                                size="md"
                                fontSize={{ base: "xs", md: "sm" }}
                                _focus={{
                                    boxShadow: "0 0 0 1px white",
                                    outline: "none"
                                }}
                                _placeholder={{
                                    color: "gray.400"
                                }}
                            />
                            <Button
                                type="submit"
                                colorScheme="orange"
                                size={{ base: "sm", md: "md" }}
                                borderRadius="full"
                                px={{ base: 3, md: 4 }}
                                h={{ base: "32px", md: "36px" }}
                                fontWeight="bold"
                                fontSize={{ base: "2xs", md: "xs" }}
                                position="absolute"
                                right={{ base: "2px", md: "2px" }}
                                top={{ base: "2px", md: "2px" }}
                                _hover={{
                                    transform: "translateY(-1px)",
                                    boxShadow: "lg"
                                }}
                                transition="all 0.2s"
                            >
                                <Text display={{ base: "none", sm: "inline" }}>SUBSCRIBE</Text>
                                <Text display={{ base: "inline", sm: "none" }}>SUB</Text>
                                <FaArrowRight />
                            </Button>
                        </Box>
                        
                        {/* Error and Success Messages */}
                        {error && (
                            <Text color="red.200" fontSize="xs" mt={2} textAlign="center">
                                {error}
                            </Text>
                        )}
                        {success && (
                            <Text color="green.200" fontSize="xs" mt={2} textAlign="center">
                                {success}
                            </Text>
                        )}
                    </form>
                </VStack>

                {/* Brand Logos */}
                <Box mt={{ base: 8, md: 12 }}>
                    <VStack gap={{ base: 4, md: 6 }}>
                        <Text 
                            color="white" 
                            fontSize={{ base: "sm", md: "md" }} 
                            fontWeight="semibold" 
                            opacity={0.8}
                            textAlign="center"
                        >
                            Trusted by leading brands
                        </Text>
                        <Box 
                            display="flex" 
                            gap={{ base: 4, md: 8 }} 
                            flexWrap="wrap" 
                            justifyContent="center" 
                            alignItems="center"
                            px={4}
                        >
                            {["Google", "Amazon", "PHILIPS", "TOSHIBA", "SAMSUNG"].map((brand) => (
                                <Text
                                    key={brand}
                                    color="white"
                                    fontSize={{ base: "xs", md: "sm" }}
                                    fontWeight="bold"
                                    opacity={0.7}
                                    _hover={{ opacity: 1 }}
                                    transition="opacity 0.2s"
                                    cursor="pointer"
                                    textAlign="center"
                                    minW="fit-content"
                                >
                                    {brand}
                                </Text>
                            ))}
                        </Box>
                    </VStack>
                </Box>
            </Box>
        </Box>
    )
}