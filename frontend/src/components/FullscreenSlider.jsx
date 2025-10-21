import { Swiper, SwiperSlide } from "swiper/react";
import { Pagination } from "swiper/modules";
import "swiper/css";
import "swiper/css/pagination";

export default function FullscreenSlider() {
  const slides = [
    {
      image: "/doctors3.webp",
      title: "Команда профессионалов",
      text: "Сертифицированные врачи. Забота и внимание каждому пациенту.",
    },
    {
      image: "/mrt.webp",
      title: "Современная диагностика",
      text: "МРТ, УЗИ, лаборатория — всё в одном месте.",
    },
    {
      image: "/Fiz.webp",
      title: "Реабилитация и профилактика",
      text: "ЛФК, массаж, физиотерапия — здоровье на долгие годы.",
    },
  ];

  return (
    <Swiper
      modules={[Pagination]}
      pagination={{ clickable: true }}
      loop={true}
      className="h-screen w-screen"
    >
      {slides.map((slide, index) => (
        <SwiperSlide key={index}>
          <div className="relative h-full w-full pt-[72px]">
            <img
              src={slide.image}
              alt={slide.title}
              className="h-full w-full object-cover"
            />
            <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/40 px-4 text-center">
              <h2 className="mb-4 text-3xl font-bold text-white md:text-5xl">
                {slide.title}
              </h2>
              <p className="max-w-xl text-lg text-white md:text-xl">
                {slide.text}
              </p>
            </div>
          </div>
        </SwiperSlide>
      ))}
    </Swiper>
  );
}
