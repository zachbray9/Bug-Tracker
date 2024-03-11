import { ChakraProvider, ColorModeScript } from '@chakra-ui/react';
import React from 'react';
import theme from './theme'
import ReactDOM from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import router from './routes.tsx';
import 'bootstrap/dist/css/bootstrap.css'

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <ChakraProvider theme={theme}>
            <ColorModeScript initialColorMode={ theme.config.initialColorMode } />
            <RouterProvider router={router}/>
        </ChakraProvider>
  </React.StrictMode>
)
