export default function Button({ children }) {
    return (
        <button className="px-5 py-3 rounded-md text-white text-base bg-red-600 hover:text-black hover:bg-white hover:outline-1 hover:outline-red-600">
            {children}
        </button>
    );
}
