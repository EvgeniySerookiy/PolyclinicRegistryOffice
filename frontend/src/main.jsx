import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import {SidePanelProvider} from "./contexsts/SidePanelContext.jsx";

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <SidePanelProvider>
            <App/>
        </SidePanelProvider>
    </StrictMode>
)
