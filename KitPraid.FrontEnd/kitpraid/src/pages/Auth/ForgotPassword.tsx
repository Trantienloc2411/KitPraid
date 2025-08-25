import React, { useState } from 'react';
import {
  Box,
  Button,
  Input,
  VStack,
  Text,
  Link,
  Heading,
  Container,
  HStack,
} from '@chakra-ui/react';
import { FaEnvelope, FaArrowLeft } from 'react-icons/fa';
import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import './AuthTabs.css';
import { Link as RouterLink } from 'react-router-dom';

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [errors, setErrors] = useState<{ email?: string }>({});

  const validateForm = () => {
    const newErrors: { email?: string } = {};

    if (!email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(email)) {
      newErrors.email = 'Email is invalid';
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
      // TODO: Implement actual forgot password logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      setIsSubmitted(true);
    } catch (error) {
      // Handle error
    } finally {
      setIsLoading(false);
    }
  };

  if (isSubmitted) {
    return (
      <Container maxW="100%" py={20} className="auth-tabs-container">
        <BreadcrumbComponent
          items={[
            { label: 'Authentication', href: '/auth' },
            { label: 'Login', href: '/auth/signin' },
            { label: 'Forgot Password', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card" textAlign="center">
          <VStack gap={6}>
            <Box>
              <Heading size="lg" color="gray.800" mb={4}>
                Check Your Email
              </Heading>
              <Text color="gray.600" fontSize="lg">
                We've sent a password reset link to:
              </Text>
              <Text color="blue.600" fontWeight="bold" fontSize="lg" mt={2}>
                {email}
              </Text>
            </Box>
            
            <Text color="gray.600">
              Click the link in the email to reset your password. The link will expire in 1 hour.
            </Text>
            
            <VStack gap={3} width="full">
              <Button
                colorScheme="blue"
                size="lg"
                width="full"
                onClick={() => setIsSubmitted(false)}
              >
                Resend Email
              </Button>
              
              <RouterLink to="/auth/signin">
                <Link color="blue.500" _hover={{ textDecoration: 'underline' }}>
                  <HStack gap={2}>
                    <FaArrowLeft />
                    <Text>Back to Sign In</Text>
                  </HStack>
                </Link>
              </RouterLink>
            </VStack>
          </VStack>
        </Box>
      </Container>
    );
  }

  return (
          <Container maxW="100%" py={20} className="auth-tabs-container">
        <BreadcrumbComponent
          items={[
            { label: 'Authentication', href: '/auth' },
            { label: 'Login', href: '/auth/signin' },
            { label: 'Forgot Password', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card">
        <VStack gap={6} align="stretch">
          <Box textAlign="center">
            <Heading size="lg" color="gray.800" mb={2}>
              Forgot Password?
            </Heading>
            <Text color="gray.600">
              Enter your email address and we'll send you a link to reset your password.
            </Text>
          </Box>

          <form onSubmit={handleSubmit}>
            <VStack gap={4}>
              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  Email Address
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

              <Button
                type="submit"
                colorScheme="blue"
                size="lg"
                width="full"
                loading={isLoading}
                loadingText="Sending Reset Link..."
              >
                Send Reset Link
              </Button>
            </VStack>
          </form>

          <HStack justify="center" fontSize="sm">
            <Text color="gray.600">
              Remember your password?{' '}
              <RouterLink to="/auth/signin">
                <Link color="blue.500" _hover={{ textDecoration: 'underline' }}>
                  Sign In
                </Link>
              </RouterLink>
            </Text>
          </HStack>
        </VStack>
      </Box>
    </Container>
  );
};

export default ForgotPassword;
