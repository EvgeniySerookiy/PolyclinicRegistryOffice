import { Swiper, SwiperSlide } from 'swiper/react';
import { Pagination } from 'swiper/modules';
import 'swiper/css';
import 'swiper/css/pagination';
/*import './swiper-custom.css';*/

export default function FullscreenSlider() {
    const slides = [
        {
            image: '/doctors3.webp',
            title: 'Команда профессионалов',
            text: 'Сертифицированные врачи. Забота и внимание каждому пациенту.',
        },
        {
            image: '/mrt.webp',
            title: 'Современная диагностика',
            text: 'МРТ, УЗИ, лаборатория — всё в одном месте.',
        },
        {
            image: '/Fiz.webp',
            title: 'Реабилитация и профилактика',
            text: 'ЛФК, массаж, физиотерапия — здоровье на долгие годы.',
        },
    ];

    return (
        <Swiper
            modules={[Pagination]}
            pagination={{ clickable: true }}
            loop={true}
            className="w-screen h-screen"
        >
            {slides.map((slide, index) => (
                <SwiperSlide key={index}>
                    <div className="relative w-full h-full pt-[72px]">
                        <img
                            src={slide.image}
                            alt={slide.title}
                            className="w-full h-full object-cover"
                        />
                        <div className="absolute inset-0 bg-black/40 flex flex-col justify-center items-center text-center px-4">
                            <h2 className="text-3xl md:text-5xl font-bold text-white mb-4">
                                {slide.title}
                            </h2>
                            <p className="text-lg md:text-xl text-white max-w-xl">
                                {slide.text}
                            </p>
                        </div>
                    </div>
                </SwiperSlide>
            ))}
        </Swiper>
    );
}
