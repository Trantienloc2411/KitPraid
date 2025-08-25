import { Box, HStack, Text, VStack } from "@chakra-ui/react";
import { FaArrowRight, FaMinus } from "react-icons/fa";
import Tag from "../Tag";
export const QuickLink = () => {

    return (
        <Box px={64} py={10} bg="gray.900" mx="auto" alignContent={"flex-start"} maxW="100%" overflowX="clip">
            <HStack  alignItems={"flex-start"} color="white" className="quick-link-container">
                <VStack w="30%" gap={2} className="quick-link-information" alignItems={"flex-start"} >
                    <Box alignSelf={"flex-start"} className="logo-container">
                        <a href="/" className="logo">
                            KitPraid
                        </a>
                    </Box>
                    <Box className="phone-support" alignSelf={"flex-start"}>
                        <Text fontSize="sm" alignSelf={"flex-start"} fontWeight="bold">
                            Customer Support:
                        </Text>
                        <Text textStyle="md" alignSelf={"flex-start"} color="white" fontWeight="bold">
                            +84 906 888 888
                        </Text>
                    </Box>
                    <Box className="quick-link-address"  >
                        <Text fontSize="sm" fontWeight="bold">
                            12 Hoa Binh, Hanoi, Vietnam
                        </Text>
                    </Box>
                    <Box className="quick-link-email" alignSelf={"flex-start"}>
                        <Text textStyle="sm" alignSelf={"flex-start"} color="white" fontWeight="bold">
                            info@kitpraid.com
                        </Text>
                    </Box>

                </VStack>
                <VStack w="30%" gap={2} alignItems={"flex-start"} className="quick-link-top-category" >
                    <Text py={4} textStyle="md" alignSelf={"flex-start"} color="white" fontWeight="bold">
                        Top Category
                    </Text>
                    <VStack gap={2} className="quick-link-top-category-list" >
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Computer & Laptop</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Smartphone</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Tablet</a>
                        </Text>
                        <Box className="quick-link-top-category-accessories" alignSelf={"flex-start"}>
                            <a href="/" className="quick-link-top-category-accessories">
                                <HStack alignItems={"center"} gap={2}>
                                    <FaMinus color="EBC80C" />
                                    <Text textStyle="sm" alignSelf={"flex-start"} color="white" fontWeight="bold">
                                        Accessories
                                    </Text>
                                </HStack>
                            </a>
                        </Box>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Camera & Photo</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">TV & Homes</a>
                        </Text>
                    </VStack>
                    <HStack alignItems={"center"} gap={2}>
                        <Text fontSize="sm" alignSelf={"flex-start"} color="#EBC80C">Browse All Products</Text>
                        <FaArrowRight color="EBC80C" />
                    </HStack>
                </VStack>
                <VStack w="20%" px={8} gap={2} alignItems={"flex-start"} className="quick-link" >
                    <Text py={4} textStyle="md" alignSelf={"flex-start"} color="white" fontWeight="bold">
                        Quick Links
                    </Text>
                    <VStack gap={2} className="quick-link-list" >
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Home</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">About Us</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Contact Us</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Privacy Policy</a>
                        </Text>
                        <Text textStyle="sm" alignSelf={"flex-start"} fontWeight="bold">
                            <a href="/">Terms of Service</a>
                        </Text>
                    </VStack>
                </VStack>
                <VStack w="20%" px={8} gap={2}  alignItems={"flex-start"} className="quick-link-popular-tag" >
                    <Text py={4} textStyle="md" alignSelf={"flex-start"} color="white" fontWeight="bold">
                        Popular Tags
                    </Text>
                    <Box 
                        className="tag-container"
                        display="flex"
                        flexWrap="wrap"
                        gap={2}
                        maxW="200px"
                        overflow="hidden"
                    >
                        <Tag tag="Game" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="iPhone" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="TV" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Asus Laptops" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Macbook" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="SSD" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Graphics Card" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Power Bank" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Smart TV" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Speaker" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Tablet" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Microwave" color="#343a40" bColor="#495057" tColor="white" />
                        <Tag tag="Samsung" color="#343a40" bColor="#495057" tColor="white" />
                    </Box>
                </VStack>
            </HStack>
        </Box>
    );
}