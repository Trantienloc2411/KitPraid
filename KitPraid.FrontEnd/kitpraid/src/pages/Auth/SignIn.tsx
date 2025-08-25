import React, { useState } from 'react';
import {
  Box,
  Button,
  Input,
  VStack,
  Text,
  Heading,
  Container,
  HStack,
} from '@chakra-ui/react';
import { FaEye, FaEyeSlash, FaEnvelope, FaLock, FaGoogle, FaFacebook } from 'react-icons/fa';
import { Link as RouterLink } from 'react-router-dom';

import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import { toaster } from '../../components/ui/toaster';

const SignIn = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<{ email?: string; password?: string }>({});

  const validateForm = () => {
    const newErrors: { email?: string; password?: string } = {};

    if (!email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      newErrors.email = 'Email is invalid';
    }

    if (!password) {
      newErrors.password = 'Password is required';
    } else if (password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setIsLoading(true);
    
    try {
      // TODO: Implement actual sign in logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      
      toaster.success({
        title: 'Success',
        description: 'Signed in successfully!',
        duration: 3000,
        closable: true,
      });
      
      // TODO: Redirect to dashboard or home page
    } catch (error) {
      toaster.error({
        title: 'Error',
        description: 'Failed to sign in. Please try again.',
        duration: 3000,
        closable: true,
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleGoogleSignIn = () => {
    // TODO: Implement Google OAuth
    toaster.info({
      title: 'Coming Soon',
      description: 'Google sign in will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  const handleFacebookSignIn = () => {
    // TODO: Implement Facebook OAuth
    toaster.info({
      title: 'Coming Soon',
      description: 'Facebook sign in will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  return (
    <Container maxW="100%" py={20}>
      <BreadcrumbComponent
        items={[
          { label: 'Authentication', href: '/auth' },
          { label: 'Sign In', isCurrentPage: true }
        ]}
      />
      
      <Box
        bg="white"
        p={8}
        borderRadius="lg"
        boxShadow="xl"
        border="1px"
        borderColor="gray.200"
      >
        <VStack gap={6} align="stretch">
          <Box textAlign="center">
            <Heading size="lg" color="gray.800" mb={2}>
              Welcome Back
            </Heading>
            <Text color="gray.600">
              Sign in to your KitPraid account
            </Text>
          </Box>

          <form onSubmit={handleSubmit}>
            <VStack gap={4}>
              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  Email
                </Text>
                <Box position="relative">
                  <Box
                    position="absolute"
                    left={3}
                    top="50%"
                    transform="translateY(-50%)"
                    zIndex={1}
                  >
                    <FaEnvelope color="gray.400" />
                  </Box>
                  <Input
                    type="email"
                    placeholder="Enter your email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    size="lg"
                    pl={10}
                  />
                </Box>
                {errors.email && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.email}
                  </Text>
                )}
              </Box>

              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  Password
                </Text>
                <Box position="relative">
                  <Box
                    position="absolute"
                    left={3}
                    top="50%"
                    transform="translateY(-50%)"
                    zIndex={1}
                  >
                    <FaLock color="gray.400" />
                  </Box>
                  <Input
                    type={showPassword ? 'text' : 'password'}
                    placeholder="Enter your password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    size="lg"
                    pl={10}
                    pr={12}
                  />
                  <Box
                    position="absolute"
                    right={3}
                    top="50%"
                    transform="translateY(-50%)"
                    zIndex={1}
                    cursor="pointer"
                    onClick={() => setShowPassword(!showPassword)}
                  >
                    {showPassword ? <FaEyeSlash color="gray.400" /> : <FaEye color="gray.400" />}
                  </Box>
                </Box>
                {errors.password && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.password}
                  </Text>
                )}
              </Box>

              <Button
                type="submit"
                colorScheme="blue"
                size="lg"
                width="full"
                loading={isLoading}
                loadingText="Signing In..."
              >
                Sign In
              </Button>
            </VStack>
          </form>

          <HStack justify="space-between" fontSize="sm">
            <RouterLink to="/auth/login/forgot-password">
              <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer">
                Forgot Password?
              </Text>
            </RouterLink>
            <Text color="gray.600">
              Don't have an account?{' '}
              <RouterLink to="/auth/signup">
                <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer" display="inline">
                  Sign Up
                </Text>
              </RouterLink>
            </Text>
          </HStack>

          <Box borderTop="1px" borderColor="gray.200" pt={6} />

          <VStack gap={3}>
            <Text fontSize="sm" color="gray.600">
              Or continue with
            </Text>
            
            <HStack gap={4} width="full">
              <Button
                variant="outline"
                size="lg"
                width="full"
                onClick={handleGoogleSignIn}
              >
                <FaGoogle style={{ marginRight: '8px' }} />
                Google
              </Button>
              <Button
                variant="outline"
                size="lg"
                width="full"
                onClick={handleFacebookSignIn}
              >
                <FaFacebook style={{ marginRight: '8px' }} />
                Facebook
              </Button>
            </HStack>
          </VStack>
        </VStack>
      </Box>
    </Container>
  );
};

export default SignIn;
