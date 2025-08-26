import React, { useState } from 'react';
import {
  Box,
  Button,
  Input,
  VStack,
  Text,
  HStack,
} from '@chakra-ui/react';
import { FaEye, FaEyeSlash, FaEnvelope, FaLock, FaUser } from 'react-icons/fa';
import { toaster } from '../../components/ui/toaster';

interface SignUpProps {
  textColor: string;
  subtextColor: string;
}

const SignUp: React.FC<SignUpProps> = ({ textColor, subtextColor }) => {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  
  // Sign Up state
  const [signUpData, setSignUpData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
    acceptTerms: false,
  });
  const [signUpErrors, setSignUpErrors] = useState<{ [key: string]: string }>({});
  const [isSignUpLoading, setIsSignUpLoading] = useState(false);

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

  const passwordStrength = getPasswordStrength(signUpData.password);

  // Sign Up validation
  const validateSignUp = () => {
    const newErrors: { [key: string]: string } = {};

    if (!signUpData.firstName.trim()) {
      newErrors.firstName = 'First name is required';
    }

    if (!signUpData.lastName.trim()) {
      newErrors.lastName = 'Last name is required';
    }

    if (!signUpData.email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(signUpData.email)) {
      newErrors.email = 'Email is invalid';
    }

    if (!signUpData.password) {
      newErrors.password = 'Password is required';
    } else if (signUpData.password.length < 8) {
      newErrors.password = 'Password must be at least 8 characters';
    }

    if (!signUpData.confirmPassword) {
      newErrors.confirmPassword = 'Please confirm your password';
    } else if (signUpData.password !== signUpData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    if (!signUpData.acceptTerms) {
      newErrors.acceptTerms = 'You must accept the terms and conditions';
    }

    setSignUpErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  // Sign Up handlers
  const handleSignUpInputChange = (field: string, value: string | boolean) => {
    setSignUpData(prev => ({ ...prev, [field]: value }));
    if (signUpErrors[field]) {
      setSignUpErrors(prev => ({ ...prev, [field]: '' }));
    }
  };

  const handleSignUpSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateSignUp()) {
      return;
    }

    setIsSignUpLoading(true);
    
    try {
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      toaster.success({
        title: 'Success',
        description: 'Account created successfully! Please check your email for verification.',
        duration: 5000,
        closable: true,
      });
      
    } catch (error) {
      toaster.error({
        title: 'Error',
        description: 'Failed to create account. Please try again.',
        duration: 3000,
        closable: true,
      });
    } finally {
      setIsSignUpLoading(false);
    }
  };

  return (
    <Box px={0} pt={6}>
      <form onSubmit={handleSignUpSubmit}>
        <VStack gap={5}>
          <HStack gap={4} w="full">
            <Box flex={1}>
              <Text as="label" display="block" mb={2} fontWeight="semibold" color={textColor}>
                First Name
              </Text>
              <Box position="relative">
                <Box
                  position="absolute"
                  left={4}
                  top="50%"
                  transform="translateY(-50%)"
                  zIndex={1}
                >
                  <FaUser color="gray.400" />
                </Box>
                <Input
                  type="text"
                  placeholder="First name"
                  value={signUpData.firstName}
                  onChange={(e) => handleSignUpInputChange('firstName', e.target.value)}
                  size="lg"
                  pl={12}
                  h="56px"
                  borderRadius="xl"
                  border="2px"
                  borderColor="gray.200"
                  _focus={{
                    borderColor: "blue.500",
                    boxShadow: "0 0 0 1px var(--chakra-colors-blue-500)"
                  }}
                  _hover={{ borderColor: "gray.300" }}
                  className="input-field"
                />
              </Box>
              {signUpErrors.firstName && (
                <Text color="red.500" fontSize="sm" mt={2}>
                  {signUpErrors.firstName}
                </Text>
              )}
            </Box>

            <Box flex={1}>
              <Text as="label" display="block" mb={2} fontWeight="semibold" color={textColor}>
                Last Name
              </Text>
              <Input
                type="text"
                placeholder="Last name"
                value={signUpData.lastName}
                onChange={(e) => handleSignUpInputChange('lastName', e.target.value)}
                size="lg"
                h="56px"
                borderRadius="xl"
                border="2px"
                borderColor="gray.200"
                _focus={{
                  borderColor: "blue.500",
                  boxShadow: "0 0 0 1px var(--chakra-colors-blue-500)"
                }}
                _hover={{ borderColor: "gray.300" }}
                className="input-field"
              />
              {signUpErrors.lastName && (
                <Text color="red.500" fontSize="sm" mt={2}>
                  {signUpErrors.lastName}
                </Text>
              )}
            </Box>
          </HStack>

          <Box w="full">
            <Text as="label" display="block" mb={2} fontWeight="semibold" color={textColor}>
              Email Address
            </Text>
            <Box position="relative">
              <Box
                position="absolute"
                left={4}
                top="50%"
                transform="translateY(-50%)"
                zIndex={1}
              >
                <FaEnvelope color="gray.400" />
              </Box>
              <Input
                type="email"
                placeholder="Enter your email"
                value={signUpData.email}
                onChange={(e) => handleSignUpInputChange('email', e.target.value)}
                size="lg"
                pl={12}
                h="56px"
                borderRadius="xl"
                border="2px"
                borderColor="gray.200"
                _focus={{
                  borderColor: "blue.500",
                  boxShadow: "0 0 0 1px var(--chakra-colors-blue-500)"
                }}
                _hover={{ borderColor: "gray.300" }}
                className="input-field"
              />
            </Box>
            {signUpErrors.email && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signUpErrors.email}
              </Text>
            )}
          </Box>

          <Box w="full">
            <Text as="label" display="block" mb={2} fontWeight="semibold" color={textColor}>
              Password
            </Text>
            <Box position="relative">
              <Box
                position="absolute"
                left={4}
                top="50%"
                transform="translateY(-50%)"
                zIndex={1}
              >
                <FaLock color="gray.400" />
              </Box>
              <Input
                type={showPassword ? 'text' : 'password'}
                placeholder="Create a password"
                value={signUpData.password}
                onChange={(e) => handleSignUpInputChange('password', e.target.value)}
                size="lg"
                pl={12}
                pr={12}
                h="56px"
                borderRadius="xl"
                border="2px"
                borderColor="gray.200"
                _focus={{
                  borderColor: "blue.500",
                  boxShadow: "0 0 0 1px var(--chakra-colors-blue-500)"
                }}
                _hover={{ borderColor: "gray.300" }}
                className="input-field"
              />
              <Box
                position="absolute"
                right={4}
                top="50%"
                transform="translateY(-50%)"
                zIndex={1}
                cursor="pointer"
                onClick={() => setShowPassword(!showPassword)}
                p={2}
                borderRadius="md"
                _hover={{ bg: "gray.100" }}
              >
                {showPassword ? <FaEyeSlash color="gray.400" /> : <FaEye color="gray.400" />}
              </Box>
            </Box>
            
            {signUpData.password && (
              <Box mt={3}>
                <HStack justify="space-between" mb={2}>
                  <Text fontSize="sm" color={subtextColor}>Password strength:</Text>
                  <Text fontSize="sm" color={`${passwordStrength.color}.500`} fontWeight="bold">
                    {passwordStrength.text}
                  </Text>
                </HStack>
                <Box
                  bg="gray.200"
                  h={2}
                  borderRadius="full"
                  overflow="hidden"
                  className="password-strength-bar"
                >
                  <Box
                    bg={`${passwordStrength.color}.500`}
                    h="full"
                    w={`${passwordStrength.score * 20}%`}
                    transition="width 0.3s ease"
                    className="password-strength-fill"
                  />
                </Box>
              </Box>
            )}
            
            {signUpErrors.password && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signUpErrors.password}
              </Text>
            )}
          </Box>

          <Box w="full">
            <Text as="label" display="block" mb={2} fontWeight="semibold" color={textColor}>
              Confirm Password
            </Text>
            <Box position="relative">
              <Box
                position="absolute"
                left={4}
                top="50%"
                transform="translateY(-50%)"
                zIndex={1}
              >
                <FaLock color="gray.400" />
              </Box>
              <Input
                type={showConfirmPassword ? 'text' : 'password'}
                placeholder="Confirm your password"
                value={signUpData.confirmPassword}
                onChange={(e) => handleSignUpInputChange('confirmPassword', e.target.value)}
                size="lg"
                pl={12}
                pr={12}
                h="56px"
                borderRadius="xl"
                border="2px"
                borderColor="gray.200"
                _focus={{
                  borderColor: "blue.500",
                  boxShadow: "0 0 0 1px var(--chakra-colors-blue-500)"
                }}
                _hover={{ borderColor: "gray.300" }}
                className="input-field"
              />
              <Box
                position="absolute"
                right={4}
                top="50%"
                transform="translateY(-50%)"
                zIndex={1}
                cursor="pointer"
                onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                p={2}
                borderRadius="md"
                _hover={{ bg: "gray.100" }}
              >
                {showConfirmPassword ? <FaEyeSlash color="gray.400" /> : <FaEye color="gray.400" />}
              </Box>
            </Box>
            {signUpErrors.confirmPassword && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signUpErrors.confirmPassword}
              </Text>
            )}
          </Box>

          <Box w="full">
            <HStack gap={3}>
              <input
                type="checkbox"
                checked={signUpData.acceptTerms}
                onChange={(e) => handleSignUpInputChange('acceptTerms', e.target.checked)}
                style={{ width: '16px', height: '16px' }}
              />
              <Text fontSize="sm" color={subtextColor}>
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
            {signUpErrors.acceptTerms && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signUpErrors.acceptTerms}
              </Text>
            )}
          </Box>

          <Button
            type="submit"
            colorScheme="blue"
            size="lg"
            width="full"
            h="56px"
            borderRadius="xl"
            fontSize="lg"
            fontWeight="semibold"
            loading={isSignUpLoading}
            loadingText="Creating Account..."
            _hover={{ transform: "translateY(-2px)", boxShadow: "lg" }}
            transition="all 0.2s"
          >
            Create Account
          </Button>
        </VStack>
      </form>
    </Box>
  );
};

export default SignUp;
