import Header from "../components/Header.jsx";
import Footer from "../components/Footer.jsx";
import FullscreenSlider from "../components/FullscreenSlider.jsx";
import SidePanel from "../components/SidePanel.jsx";

export default function Home() {
    return (
        <>
            <Header/>
                <main className="main">
                    <FullscreenSlider/>
                </main>
            <Footer/>
        </>
    )
}