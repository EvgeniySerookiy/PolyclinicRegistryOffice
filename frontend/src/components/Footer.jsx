import NavItem from "./header/NavItem.jsx";
import Button from "./header/Button.jsx";

export default function Footer() {
    return (
        <footer className="bg-gray-200 py-4">
            <div className="w-[1250px] mx-auto text-black py-6 px-4">
                <div className="grid grid-cols-1 md:grid-cols-[3fr_2fr_5fr] gap-10 text-sm">
                    <div className="space-y-2">
                        <p className="text-sm font-semibold">ООО «МЕДИКАЛ ЦЕНТР»</p>
                        <p>220005, Республика Беларусь,<br/>г. Минск, ул. Гикало, д.1, пом.10</p>
                        <p>Лицензия № 02040/536</p>
                        <p className="text-xs italic">
                            * Прейскурант доступен у администраторов. Цены на сайте — справочные.
                            Стоимость для иностранных граждан уточняйте по номеру 111.
                        </p>
                    </div>

                    <div className="space-y-2">
                        <p className="text-sm font-semibold">ИНФОРМАЦИЯ</p>
                        <nav className="grid grid-cols-1 items-center justify-between gap-1 text-sm font-normal">
                            <NavItem to="">Вакансии</NavItem>
                            <NavItem to="">Обслуживание страховых клиентов</NavItem>
                            <NavItem to="">Способы оплаты</NavItem>
                            <NavItem to="">Реквизиты</NavItem>
                            <NavItem to="">Как добраться</NavItem>
                        </nav>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 border border-gray-300 rounded-md">
                        <div className="flex flex-col justify-center gap-4 px-6 py-4">
                            <div className="flex items-center">
                                <img src="/Phone.svg" alt="Медцентр" className="h-6" />
                                <span style={{ letterSpacing: '-0.11em' }} className="text-4xl font-normal text-red-600">111</span>
                            </div>
                            <div>
                                <p className="text-xs font-normal text-red-600">КОНТАКТ-ЦЕНТР</p>
                                <p className="text-base font-semibold text-red-600">07:00 - 22:00</p>
                            </div>
                            <Button>Запись онлайн</Button>
                        </div>
                        <div className="flex flex-col justify-center gap-2 px-10 py-4 border-l border-gray-300">
                            <p className="text-sm font-semibold">АДРЕСА МЕДЦЕНТРОВ</p>
                            <ul className="space-y-1">
                                <li>пр. Независимости, 58а</li>
                                <li>пр. Независимости, 95</li>
                                <li>ул. Гикало, 1</li>
                                <li>ул. Пр.Мира, 15</li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="text-center mt-5">
                    <p className="text-xs font-normal text-black">© Многопрофильная медицинская компания «МЕДИКАЛ ЦЕНТР»</p>
                </div>
            </div>
        </footer>
    );
}
