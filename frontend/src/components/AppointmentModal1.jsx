// // components/AppointmentModal.jsx
// import { useState, useEffect } from 'react';
//
// export default function AppointmentModal({ isOpen, onClose }) {
//     const [currentStep, setCurrentStep] = useState(1);
//     const [formData, setFormData] = useState({
//         specialization: null,
//         specialist: null,
//         scheduleSlot: null,
//         patient: {
//             lastName: '',
//             firstName: '',
//             middleName: '',
//             phoneNumber: '',
//             email: '',
//             dateOfBirth: ''
//         }
//     });
//
//     const [specializations, setSpecializations] = useState([]);
//     const [specialists, setSpecialists] = useState([]);
//     const [availableSlots, setAvailableSlots] = useState([]);
//     const [loading, setLoading] = useState(false);
//     const [showDropdown, setShowDropdown] = useState({
//         specialization: false,
//         specialist: false,
//         scheduleSlot: false
//     });
//
//     // Сброс состояния при открытии модального окна
//     useEffect(() => {
//         if (isOpen) {
//             setCurrentStep(1);
//             setFormData({
//                 specialization: null,
//                 specialist: null,
//                 scheduleSlot: null,
//                 patient: {
//                     lastName: '',
//                     firstName: '',
//                     middleName: '',
//                     phoneNumber: '',
//                     email: '',
//                     dateOfBirth: ''
//                 }
//             });
//             setShowDropdown({
//                 specialization: false,
//                 specialist: false,
//                 scheduleSlot: false
//             });
//             loadSpecializations();
//         }
//     }, [isOpen]);
//
//     // Загрузка специализаций
//     const loadSpecializations = async () => {
//         try {
//             setLoading(true);
//             const response = await fetch("http://localhost:5233/SpecializationRead");
//             if (response.ok) {
//                 const data = await response.json();
//                 setSpecializations(data);
//             }
//         } catch (error) {
//             console.error('Ошибка загрузки специализаций:', error);
//         } finally {
//             setLoading(false);
//         }
//     };
//
//     // Загрузка врачей по специализации
//     const loadSpecialists = async (specializationId) => {
//         try {
//             setLoading(true);
//             const response = await fetch(`http://localhost:5233/SpecialistRead/${specializationId}`);
//             if (response.ok) {
//                 const data = await response.json();
//                 setSpecialists(data);
//             }
//         } catch (error) {
//             console.error('Ошибка загрузки врачей:', error);
//         } finally {
//             setLoading(false);
//         }
//     };
//
//     // Загрузка свободных слотов врача
//     const loadAvailableSlots = async (specialistId) => {
//         try {
//             setLoading(true);
//             const response = await fetch(`http://localhost:5233/SheduleSlotRead/${specialistId}/free-slots`);
//             if (response.ok) {
//                 const data = await response.json();
//                 setAvailableSlots(data);
//             }
//         } catch (error) {
//             console.error('Ошибка загрузки слотов:', error);
//         } finally {
//             setLoading(false);
//         }
//     };
//
//     const handleSpecializationSelect = (specialization) => {
//         setFormData(prev => ({ ...prev, specialization, specialist: null, scheduleSlot: null }));
//         setShowDropdown(prev => ({ ...prev, specialization: false }));
//         loadSpecialists(specialization.id);
//     };
//
//     const handleSpecialistSelect = (specialist) => {
//         setFormData(prev => ({ ...prev, specialist, scheduleSlot: null }));
//         setShowDropdown(prev => ({ ...prev, specialist: false }));
//         loadAvailableSlots(specialist.id);
//     };
//
//     const handleSlotSelect = (slot) => {
//         setFormData(prev => ({ ...prev, scheduleSlot: slot }));
//         setShowDropdown(prev => ({ ...prev, scheduleSlot: false }));
//     };
//
//     const handlePatientDataChange = (field, value) => {
//         setFormData(prev => ({
//             ...prev,
//             patient: { ...prev.patient, [field]: value }
//         }));
//     };
//
//     const handleSubmit = async (e) => {
//         e.preventDefault();
//         try {
//             setLoading(true);
//
//             const appointmentData = {
//                 patientLastName: formData.patient.lastName,
//                 patientFirstName: formData.patient.firstName,
//                 patientMiddleName: formData.patient.middleName,
//                 patientPhoneNumber: formData.patient.phoneNumber,
//                 patientEmail: formData.patient.email,
//                 patientDateOfBirth: formData.patient.dateOfBirth,
//                 specialistId: formData.specialist.id,
//                 appointmentDate: formData.scheduleSlot.date,
//                 appointmentTime: formData.scheduleSlot.startTime
//             };
//
//             const response = await fetch('http://localhost:5233/AppointmentWrite', {
//                 method: 'POST',
//                 headers: {
//                     'Content-Type': 'application/json',
//                 },
//                 body: JSON.stringify(appointmentData)
//             });
//
//             if (response.ok) {
//                 alert('Запись успешно создана!');
//                 onClose();
//             } else {
//                 const error = await response.json();
//                 alert(`Ошибка: ${error.message}`);
//             }
//         } catch (error) {
//             console.error('Ошибка при создании записи:', error);
//             alert('Произошла ошибка при создании записи');
//         } finally {
//             setLoading(false);
//         }
//     };
//
//     const handleNext = () => {
//         if (currentStep < 4) {
//             setCurrentStep(prev => prev + 1);
//         }
//     };
//
//     const handleBack = () => {
//         if (currentStep > 1) {
//             setCurrentStep(prev => prev - 1);
//         }
//     };
//
//     const toggleDropdown = (dropdown) => {
//         setShowDropdown(prev => ({
//             ...prev,
//             [dropdown]: !prev[dropdown]
//         }));
//     };
//
//     // Группировка слотов по дате
//     const groupedSlots = availableSlots.reduce((acc, slot) => {
//         const date = new Date(slot.date).toLocaleDateString('ru-RU');
//         if (!acc[date]) {
//             acc[date] = [];
//         }
//         acc[date].push(slot);
//         return acc;
//     }, {});
//
//     if (!isOpen) return null;
//
//     return (
//         <div className="fixed inset-0 bg-black bg-opacity-60 flex items-center justify-center z-50 p-4">
//             <div className="bg-white rounded-xl shadow-2xl w-full max-w-2xl max-h-[95vh] overflow-y-auto border border-gray-200">
//                 {/* Заголовок */}
//                 <div className="flex justify-between items-center p-6 border-b border-gray-200 bg-gradient-to-r from-blue-50 to-gray-50">
//                     <h2 className="text-2xl font-bold text-gray-800">Запись на прием</h2>
//                     <button
//                         onClick={onClose}
//                         className="text-gray-500 hover:text-gray-700 text-2xl bg-white rounded-full p-1 shadow-sm hover:shadow-md transition-all"
//                     >
//                         ×
//                     </button>
//                 </div>
//
//                 {/* Индикатор прогресса */}
//                 <div className="flex justify-center p-6 border-b border-gray-200 bg-white">
//                     {[
//                         { number: 1, label: 'Специализация' },
//                         { number: 2, label: 'Врач' },
//                         { number: 3, label: 'Время' },
//                         { number: 4, label: 'Данные' }
//                     ].map((step, index) => (
//                         <div key={step.number} className="flex items-center">
//                             <div className="flex flex-col items-center">
//                                 <div className={`w-10 h-10 rounded-full flex items-center justify-center text-sm font-semibold border-2 ${
//                                     step.number === currentStep
//                                         ? 'bg-blue-600 text-white border-blue-600'
//                                         : step.number < currentStep
//                                             ? 'bg-green-500 text-white border-green-500'
//                                             : 'bg-white text-gray-400 border-gray-300'
//                                 }`}>
//                                     {step.number}
//                                 </div>
//                                 <span className="text-xs mt-2 text-gray-600 hidden sm:block">{step.label}</span>
//                             </div>
//                             {step.number < 4 && (
//                                 <div className={`w-8 h-1 mx-4 ${
//                                     step.number < currentStep ? 'bg-green-500' : 'bg-gray-300'
//                                 }`} />
//                             )}
//                         </div>
//                     ))}
//                 </div>
//
//                 <div className="p-6 bg-gray-50">
//                     {loading && (
//                         <div className="text-center py-8">
//                             <div className="inline-block animate-spin rounded-full h-10 w-10 border-b-2 border-blue-600"></div>
//                             <p className="mt-3 text-gray-600 font-medium">Загрузка...</p>
//                         </div>
//                     )}
//
//                     {/* Шаг 1: Выбор специализации */}
//                     {currentStep === 1 && !loading && (
//                         <div className="space-y-4">
//                             <h3 className="text-lg font-semibold text-gray-800 mb-2">Выберите специализацию</h3>
//                             <div className="relative">
//                                 <button
//                                     onClick={() => toggleDropdown('specialization')}
//                                     className="w-full p-4 text-left bg-white border-2 border-gray-300 rounded-xl hover:border-blue-500 focus:border-blue-500 focus:ring-2 focus:ring-blue-200 transition-all duration-200 flex justify-between items-center"
//                                 >
//                                     <span className={formData.specialization ? "text-gray-800" : "text-gray-500"}>
//                                         {formData.specialization ? formData.specialization.name : "Выберите специализацию"}
//                                     </span>
//                                     <svg className={`w-5 h-5 text-gray-400 transform transition-transform ${showDropdown.specialization ? 'rotate-180' : ''}`}
//                                          fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                         <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
//                                     </svg>
//                                 </button>
//
//                                 {showDropdown.specialization && (
//                                     <div className="absolute z-10 w-full mt-2 bg-white border border-gray-200 rounded-xl shadow-lg max-h-60 overflow-y-auto">
//                                         {specializations.map(spec => (
//                                             <button
//                                                 key={spec.id}
//                                                 onClick={() => handleSpecializationSelect(spec)}
//                                                 className="w-full p-4 text-left hover:bg-blue-50 border-b border-gray-100 last:border-b-0 transition-colors"
//                                             >
//                                                 <div className="font-medium text-gray-800">{spec.name}</div>
//                                             </button>
//                                         ))}
//                                     </div>
//                                 )}
//                             </div>
//
//                             {formData.specialization && (
//                                 <div className="flex justify-end mt-6">
//                                     <button
//                                         onClick={handleNext}
//                                         className="px-8 py-3 bg-blue-600 text-white rounded-xl hover:bg-blue-700 font-medium transition-colors shadow-md hover:shadow-lg"
//                                     >
//                                         Далее →
//                                     </button>
//                                 </div>
//                             )}
//                         </div>
//                     )}
//
//                     {/* Шаг 2: Выбор врача */}
//                     {currentStep === 2 && !loading && (
//                         <div className="space-y-4">
//                             <div className="flex items-center space-x-2 mb-4">
//                                 <button
//                                     onClick={handleBack}
//                                     className="p-2 text-gray-600 hover:text-gray-800 hover:bg-gray-100 rounded-lg transition-colors"
//                                 >
//                                     ← Назад
//                                 </button>
//                                 <h3 className="text-lg font-semibold text-gray-800">
//                                     Выберите врача {formData.specialization && `(${formData.specialization.name})`}
//                                 </h3>
//                             </div>
//
//                             <div className="relative">
//                                 <button
//                                     onClick={() => toggleDropdown('specialist')}
//                                     className="w-full p-4 text-left bg-white border-2 border-gray-300 rounded-xl hover:border-blue-500 focus:border-blue-500 focus:ring-2 focus:ring-blue-200 transition-all duration-200 flex justify-between items-center"
//                                 >
//                                     <span className={formData.specialist ? "text-gray-800" : "text-gray-500"}>
//                                         {formData.specialist
//                                             ? `${formData.specialist.lastName} ${formData.specialist.firstName} ${formData.specialist.middleName}`
//                                             : "Выберите врача"}
//                                     </span>
//                                     <svg className={`w-5 h-5 text-gray-400 transform transition-transform ${showDropdown.specialist ? 'rotate-180' : ''}`}
//                                          fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                         <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
//                                     </svg>
//                                 </button>
//
//                                 {showDropdown.specialist && (
//                                     <div className="absolute z-10 w-full mt-2 bg-white border border-gray-200 rounded-xl shadow-lg max-h-60 overflow-y-auto">
//                                         {specialists.map(specialist => (
//                                             <button
//                                                 key={specialist.id}
//                                                 onClick={() => handleSpecialistSelect(specialist)}
//                                                 className="w-full p-4 text-left hover:bg-blue-50 border-b border-gray-100 last:border-b-0 transition-colors"
//                                             >
//                                                 <div className="font-medium text-gray-800">
//                                                     {specialist.lastName} {specialist.firstName} {specialist.middleName}
//                                                 </div>
//                                                 <div className="text-sm text-gray-600 mt-1">
//                                                     Кабинет: {specialist.officeNumber}
//                                                 </div>
//                                                 {specialist.description && (
//                                                     <div className="text-sm text-gray-500 mt-2">
//                                                         {specialist.description}
//                                                     </div>
//                                                 )}
//                                             </button>
//                                         ))}
//                                     </div>
//                                 )}
//                             </div>
//
//                             {formData.specialist && (
//                                 <div className="flex justify-between mt-6">
//                                     <button
//                                         onClick={handleBack}
//                                         className="px-6 py-3 bg-gray-500 text-white rounded-xl hover:bg-gray-600 font-medium transition-colors"
//                                     >
//                                         ← Назад
//                                     </button>
//                                     <button
//                                         onClick={handleNext}
//                                         className="px-8 py-3 bg-blue-600 text-white rounded-xl hover:bg-blue-700 font-medium transition-colors shadow-md hover:shadow-lg"
//                                     >
//                                         Далее →
//                                     </button>
//                                 </div>
//                             )}
//                         </div>
//                     )}
//
//                     {/* Шаг 3: Выбор времени */}
//                     {currentStep === 3 && !loading && (
//                         <div className="space-y-4">
//                             <div className="flex items-center space-x-2 mb-4">
//                                 <button
//                                     onClick={handleBack}
//                                     className="p-2 text-gray-600 hover:text-gray-800 hover:bg-gray-100 rounded-lg transition-colors"
//                                 >
//                                     ← Назад
//                                 </button>
//                                 <h3 className="text-lg font-semibold text-gray-800">
//                                     Выберите время приема
//                                 </h3>
//                             </div>
//
//                             <div className="relative">
//                                 <button
//                                     onClick={() => toggleDropdown('scheduleSlot')}
//                                     className="w-full p-4 text-left bg-white border-2 border-gray-300 rounded-xl hover:border-blue-500 focus:border-blue-500 focus:ring-2 focus:ring-blue-200 transition-all duration-200 flex justify-between items-center"
//                                 >
//                                     <span className={formData.scheduleSlot ? "text-gray-800" : "text-gray-500"}>
//                                         {formData.scheduleSlot
//                                             ? `${new Date(formData.scheduleSlot.date).toLocaleDateString('ru-RU')} ${formData.scheduleSlot.startTime}`
//                                             : "Выберите время приема"}
//                                     </span>
//                                     <svg className={`w-5 h-5 text-gray-400 transform transition-transform ${showDropdown.scheduleSlot ? 'rotate-180' : ''}`}
//                                          fill="none" stroke="currentColor" viewBox="0 0 24 24">
//                                         <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
//                                     </svg>
//                                 </button>
//
//                                 {showDropdown.scheduleSlot && (
//                                     <div className="absolute z-10 w-full mt-2 bg-white border border-gray-200 rounded-xl shadow-lg max-h-60 overflow-y-auto">
//                                         {Object.entries(groupedSlots).map(([date, slots]) => (
//                                             <div key={date}>
//                                                 <div className="px-4 py-2 bg-gray-100 text-gray-700 font-medium border-b border-gray-200">
//                                                     {date}
//                                                 </div>
//                                                 {slots.map(slot => (
//                                                     <button
//                                                         key={slot.id}
//                                                         onClick={() => handleSlotSelect(slot)}
//                                                         className="w-full p-4 text-left hover:bg-blue-50 border-b border-gray-100 last:border-b-0 transition-colors"
//                                                     >
//                                                         <div className="font-medium text-gray-800">
//                                                             {slot.startTime} - {slot.endTime}
//                                                         </div>
//                                                         <div className="text-sm text-gray-600">
//                                                             Длительность: {slot.durationMinutes} мин.
//                                                         </div>
//                                                     </button>
//                                                 ))}
//                                             </div>
//                                         ))}
//                                         {availableSlots.length === 0 && (
//                                             <div className="p-4 text-center text-gray-500">
//                                                 Нет доступных слотов для записи
//                                             </div>
//                                         )}
//                                     </div>
//                                 )}
//                             </div>
//
//                             {formData.scheduleSlot && (
//                                 <div className="flex justify-between mt-6">
//                                     <button
//                                         onClick={handleBack}
//                                         className="px-6 py-3 bg-gray-500 text-white rounded-xl hover:bg-gray-600 font-medium transition-colors"
//                                     >
//                                         ← Назад
//                                     </button>
//                                     <button
//                                         onClick={handleNext}
//                                         className="px-8 py-3 bg-blue-600 text-white rounded-xl hover:bg-blue-700 font-medium transition-colors shadow-md hover:shadow-lg"
//                                     >
//                                         Далее →
//                                     </button>
//                                 </div>
//                             )}
//                         </div>
//                     )}
//
//                     {/* Шаг 4: Данные пациента */}
//                     {currentStep === 4 && !loading && (
//                         <div className="space-y-6">
//                             <div className="flex items-center space-x-2 mb-4">
//                                 <button
//                                     onClick={handleBack}
//                                     className="p-2 text-gray-600 hover:text-gray-800 hover:bg-gray-100 rounded-lg transition-colors"
//                                 >
//                                     ← Назад
//                                 </button>
//                                 <h3 className="text-lg font-semibold text-gray-800">Ваши данные</h3>
//                             </div>
//
//                             <form onSubmit={handleSubmit} className="space-y-6">
//                                 <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Фамилия *
//                                         </label>
//                                         <input
//                                             type="text"
//                                             value={formData.patient.lastName}
//                                             onChange={(e) => handlePatientDataChange('lastName', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                             required
//                                         />
//                                     </div>
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Имя *
//                                         </label>
//                                         <input
//                                             type="text"
//                                             value={formData.patient.firstName}
//                                             onChange={(e) => handlePatientDataChange('firstName', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                             required
//                                         />
//                                     </div>
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Отчество
//                                         </label>
//                                         <input
//                                             type="text"
//                                             value={formData.patient.middleName}
//                                             onChange={(e) => handlePatientDataChange('middleName', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                         />
//                                     </div>
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Телефон *
//                                         </label>
//                                         <input
//                                             type="tel"
//                                             value={formData.patient.phoneNumber}
//                                             onChange={(e) => handlePatientDataChange('phoneNumber', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                             required
//                                         />
//                                     </div>
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Email
//                                         </label>
//                                         <input
//                                             type="email"
//                                             value={formData.patient.email}
//                                             onChange={(e) => handlePatientDataChange('email', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                         />
//                                     </div>
//                                     <div>
//                                         <label className="block text-sm font-medium text-gray-700 mb-2">
//                                             Дата рождения *
//                                         </label>
//                                         <input
//                                             type="date"
//                                             value={formData.patient.dateOfBirth}
//                                             onChange={(e) => handlePatientDataChange('dateOfBirth', e.target.value)}
//                                             className="w-full p-3 border-2 border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-200 focus:border-blue-500 transition-all"
//                                             required
//                                         />
//                                     </div>
//                                 </div>
//
//                                 {/* Информация о выбранной записи */}
//                                 <div className="bg-blue-50 border border-blue-200 rounded-xl p-4">
//                                     <h4 className="font-semibold text-gray-800 mb-3 text-lg">Детали записи:</h4>
//                                     <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
//                                         <div>
//                                             <span className="text-gray-600">Специализация:</span>
//                                             <p className="font-medium">{formData.specialization?.name}</p>
//                                         </div>
//                                         <div>
//                                             <span className="text-gray-600">Врач:</span>
//                                             <p className="font-medium">
//                                                 {formData.specialist?.lastName} {formData.specialist?.firstName} {formData.specialist?.middleName}
//                                             </p>
//                                         </div>
//                                         <div>
//                                             <span className="text-gray-600">Дата:</span>
//                                             <p className="font-medium">
//                                                 {formData.scheduleSlot?.date && new Date(formData.scheduleSlot.date).toLocaleDateString('ru-RU')}
//                                             </p>
//                                         </div>
//                                         <div>
//                                             <span className="text-gray-600">Время:</span>
//                                             <p className="font-medium">
//                                                 {formData.scheduleSlot?.startTime} - {formData.scheduleSlot?.endTime}
//                                             </p>
//                                         </div>
//                                     </div>
//                                 </div>
//
//                                 <div className="flex justify-between pt-4">
//                                     <button
//                                         type="button"
//                                         onClick={handleBack}
//                                         className="px-6 py-3 bg-gray-500 text-white rounded-xl hover:bg-gray-600 font-medium transition-colors"
//                                     >
//                                         ← Назад
//                                     </button>
//                                     <button
//                                         type="submit"
//                                         disabled={loading}
//                                         className="px-8 py-3 bg-green-600 text-white rounded-xl hover:bg-green-700 font-medium transition-colors shadow-md hover:shadow-lg disabled:bg-gray-400 disabled:cursor-not-allowed"
//                                     >
//                                         {loading ? 'Загрузка...' : 'Записаться на прием'}
//                                     </button>
//                                 </div>
//                             </form>
//                         </div>
//                     )}
//                 </div>
//             </div>
//         </div>
//     );
// }