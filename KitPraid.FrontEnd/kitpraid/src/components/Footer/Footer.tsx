import { Box } from "@chakra-ui/react"
import { Newsletter } from "./Newsletter"
import { QuickLink } from "./QuickLink"

export const Footer = () => {
    return (
        <Box  mx="auto" alignContent={"center"}>
            <Newsletter />
            <QuickLink />
        </Box>
    )   
}