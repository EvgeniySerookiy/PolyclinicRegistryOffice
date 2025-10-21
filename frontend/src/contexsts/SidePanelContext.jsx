import { createContext, useContext, useState } from "react";

// Создаём контекст
const SidePanelContext = createContext();

// Хук для удобного доступа к контексту
export const useSidePanel = () => useContext(SidePanelContext);

// Провайдер
export const SidePanelProvider = ({ children }) => {
    const [isOpen, setIsOpen] = useState(false);

    const openPanel = () => setIsOpen(true);
    const closePanel = () => setIsOpen(false);

    return (
        <SidePanelContext.Provider value={{ isOpen, openPanel, closePanel }}>
            {children}
        </SidePanelContext.Provider>
    );
};
