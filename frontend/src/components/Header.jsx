import NavItem from "./header/NavItem.jsx";
import Button from "./header/Button.jsx";

export default function Header() {
  return (
    <>
      <header className="fixed top-0 right-0 left-0 z-11 flex items-center justify-between border-b border-gray-200 bg-white px-4 py-2">
        <div className="flex items-center justify-between gap-2">
          <img src="/LogoMedCen.svg" alt="Медцентр" className="h-14" />
          <nav className="flex items-center justify-between gap-4 text-sm font-medium">
            <NavItem to="/">ГЛАВНАЯ</NavItem>
            <NavItem to="/doctors">ВРАЧИ</NavItem>
            {/*<NavItem to="/login">ВХОД</NavItem>*/}
          </nav>
        </div>
        <div className="flex items-center justify-between gap-4">
          <div className="flex items-center justify-between">
            <img src="/Phone.svg" alt="Медцентр" className="h-6" />
            <span
              style={{ letterSpacing: "-0.11em" }}
              className="text-4xl font-normal text-red-600"
            >
              111
            </span>
          </div>
          <div className="items-center justify-between">
            <p className="text-xs font-normal text-red-600">КОНТАКТ-ЦЕНТР</p>
            <p className="text-base font-semibold text-red-600">
              07:00 - 22:00
            </p>
          </div>
          <Button>Запись онлайн</Button>
        </div>
      </header>
    </>
  );
}
