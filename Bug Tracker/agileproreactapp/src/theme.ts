import { extendTheme, ThemeConfig } from '@chakra-ui/react';

const config: ThemeConfig = {
    initialColorMode: 'light',
    useSystemColorMode: false
};

const styles = {
    global: () => ({
        body: {
            bg: "background",
            color: "text"
        }
    })
}

const semanticTokens = {
    colors: {
        background: {
            default: "#ffffff",
            _dark: "#1E1E1E"
        },
        text: {
            default: "#1A202C",
            _dark: "#FAFAFA",
            subtle: {
                default: "#44546f",
                _dark: "#b0b0b0"
            }
        },
        surface: {
            default: "#ffffff",
            _dark: "#2a2a2a"
        },
        overlay: {
            default: "#ffffff",
            _dark: "#333333"
        },
        hover: {
            default: "#f0f0f0",
            _dark: "#3a3a3a"
        },
        variants: {
            filled: {
                default: "gray.200",
                _dark: "#242424"
            }
        }
    }  
}

const components = {
    Modal: {
        baseStyle: {
            dialog: {
                bg: "surface",
                color: "text"
            }
        }
    },
    Card: {
        baseStyle: {
            container: {
                bg: "surface",
                color: "text"
            }
        },
        variants: {
            filled: {
                container: {
                    bg: "variants.filled"
                }
            }
        }
    },
    Menu: {
        baseStyle: {
            list: {
                bg: "overlay",
                color: "text"
            },
            item: {
                bg: "overlay",
                _hover: {
                    bg: "hover"
                }
            }
        }
    },
    
}

const theme = extendTheme({ config, styles, semanticTokens, components });

export default theme;