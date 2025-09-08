import { Box, Text } from "@chakra-ui/react"
import { useState } from "react";
import "./Tag.css";

export default function Tag({tag, color, bColor, tColor, isHighlighted = false}: {
    tag: string, 
    color: string, 
    bColor: string, 
    tColor: string,
    isHighlighted?: boolean
}) {
    const tagColor = color || "#343a40" // Default dark grey background
    const borderColor = bColor || "#495057" // Default border color
    const textColor = tColor || "white"

    const [isHovered, setIsHovered] = useState(false);
    const [isSelected, setIsSelected] = useState(false);
    
    const handleMouseEnter = () => {
        setIsHovered(true);
    }

    const handleMouseLeave = () => {
        setIsHovered(false);
    }

    const handleClick = () => {
        setIsSelected(!isSelected);
    }

    return (
        <Box 
            className={`tag ${isSelected ? 'selected' : ''} ${isHighlighted ? 'highlighted' : ''}`}
            bg={isSelected ? "#fbbf24" : tagColor} 
            color={textColor} 
            borderColor={isHighlighted ? "#fbbf24" : borderColor} 
            borderWidth={isHighlighted ? 2 : 1} 
            borderStyle={isHighlighted ? "dashed" : "solid"}
            borderRadius="md" 
            px={3}
            py={2} 
            fontSize="xs" 
            fontWeight="bold"
            cursor="pointer"
            transition="all 0.2s ease"
            _hover={{
                bg: isHighlighted ? "#fbbf24" : "#495057",
                transform: "translateY(-1px)",
                boxShadow: "0 2px 8px rgba(0,0,0,0.2)"
            }}
            position="relative"
            overflow="hidden"
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
            onClick={handleClick}
            tabIndex={0}
            role="button"
            aria-pressed={isSelected}
            maxW="fit-content"
            minW="fit-content"
            textAlign="center"
            whiteSpace="nowrap"
        >
            <Text 
                color={textColor}
                position="relative"
                zIndex={2}
                userSelect="none"
                textTransform="uppercase"
                fontSize="xs"
                fontWeight="600"
                letterSpacing="0.5px"
                lineHeight="1"
            >
                {tag}
            </Text>
            
            {/* Highlighted tag pattern overlay */}
            {isHighlighted && (
                <Box
                    position="absolute"
                    top="0"
                    left="0"
                    right="0"
                    bottom="0"
                    backgroundImage="repeating-linear-gradient(45deg, transparent, transparent 2px, rgba(128, 0, 128, 0.3) 2px, rgba(128, 0, 128, 0.3) 4px)"
                    opacity="0.6"
                    zIndex={1}
                />
            )}
        </Box>
    )
}
