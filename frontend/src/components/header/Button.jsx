import { useSidePanel } from "../../contexsts/SidePanelContext.jsx";

export default function Button({ children }) {
  const { openPanel } = useSidePanel();

  return (
    <button
      onClick={openPanel}
      className="rounded-md bg-red-600 px-5 py-3 text-base text-white transition-colors hover:bg-white hover:text-black hover:outline-1 hover:outline-red-600"
    >
      {children}
    </button>
  );
}
