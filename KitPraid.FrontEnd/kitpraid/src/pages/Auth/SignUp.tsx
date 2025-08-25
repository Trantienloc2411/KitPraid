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
import { FaEye, FaEyeSlash, FaEnvelope, FaLock, FaUser, FaGoogle, FaFacebook } from 'react-icons/fa';
import { Link as RouterLink } from 'react-router-dom';

import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import { toaster } from '../../components/ui/toaster';

const SignUp = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
    acceptTerms: false,
  });
  const [isLoading, setIsLoading] = useState(false);
  const [errors, setErrors] = useState<{ [key: string]: string }>({});

  const getPasswordStrength = (password: string) => {
    let score = 0;
    if (password.length >= 8) score++;
    if (/[a-z]/.test(password)) score++;
    if (/[A-Z]/.test(password)) score++;
    if (/[0-9]/.test(password)) score++;
    if (/[^A-Za-z0-9]/.test(password)) score++;
    
    if (score <= 2) return { score, color: 'red', text: 'Weak' };
    if (score <= 3) return { score, color: 'orange', text: 'Fair' };
    if (score <= 4) return { score, color: 'yellow', text: 'Good' };
    return { score, color: 'green', text: 'Strong' };
  };

  const passwordStrength = getPasswordStrength(formData.password);

  const validateForm = () => {
    const newErrors: { [key: string]: string } = {};

    if (!formData.firstName.trim()) {
      newErrors.firstName = 'First name is required';
    }

    if (!formData.lastName.trim()) {
      newErrors.lastName = 'Last name is required';
    }

    if (!formData.email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'Email is invalid';
    }

    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 8) {
      newErrors.password = 'Password must be at least 8 characters';
    }

    if (!formData.confirmPassword) {
      newErrors.confirmPassword = 'Please confirm your password';
    } else if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    if (!formData.acceptTerms) {
      newErrors.acceptTerms = 'You must accept the terms and conditions';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleInputChange = (field: string, value: string | boolean) => {
    setFormData(prev => ({ ...prev, [field]: value }));
    
    // Clear error when user starts typing
    if (errors[field]) {
      setErrors(prev => ({ ...prev, [field]: '' }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setIsLoading(true);
    
    try {
      // TODO: Implement actual sign up logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      
      toaster.success({
        title: 'Success',
        description: 'Account created successfully! Please check your email for verification.',
        duration: 5000,
        closable: true,
      });
      
      // TODO: Redirect to email verification page
    } catch (error) {
      toaster.error({
        title: 'Error',
        description: 'Failed to create account. Please try again.',
        duration: 3000,
        closable: true,
      });
    } finally {
      setIsLoading(false);
    }
  };

  const handleGoogleSignUp = () => {
    // TODO: Implement Google OAuth
    toaster.info({
      title: 'Coming Soon',
      description: 'Google sign up will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  const handleFacebookSignUp = () => {
    // TODO: Implement Facebook OAuth
    toaster.info({
      title: 'Coming Soon',
      description: 'Facebook sign up will be available soon!',
      duration: 3000,
      closable: true,
    });
  };

  return (
    <Container maxW="md" py={10}>
      <BreadcrumbComponent
        items={[
          { label: 'Authentication', href: '/auth' },
          { label: 'Sign Up', isCurrentPage: true }
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
              Create Account
            </Heading>
            <Text color="gray.600">
              Join KitPraid and start shopping today
            </Text>
          </Box>

          <form onSubmit={handleSubmit}>
            <VStack gap={4}>
              <HStack gap={4} width="full">
                <Box flex={1}>
                  <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                    First Name
                  </Text>
                  <Box position="relative">
                    <Box
                      position="absolute"
                      left={3}
                      top="50%"
                      transform="translateY(-50%)"
                      zIndex={1}
                    >
                      <FaUser color="gray.400" />
                    </Box>
                    <Input
                      type="text"
                      placeholder="First name"
                      value={formData.firstName}
                      onChange={(e) => handleInputChange('firstName', e.target.value)}
                      size="lg"
                      pl={10}
                    />
                  </Box>
                  {errors.firstName && (
                    <Text color="red.500" fontSize="sm" mt={1}>
                      {errors.firstName}
                    </Text>
                  )}
                </Box>

                <Box flex={1}>
                  <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                    Last Name
                  </Text>
                  <Input
                    type="text"
                    placeholder="Last name"
                    value={formData.lastName}
                    onChange={(e) => handleInputChange('lastName', e.target.value)}
                    size="lg"
                  />
                  {errors.lastName && (
                    <Text color="red.500" fontSize="sm" mt={1}>
                      {errors.lastName}
                    </Text>
                  )}
                </Box>
              </HStack>

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
                    value={formData.email}
                    onChange={(e) => handleInputChange('email', e.target.value)}
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
                    placeholder="Create a password"
                    value={formData.password}
                    onChange={(e) => handleInputChange('password', e.target.value)}
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
                
                {formData.password && (
                  <Box mt={2}>
                    <HStack justify="space-between" mb={1}>
                      <Text fontSize="sm" color="gray.600">Password strength:</Text>
                      <Text fontSize="sm" color={`${passwordStrength.color}.500`} fontWeight="bold">
                        {passwordStrength.text}
                      </Text>
                    </HStack>
                    <Box
                      bg="gray.200"
                      h={2}
                      borderRadius="full"
                      overflow="hidden"
                    >
                      <Box
                        bg={`${passwordStrength.color}.500`}
                        h="full"
                        w={`${passwordStrength.score * 20}%`}
                        transition="width 0.3s ease"
                      />
                    </Box>
                  </Box>
                )}
                
                {errors.password && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.password}
                  </Text>
                )}
              </Box>

              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  Confirm Password
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
                    type={showConfirmPassword ? 'text' : 'password'}
                    placeholder="Confirm your password"
                    value={formData.confirmPassword}
                    onChange={(e) => handleInputChange('confirmPassword', e.target.value)}
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
                    onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                  >
                    {showConfirmPassword ? <FaEyeSlash color="gray.400" /> : <FaEye color="gray.400" />}
                  </Box>
                </Box>
                {errors.confirmPassword && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.confirmPassword}
                  </Text>
                )}
              </Box>

              <Box>
                <HStack gap={3}>
                  <input
                    type="checkbox"
                    checked={formData.acceptTerms}
                    onChange={(e) => handleInputChange('acceptTerms', e.target.checked)}
                    style={{ width: '16px', height: '16px' }}
                  />
                  <Text fontSize="sm" color="gray.600">
                    I agree to the{' '}
                    <Text as="span" color="blue.500" cursor="pointer" _hover={{ textDecoration: 'underline' }}>
                      Terms of Service
                    </Text>{' '}
                    and{' '}
                    <Text as="span" color="blue.500" cursor="pointer" _hover={{ textDecoration: 'underline' }}>
                      Privacy Policy
                    </Text>
                  </Text>
                </HStack>
                {errors.acceptTerms && (
                  <Text color="red.500" fontSize="sm" mt={1}>
                    {errors.acceptTerms}
                  </Text>
                )}
              </Box>

              <Button
                type="submit"
                colorScheme="blue"
                size="lg"
                width="full"
                loading={isLoading}
                loadingText="Creating Account..."
              >
                Create Account
              </Button>
            </VStack>
          </form>

          <HStack justify="space-between" fontSize="sm">
            <Text color="gray.600">
              Already have an account?{' '}
              <RouterLink to="/auth/signin">
                <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer" display="inline">
                  Sign In
                </Text>
              </RouterLink>
            </Text>
            <RouterLink to="/auth/signup/email-verification">
              <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer" display="inline">
                Verify Email
              </Text>
            </RouterLink>
          </HStack>

          <Box borderTop="1px" borderColor="gray.200" pt={6} />

          <VStack gap={3}>
            <Text fontSize="sm" color="gray.600">
              Or sign up with
            </Text>
            
            <HStack gap={4} width="full">
              <Button
                variant="outline"
                size="lg"
                width="full"
                onClick={handleGoogleSignUp}
              >
                <FaGoogle style={{ marginRight: '8px' }} />
                Google
              </Button>
              <Button
                variant="outline"
                size="lg"
                width="full"
                onClick={handleFacebookSignUp}
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

export default SignUp;
