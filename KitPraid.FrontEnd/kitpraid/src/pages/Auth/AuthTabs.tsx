import {
  Box,
  Button,
  VStack,
  Text,
  Heading,
  Container,
  HStack,
} from '@chakra-ui/react';
import './AuthTabs.css';
import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import SignIn from './SignIn';
import SignUp from './SignUp';
import SocialAuth from './SocialAuth';
import { useState } from 'react';

const AuthTabs = () => {
  const [activeTab, setActiveTab] = useState(0);

  // Color mode values
  const bgColor = 'white';
  const borderColor = 'gray.200';
  const textColor = 'gray.800';
  const subtextColor = 'gray.600';

  return (
    <Container maxW="100%" py={20} className="auth-tabs-container">
      <BreadcrumbComponent
        items={[
          { label: 'Authentication', href: '/auth' },
          { label: activeTab === 0 ? 'Sign In' : 'Sign Up', isCurrentPage: true }
        ]}
      />
      
      <Box
        maxW="500px"
        mx="auto"
        bg={bgColor}
        p={8}
        borderRadius="2xl"
        boxShadow="2xl"
        border="1px"
        borderColor={borderColor}
        position="relative"
        overflow="hidden"
        className="auth-card"
      >
        {/* Background decoration */}
        <Box
          position="absolute"
          top="-50px"
          right="-50px"
          w="200px"
          h="200px"
          bg="blue.100"
          borderRadius="full"
          opacity="0.1"
          className="bg-decoration"
        />
        <Box
          position="absolute"
          bottom="-50px"
          left="-50px"
          w="150px"
          h="150px"
          bg="purple.100"
          borderRadius="full"
          opacity="0.1"
          className="bg-decoration"
        />

        <VStack gap={8} align="stretch">
          {/* Header */}
          <Box textAlign="center">
            <Heading size="xl" color={textColor} mb={3} fontWeight="bold">
              Welcome to KitPraid
            </Heading>
            <Text color={subtextColor} fontSize="lg">
              Your ultimate shopping destination
            </Text>
          </Box>

          {/* Tabs */}
          <Box>
            {/* Tab Headers */}
            <HStack 
              bg="gray.50" 
              p={1} 
              borderRadius="xl"
              border="1px"
              borderColor="gray.200"
              className="tab-list"
              mb={6}
            >
              <Button
                flex={1}
                variant="ghost"
                size="lg"
                borderRadius="lg"
                className="tab-item"
                bg={activeTab === 0 ? "white" : "transparent"}
                color={activeTab === 0 ? "blue.600" : "gray.600"}
                boxShadow={activeTab === 0 ? "md" : "none"}
                onClick={() => setActiveTab(0)}
                _hover={{
                  bg: activeTab === 0 ? "white" : "gray.100"
                }}
              >
                Sign In
              </Button>
              <Button
                flex={1}
                variant="ghost"
                size="lg"
                borderRadius="lg"
                className="tab-item"
                bg={activeTab === 1 ? "white" : "transparent"}
                color={activeTab === 1 ? "blue.600" : "gray.600"}
                boxShadow={activeTab === 1 ? "md" : "none"}
                onClick={() => setActiveTab(1)}
                _hover={{
                  bg: activeTab === 1 ? "white" : "gray.100"
                }}
              >
                Sign Up
              </Button>
            </HStack>

            {/* Tab Content */}
            {activeTab === 0 && (
              <SignIn textColor={textColor} subtextColor={subtextColor} />
            )}

            {/* Sign Up Panel */}
            {activeTab === 1 && (
              <SignUp textColor={textColor} subtextColor={subtextColor} />
            )}
          </Box>

          {/* Social Authentication */}
          <SocialAuth subtextColor={subtextColor} />
        </VStack>
      </Box>
    </Container>
  );
};

export default AuthTabs;
