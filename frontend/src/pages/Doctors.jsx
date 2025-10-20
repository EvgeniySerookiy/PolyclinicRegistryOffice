import { useEffect, useState } from "react";
import Header from "../components/Header.jsx";
import Footer from "../components/Footer.jsx";

export default function Doctors() {
    const [doctors, setDoctors] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchDoctors() {
            try {
                const response = await fetch("http://localhost:5233/SpecialistRead");
                if (!response.ok) throw new Error("Ошибка загрузки данных");
                const data = await response.json();
                setDoctors(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }

        fetchDoctors();
    }, []);

    return (
        <>
            <Header/>
            <main className="text-black py-10 px-4 min-h-screen pt-[72px]">
                <div className="w-[1250px] mx-auto">
                    <h1 className="text-2xl font-semibold my-7">Наши специалисты</h1>

                    {loading && <p className="text-center">Загрузка...</p>}
                    {error && <p className="text-center text-red-600">{error}</p>}

                    {!loading && !error && doctors.length === 0 && (
                        <p className="text-center text-gray-600">Врачи не найдены</p>
                    )}

                    {!loading && !error && doctors.length > 0 && (
                        <div className="overflow-x-auto rounded-md border border-gray-300 bg-white">
                            <table className="min-w-full text-sm">
                                <thead className="bg-gray-200">
                                <tr>
                                    <th className="px-4 py-4 text-left">Фамилия</th>
                                    <th className="px-4 py-4 text-left">Имя</th>
                                    <th className="px-4 py-4 text-left">Отчество</th>
                                    <th className="px-4 py-4 text-left">Специальность</th>
                                    <th className="px-4 py-4 text-left">Кабинет</th>
                                    <th className="px-4 py-4 text-left">Описание</th>
                                </tr>
                                </thead>
                                <tbody>
                                {doctors.map((doc) => (
                                    <tr key={doc.id} className="border-t border-gray-300 hover:bg-gray-100">
                                        <td className="px-4 py-3">{doc.lastName}</td>
                                        <td className="px-4 py-3">{doc.firstName}</td>
                                        <td className="px-4 py-3">{doc.middleName}</td>
                                        <td className="px-4 py-3">{doc.specializationName}</td>
                                        <td className="px-4 py-3">{doc.officeNumber}</td>
                                        <td className="px-4 py-3">{doc.description}</td>
                                    </tr>
                                ))}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>
            </main>
            <Footer/>
        </>
    );
}
