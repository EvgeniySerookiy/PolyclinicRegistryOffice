import { useEffect, useState } from "react";
import { useSidePanel } from "../contexsts/SidePanelContext.jsx";
import ReactDropdownSelect from "react-dropdown-select";

export default function SidePanel() {
  const { isOpen, closePanel } = useSidePanel();

  const [specializations, setSpecializations] = useState([]);
  const [selectedSpec, setSelectedSpec] = useState([]);
  const [doctors, setDoctors] = useState([]);
  const [selectedDoctor, setSelectedDoctor] = useState([]);
  const [freeSlots, setFreeSlots] = useState([]);
  const [selectedDate, setSelectedDate] = useState(null);
  const [availableTimes, setAvailableTimes] = useState([]);
  const [selectedTime, setSelectedTime] = useState([]);

  const [patient, setPatient] = useState({
    lastName: "",
    firstName: "",
    middleName: "",
    phone: "",
    email: "",
    dateOfBirth: "",
  });

  const [loadingSpec, setLoadingSpec] = useState(true);
  const [errorSpec, setErrorSpec] = useState(null);
  const [loadingDoctors, setLoadingDoctors] = useState(false);
  const [errorDoctors, setErrorDoctors] = useState(null);
  const [loadingSlots, setLoadingSlots] = useState(false);
  const [errorSlots, setErrorSlots] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [success, setSuccess] = useState(false);
  const [errorSubmit, setErrorSubmit] = useState(null);

  // Загрузка специализаций
  useEffect(() => {
    async function fetchSpecialization() {
      try {
        const response = await fetch(
          "http://localhost:5233/SpecializationRead",
        );
        if (!response.ok) throw new Error("Ошибка загрузки специализаций");
        const data = await response.json();
        setSpecializations(data.map((s) => ({ value: s.id, label: s.name })));
      } catch (err) {
        setErrorSpec(err.message);
      } finally {
        setLoadingSpec(false);
      }
    }
    fetchSpecialization();
  }, []);

  // Загрузка врачей при выборе специализации
  useEffect(() => {
    if (selectedSpec.length > 0) {
      const specializationId = selectedSpec[0].value;
      setLoadingDoctors(true);
      setDoctors([]);
      setSelectedDoctor([]);
      setErrorDoctors(null);

      async function fetchDoctors() {
        try {
          const response = await fetch(
            `http://localhost:5233/SpecialistRead/${specializationId}`,
          );
          if (!response.ok) throw new Error("Ошибка загрузки врачей");
          const data = await response.json();
          setDoctors(
            data.map((d) => ({
              value: d.id,
              label: `${d.lastName} ${d.firstName}`,
            })),
          );
        } catch (err) {
          setErrorDoctors(err.message);
        } finally {
          setLoadingDoctors(false);
        }
      }

      fetchDoctors();
    }
  }, [selectedSpec]);

  // Загрузка свободных слотов при выборе врача
  useEffect(() => {
    if (selectedDoctor.length > 0) {
      const specialistId = selectedDoctor[0].value;
      setLoadingSlots(true);
      setFreeSlots([]);
      setSelectedDate(null);
      setAvailableTimes([]);
      setSelectedTime([]);
      setErrorSlots(null);

      async function fetchFreeSlots() {
        try {
          const response = await fetch(
            `http://localhost:5233/SheduleSlotRead/${specialistId}/free-slots`,
          );
          if (!response.ok) throw new Error("Ошибка загрузки слотов");
          const data = await response.json();
          setFreeSlots(data);
        } catch (err) {
          setErrorSlots(err.message);
        } finally {
          setLoadingSlots(false);
        }
      }

      fetchFreeSlots();
    }
  }, [selectedDoctor]);

  // Фильтрация времени при выборе даты
  const availableDates = [...new Set(freeSlots.map((slot) => slot.date))];

  function handleDateSelect(date) {
    setSelectedDate(date);
    const times = freeSlots
      .filter((slot) => slot.date === date)
      .map((slot) => ({
        value: slot.startTime,
        label: slot.startTime,
      }));
    setAvailableTimes(times);
    setSelectedTime([]);
  }

  // 🔹 Отправка данных пациента
  async function handleSubmit(e) {
    e.preventDefault();
    if (!selectedDoctor.length || !selectedDate || !selectedTime.length) return;

    setSubmitting(true);
    setErrorSubmit(null);
    setSuccess(false);

    const payload = {
      specialistId: selectedDoctor[0].value,
      appointmentDate: new Date(selectedDate).toISOString(), // ✅ формат ISO
      appointmentTime: selectedTime[0].value,
      patientLastName: patient.lastName,
      patientFirstName: patient.firstName,
      patientMiddleName: patient.middleName,
      patientPhoneNumber: patient.phone,
      patientEmail: patient.email,
      patientDateOfBirth: patient.dateOfBirth
        ? new Date(patient.dateOfBirth).toISOString()
        : null,
    };

    try {
      const response = await fetch("http://localhost:5233/AppointmentWrite", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const text = await response.text();
        throw new Error(text || "Ошибка при записи");
      }

      setSuccess(true);
      setPatient({
        lastName: "",
        firstName: "",
        middleName: "",
        phone: "",
        email: "",
        dateOfBirth: "",
      });
      setSelectedSpec([]);
      setSelectedDoctor([]);
      setSelectedDate(null);
      setSelectedTime([]);
    } catch (err) {
      setErrorSubmit(err.message);
    } finally {
      setSubmitting(false);
    }
  }

  // Блокировка прокрутки фона при открытой панели
  useEffect(() => {
    document.body.style.overflow = isOpen ? "hidden" : "";
    return () => (document.body.style.overflow = "");
  }, [isOpen]);

  return (
    <>
      {isOpen && (
        <div
          onClick={closePanel}
          className="fixed inset-0 z-40 bg-black/50 backdrop-blur-sm"
        />
      )}

      <div
        className={`fixed top-0 right-0 z-50 h-full w-[30vw] transform bg-white shadow-xl transition-transform duration-300 ${
          isOpen ? "translate-x-0" : "translate-x-full"
        }`}
      >
        <div className="flex items-center justify-between border-b p-4">
          <h2 className="text-base font-semibold text-black">ЗАПИСЬ ОНЛАЙН</h2>
          <button
            onClick={closePanel}
            className="cursor-pointer text-2xl leading-none text-gray-500 hover:text-black"
          >
            &times;
          </button>
        </div>

        <div className="h-[calc(100vh-64px)] space-y-6 overflow-y-auto p-4 text-black">
          {/* Специализация */}
          {loadingSpec && <p>Загрузка специализаций...</p>}
          {errorSpec && <p className="text-red-600">{errorSpec}</p>}
          {!loadingSpec && !errorSpec && (
            <ReactDropdownSelect
              options={specializations}
              values={selectedSpec}
              onChange={setSelectedSpec}
              placeholder="Выберите специализацию"
              className="w-full"
            />
          )}

          {/* Врачи */}
          {selectedSpec.length > 0 && (
            <>
              {loadingDoctors && <p>Загрузка врачей...</p>}
              {errorDoctors && <p className="text-red-600">{errorDoctors}</p>}
              {!loadingDoctors && !errorDoctors && (
                <ReactDropdownSelect
                  options={doctors}
                  values={selectedDoctor}
                  onChange={setSelectedDoctor}
                  placeholder="Выберите врача"
                  className="w-full"
                />
              )}
            </>
          )}

          {/* Даты и время */}
          {selectedDoctor.length > 0 && (
            <>
              {loadingSlots && <p>Загрузка слотов...</p>}
              {errorSlots && <p className="text-red-600">{errorSlots}</p>}
              {!loadingSlots && !errorSlots && freeSlots.length > 0 && (
                <>
                  <div className="grid grid-cols-3 gap-2">
                    {availableDates.map((date) => (
                      <button
                        key={date}
                        onClick={() => handleDateSelect(date)}
                        className={`rounded border p-2 text-sm ${
                          selectedDate === date
                            ? "bg-red-500 text-white"
                            : "bg-white hover:bg-gray-100"
                        }`}
                      >
                        {new Date(date).toLocaleDateString()}
                      </button>
                    ))}
                  </div>

                  {selectedDate && (
                    <ReactDropdownSelect
                      options={availableTimes}
                      values={selectedTime}
                      onChange={setSelectedTime}
                      placeholder="Выберите время"
                      className="w-full"
                    />
                  )}
                </>
              )}
            </>
          )}

          {/*  Данные пациента */}
          {selectedTime.length > 0 && (
            <form onSubmit={handleSubmit} className="space-y-3">
              <h3 className="text-sm font-semibold">Данные пациента</h3>

              <input
                type="text"
                placeholder="Фамилия"
                value={patient.lastName}
                onChange={(e) =>
                  setPatient({ ...patient, lastName: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
                required
              />
              <input
                type="text"
                placeholder="Имя"
                value={patient.firstName}
                onChange={(e) =>
                  setPatient({ ...patient, firstName: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
                required
              />
              <input
                type="text"
                placeholder="Отчество"
                value={patient.middleName}
                onChange={(e) =>
                  setPatient({ ...patient, middleName: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
              />
              <input
                type="tel"
                placeholder="Телефон"
                value={patient.phone}
                onChange={(e) =>
                  setPatient({ ...patient, phone: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
                required
              />
              <input
                type="email"
                placeholder="Email"
                value={patient.email}
                onChange={(e) =>
                  setPatient({ ...patient, email: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
              />
              <input
                type="date"
                placeholder="Дата рождения"
                value={patient.dateOfBirth}
                onChange={(e) =>
                  setPatient({ ...patient, dateOfBirth: e.target.value })
                }
                className="w-full rounded border p-2 text-sm"
              />

              {errorSubmit && (
                <p className="text-sm text-red-600">{errorSubmit}</p>
              )}

              <button
                type="submit"
                disabled={submitting}
                className="w-full rounded bg-red-600 py-2 text-white transition hover:bg-red-700"
              >
                {submitting ? "Отправка..." : "Записаться"}
              </button>
            </form>
          )}
        </div>
      </div>
    </>
  );
}
