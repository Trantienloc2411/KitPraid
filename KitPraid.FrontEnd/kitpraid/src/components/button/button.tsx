import { Button as ChakraButton } from "@chakra-ui/react"
export default function Button({ children, ...props }: { children: React.ReactNode } & React.ComponentProps<typeof ChakraButton>) {
    return (
        <ChakraButton {...props} className="bg-blue-500 text-white p-2 rounded-md">
            {children}
        </ChakraButton>
    )
}