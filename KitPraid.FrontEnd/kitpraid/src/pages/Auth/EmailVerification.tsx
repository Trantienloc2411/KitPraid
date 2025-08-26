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
import { FaEnvelope, FaCheckCircle, FaArrowLeft, FaRedo } from 'react-icons/fa';
import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import './AuthTabs.css';
import { Link as RouterLink } from 'react-router-dom';

const EmailVerification = () => {
  const [verificationCode, setVerificationCode] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [isVerified, setIsVerified] = useState(false);
  const [isResent, setIsResent] = useState(false);
  const [errors, setErrors] = useState<{ verificationCode?: string }>({});

  const validateForm = () => {
    const newErrors: { verificationCode?: string } = {};

    if (!verificationCode) {
      newErrors.verificationCode = 'Verification code is required';
    } else if (verificationCode.length !== 6) {
      newErrors.verificationCode = 'Verification code must be 6 digits';
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
      // TODO: Implement actual verification logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      setIsVerified(true);
    } catch (error) {
      // Handle error
    } finally {
      setIsLoading(false);
    }
  };

  const handleResendCode = async () => {
    setIsLoading(true);
    
    try {
      // TODO: Implement actual resend logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      setIsResent(true);
      setTimeout(() => setIsResent(false), 5000); // Hide message after 5 seconds
    } catch (error) {
      // Handle error
    } finally {
      setIsLoading(false);
    }
  };

  if (isVerified) {
    return (
      <Container maxW="100%" py={20} className="auth-tabs-container">
        <BreadcrumbComponent
          items={[
            { label: 'Authentication', href: '/auth' },
            { label: 'Sign Up', href: '/auth/signup' },
            { label: 'Email Verification', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card" textAlign="center">
          <VStack gap={6}>
            <Box>
              <FaCheckCircle size={64} color="green" />
              <Heading size="lg" color="gray.800" mb={4} mt={4}>
                Email Verified Successfully!
              </Heading>
              <Text color="gray.600">
                Your email has been verified. You can now sign in to your account.
              </Text>
            </Box>

            <VStack gap={3} width="full">
              <RouterLink to="/auth/signin">
                <Button
                  colorScheme="blue"
                  size="lg"
                  width="full"
                >
                  Sign In
                </Button>
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
            { label: 'Sign Up', href: '/auth/signup' },
            { label: 'Email Verification', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card">
        <VStack gap={6} align="stretch">
          <Box textAlign="center">
            <Heading size="lg" color="gray.800" mb={2}>
              Verify Your Email
            </Heading>
            <Text color="gray.600">
              We've sent a verification code to your email address. Please enter it below.
            </Text>
          </Box>

          {isResent && (
            <Box
              bg="green.100"
              border="1px"
              borderColor="green.300"
              borderRadius="md"
              p={3}
              color="green.800"
            >
              Verification code has been resent to your email.
            </Box>
          )}

          <form onSubmit={handleSubmit}>
            <VStack gap={4}>
              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  Verification Code
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
                    type="text"
                    placeholder="Enter 6-digit code"
                    value={verificationCode}
                    onChange={(e) => {
                      setVerificationCode(e.target.value.replace(/\D/g, '').slice(0, 6));
                      if (errors.verificationCode) {
                        setErrors(prev => ({ ...prev, verificationCode: '' }));
                      }
                    }}
                    size="lg"
                    pl={10}
                    maxLength={6}
                  />
                </Box>
                {errors.verificationCode && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.verificationCode}
                  </Text>
                )}
              </Box>

              <Button
                type="submit"
                colorScheme="blue"
                size="lg"
                width="full"
                loading={isLoading}
                loadingText="Verifying..."
              >
                Verify Email
              </Button>
            </VStack>
          </form>

          <VStack gap={3}>
            <Text fontSize="sm" color="gray.600" textAlign="center">
              Didn't receive the code? Check your spam folder or
            </Text>

            <Button
              variant="outline"
              size="md"
              onClick={handleResendCode}
              loading={isLoading}
              loadingText="Sending..."
            >
              <FaRedo style={{ marginRight: '8px' }} />
              Resend Code
            </Button>
          </VStack>

          <HStack justify="center" fontSize="sm">
            <Text color="gray.600">
              Already verified?{' '}
              <RouterLink to="/auth/signin">
                <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer" display="inline">
                  Sign In
                </Text>
              </RouterLink>
            </Text>
          </HStack>

          <HStack justify="center">
            <RouterLink to="/auth/signin">
              <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer">
                <HStack gap={2}>
                  <FaArrowLeft />
                  <Text>Back to Sign In</Text>
                </HStack>
              </Text>
            </RouterLink>
          </HStack>
        </VStack>
      </Box>
    </Container>
  );
};

export default EmailVerification;
