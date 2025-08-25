import React from 'react';
import { Box, HStack, Text } from '@chakra-ui/react';
import { Link as RouterLink } from 'react-router-dom';
import { FaChevronRight, FaHome } from 'react-icons/fa';

interface BreadcrumbItem {
  label: string;
  href?: string;
  isCurrentPage?: boolean;
}

interface BreadcrumbProps {
  items: BreadcrumbItem[];
  showHome?: boolean;
  separator?: React.ReactNode;
}

const BreadcrumbComponent: React.FC<BreadcrumbProps> = ({
  items,
  showHome = true,
  separator = <FaChevronRight color="#9CA3AF" />,
}) => {
  const breadcrumbItems = showHome 
    ? [{ label: 'Home', href: '/' }, ...items]
    : items;

  return (
    <Box
      mb={8}
      p={4}
      bg="gray.50"
      borderRadius="lg"
      border="1px"
      borderColor="gray.200"
    >
      <HStack gap="12px" fontSize="sm" color="gray.500">
        {breadcrumbItems.map((item, index) => (
          <React.Fragment key={index}>
            {index > 0 && separator}
            {item.href && !item.isCurrentPage ? (
              <RouterLink to={item.href}>
                <Text
                  color="blue.600"
                  fontWeight="500"
                  cursor="pointer"
                  _hover={{ 
                    color: "blue.700",
                    textDecoration: "underline",
                    transform: "translateY(-1px)",
                    transition: "all 0.2s ease"
                  }}
                  transition="all 0.2s ease"
                >
                  {index === 0 && showHome ? (
                    <HStack gap="4px">
                      <FaHome />
                      <span>{item.label}</span>
                    </HStack>
                  ) : (
                    item.label
                  )}
                </Text>
              </RouterLink>
            ) : (
              <Text 
                color="gray.800" 
                fontWeight="600"
                cursor="default"
              >
                {item.label}
              </Text>
            )}
          </React.Fragment>
        ))}
      </HStack>
    </Box>
  );
};

export default BreadcrumbComponent;
