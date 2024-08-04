import { ChakraProvider, ColorModeScript } from '@chakra-ui/react';
import React from 'react';
import theme from './theme'
import ReactDOM from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import router from './Router/routes.tsx';
import { HelmetProvider } from 'react-helmet-async';

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <HelmetProvider>
            <ChakraProvider theme={theme}>
                <ColorModeScript initialColorMode={ theme.config.initialColorMode } />
                <RouterProvider router={router}/>
            </ChakraProvider>
        </HelmetProvider>
  </React.StrictMode>
)
