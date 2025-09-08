import React from 'react';
import {
  Box,
  Button,
  VStack,
  Text,
  HStack,
} from '@chakra-ui/react';
import { FaGoogle, FaFacebook, FaApple } from 'react-icons/fa';
import { toaster } from '../../components/ui/toaster';

interface SocialAuthProps {
  subtextColor: string;
}

const SocialAuth: React.FC<SocialAuthProps> = ({ subtextColor }) => {
  // Social auth handlers
  const handleGoogleAuth = () => {
    toaster.info({
      title: 'Coming Soon',
      description: 'Google authentication will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  const handleFacebookAuth = () => {
    toaster.info({
      title: 'Coming Soon',
      description: 'Facebook authentication will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  const handleAppleAuth = () => {
    toaster.info({
      title: 'Coming Soon',
      description: 'Apple authentication will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  return (
    <Box borderTop="1px" borderColor="gray.200" pt={6}>
      <VStack gap={4}>
        <Text fontSize="sm" color={subtextColor} textAlign="center">
          Or continue with
        </Text>
        
        <HStack gap={3} w="full">
          <Button
            variant="outline"
            size="lg"
            flex={1}
            h="48px"
            borderRadius="xl"
            border="2px"
            borderColor="gray.200"
            onClick={handleGoogleAuth}
            _hover={{ 
              borderColor: "red.400", 
              color: "red.500",
              transform: "translateY(-1px)",
              boxShadow: "md"
            }}
            transition="all 0.2s"
            className="social-button"
          >
            <FaGoogle style={{ marginRight: '8px', color: '#DB4437' }} />
            Google
          </Button>
          <Button
            variant="outline"
            size="lg"
            flex={1}
            h="48px"
            borderRadius="xl"
            border="2px"
            borderColor="gray.200"
            onClick={handleFacebookAuth}
            _hover={{ 
              borderColor: "blue.400", 
              color: "blue.500",
              transform: "translateY(-1px)",
              boxShadow: "md"
            }}
            transition="all 0.2s"
            className="social-button"
          >
            <FaFacebook style={{ marginRight: '8px', color: '#4267B2' }} />
            Facebook
          </Button>
          <Button
            variant="outline"
            size="lg"
            flex={1}
            h="48px"
            borderRadius="xl"
            border="2px"
            borderColor="gray.200"
            onClick={handleAppleAuth}
            _hover={{ 
              borderColor: "gray.600", 
              color: "gray.700",
              transform: "translateY(-1px)",
              boxShadow: "md"
            }}
            transition="all 0.2s"
            className="social-button"
          >
            <FaApple style={{ marginRight: '8px' }} />
            Apple
          </Button>
        </HStack>
      </VStack>
    </Box>
  );
};

export default SocialAuth;
