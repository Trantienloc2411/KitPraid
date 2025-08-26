import React, { useState } from 'react';
import {
  Box,
  Button,
  Input,
  VStack,
  Text,
  HStack,
} from '@chakra-ui/react';
import { FaEye, FaEyeSlash, FaEnvelope, FaLock } from 'react-icons/fa';
import { Link as RouterLink } from 'react-router-dom';
import { toaster } from '../../components/ui/toaster';
import { useAppDispatch, useAppSelector } from '../../hooks/hook';
import { login } from '../../redux/authSlice';
interface SignInProps {
  textColor: string;
  subtextColor: string;
}

const SignIn: React.FC<SignInProps> = ({ textColor, subtextColor }) => {
  const { loading } = useAppSelector(state => state.auth);
  const dispatch = useAppDispatch();
  const [showPassword, setShowPassword] = useState(false);
  const [signInData, setSignInData] = useState({
    email: '',
    password: '',
  });
  const [signInErrors, setSignInErrors] = useState<{ email?: string; password?: string }>({});
  const [isSignInLoading, setIsSignInLoading] = useState(false);

  // Sign In validation
  const validateSignIn = () => {
    const newErrors: { email?: string; password?: string } = {};

    if (!signInData.email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(signInData.email)) {
      newErrors.email = 'Email is invalid';
    }

    if (!signInData.password) {
      newErrors.password = 'Password is required';
    } else if (signInData.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }

    setSignInErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  // Sign In handlers
  const handleSignInInputChange = (field: string, value: string) => {
    setSignInData(prev => ({ ...prev, [field]: value }));
    if (signInErrors[field as keyof typeof signInErrors]) {
      setSignInErrors(prev => ({ ...prev, [field]: '' }));
    }
  };

  const handleSignInSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Form submitted with data:', signInData);

    if (!validateSignIn()) {
      console.log('Validation failed');
      return;
    }

    console.log('Starting login process...');
    setIsSignInLoading(true);

    try {
      // Dispatch login action and wait for result
      console.log('Dispatching login action...');
      const result = await dispatch(login(signInData)).unwrap();
      console.log('Login successful:', result);
      toaster.success({
        title: 'Success',
        description: 'Signed in successfully!',
        duration: 3000,
        closable: true,
      });
    } catch (error) {
      console.error('Login error:', error);
      toaster.error({
        title: 'Error',
        description: 'Failed to sign in. Please try again.',
        duration: 3000,
        closable: true,
      });
    } finally {
      setIsSignInLoading(false);
    }
  };

  return (
    <Box px={0} pt={6}>
      <form onSubmit={handleSignInSubmit}>
        <VStack gap={5}>
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
                value={signInData.email}
                onChange={(e) => handleSignInInputChange('email', e.target.value)}
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
            {signInErrors.email && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signInErrors.email}
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
                placeholder="Enter your password"
                value={signInData.password}
                onChange={(e) => handleSignInInputChange('password', e.target.value)}
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
            {signInErrors.password && (
              <Text color="red.500" fontSize="sm" mt={2}>
                {signInErrors.password}
              </Text>
            )}
          </Box>

          <HStack justify="space-between" w="full" fontSize="sm">
            <RouterLink to="/auth/login/forgot-password">
              <Text color="blue.500" _hover={{ textDecoration: 'underline' }} cursor="pointer">
                Forgot Password?
              </Text>
            </RouterLink>
          </HStack>

          <Button
            type="submit"
            colorPalette="blue"
            size="lg"
            width="full"
            height="56px"
            borderRadius="xl"
            fontSize="lg"
            fontWeight="semibold"
            loading={isSignInLoading}
            loadingText="Signing In..."
            _hover={{ transform: "translateY(-2px)", boxShadow: "lg" }}
            transition="all 0.2s"
          >
            Sign In
          </Button>
        </VStack>
      </form>
    </Box>
  );
};

export default SignIn;
