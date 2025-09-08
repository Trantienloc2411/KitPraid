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
import { FaEye, FaEyeSlash, FaLock, FaCheckCircle } from 'react-icons/fa';
import BreadcrumbComponent from '../../components/ui/Breadcrumb';
import './AuthTabs.css';
import { Link as RouterLink } from 'react-router-dom';

const ResetPassword = () => {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [formData, setFormData] = useState({
    password: '',
    confirmPassword: '',
  });
  const [isLoading, setIsLoading] = useState(false);
  const [isSuccess, setIsSuccess] = useState(false);
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

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleInputChange = (field: string, value: string) => {
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
      // TODO: Implement actual password reset logic
      await new Promise(resolve => setTimeout(resolve, 1000)); // Simulate API call
      setIsSuccess(true);
    } catch (error) {
      // Handle error
    } finally {
      setIsLoading(false);
    }
  };

  if (isSuccess) {
    return (
      <Container maxW="100%" py={20} className="auth-tabs-container">
        <BreadcrumbComponent
          items={[
            { label: 'Authentication', href: '/auth' },
            { label: 'Login', href: '/auth/signin' },
            { label: 'Reset Password', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card" textAlign="center">
          <VStack gap={6}>
            <Box>
              <FaCheckCircle size={64} color="green" />
              <Heading size="lg" color="gray.800" mb={4} mt={4}>
                Password Reset Successfully!
              </Heading>
              <Text color="gray.600">
                Your password has been updated. You can now sign in with your new password.
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
            { label: 'Login', href: '/auth/signin' },
            { label: 'Reset Password', isCurrentPage: true }
          ]}
        />
        
        <Box maxW="500px" mx="auto" bg="white" p={8} borderRadius="2xl" boxShadow="2xl" border="1px" borderColor="gray.200" position="relative" overflow="hidden" className="auth-card">
        <VStack gap={6} align="stretch">
          <Box textAlign="center">
            <Heading size="lg" color="gray.800" mb={2}>
              Reset Your Password
            </Heading>
            <Text color="gray.600">
              Enter your new password below
            </Text>
          </Box>

          <form onSubmit={handleSubmit}>
            <VStack gap={4}>
              <Box>
                <Text as="label" display="block" mb={2} fontWeight="medium" color="gray.700">
                  New Password
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
                    placeholder="Enter new password"
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
                  Confirm New Password
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
                    placeholder="Confirm new password"
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

              <Button
                type="submit"
                colorScheme="blue"
                size="lg"
                width="full"
                loading={isLoading}
                loadingText="Resetting Password..."
              >
                Reset Password
              </Button>
            </VStack>
          </form>

          <HStack justify="center" fontSize="sm">
            <Text color="gray.600">
              Remember your password?{' '}
              <RouterLink to="/auth/signin">
                <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer" display="inline">
                  Sign In
                </Text>
              </RouterLink>
            </Text>
          </HStack>
        </VStack>
      </Box>
    </Container>
  );
};

export default ResetPassword;
