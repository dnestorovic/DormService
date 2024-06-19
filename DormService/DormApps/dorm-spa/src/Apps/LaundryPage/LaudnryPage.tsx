import React, { useEffect, useState } from 'react'
import DropdownMenu from './components/DropdownMenu/DropdownMenu';
import { useMount } from 'react-use';
import { Timeframes, WashingMachine } from './models/WashingMachine';
import UserReservationsService from './services/UserReservationsService';
import WashingMachinesTabel from './components/WashingMachinesTable/WashingMachinesTable';
import WashingMachineReservationModal from './components/WashingMachineReservationModal/WashingMachineReservationModal';
import { Notification, NotificationType } from '../../components/Notifications/Notification';
import DiscountWashingMachinesTabel from './components/DiscountWashingMachinesTable/DiscountWashingMachinesTable';
import AlreadyReservedMachinesCard from './components/AlreadyReservedMachinesCard/AlreadyReservedMachinesCard';

export default function LaudnryPage() {

  const [reservedMachine, setReservedMachine] = useState<WashingMachine>();
  const [getDiscountMachines, setGetDiscountMachines] = useState<WashingMachine[]>([]);
  const [showNotification, setShowNotification] = useState<{type: NotificationType, message: string}>();

  const [selectedDate, setSelectedDate] = useState<string>();
  const [selectedTime, setSelectedTime] = useState<string>();
  const [allDates, setAllDates] = useState<string[]>([]);

  const [availabelMachines, setAvailabelMachines] = useState<WashingMachine[]>([]);
  const [alreadyReservedMachines, setAlreadyReservedMachines] = useState<WashingMachine[]>([]); 
  
  const getStudentId = () => {
    return localStorage.getItem("username") || "";
  }

  const selectDate = (date: string) => {
    setSelectedDate(prev => date);
    getWashingMachines(date);
  }

  const selectPeriod = (time: string) => {
      setSelectedTime(prev => time);
  }

  const getWashingMachines = (date: string) => {
    UserReservationsService.getWashingMachinesByDate(date.replaceAll("/", "."))
      .then(machines => setAvailabelMachines([...machines]))
      .catch(() => setShowNotification({type: NotificationType.Error, message: "Machines for the given date not availabel"}))
  }

  const getReservedMachinesForStudent = () => {
    UserReservationsService.getWashingMachinesByStudentId(getStudentId())
      .then((machines) => setAlreadyReservedMachines(machines));
  }

  const reserveMachine = (machine: WashingMachine) => {
      setReservedMachine(prev => machine);
  }

  const handleReservation = (reservation: WashingMachine) => {
      setReservedMachine(undefined);
      UserReservationsService.reserveWashingMachine(reservation, getStudentId())
        .then(() => {
          setShowNotification({type: NotificationType.Success, message: "Washing machine successfully reserved"})
          selectedDate && getWashingMachines(selectedDate);
          getReservedMachinesForStudent();
          setGetDiscountMachines([]);
        })
        .catch(() => setShowNotification({type:  NotificationType.Error, message: "Washing machine cannot be reserved"}));
  }

  const handleGetDiscount = () => {
    setGetDiscountMachines([]); // clear any previous data
      UserReservationsService.getMachineWithDiscount()
        .then((id) => {
            allDates.forEach(date => {
                UserReservationsService.getWashingMachinesByDate(date.replaceAll("/", "."))
                  .then((machines) => {
                      setGetDiscountMachines(prev => [...prev, ...(machines.filter(m => m.configurationId === id))]);
                  })
            });            
        })
        .catch(() => setShowNotification({type:  NotificationType.Error, message: "There are no discounted washing machines at the moment."}));


      setSelectedDate(undefined);
      setSelectedTime(undefined);
  }

  useEffect(() => {
    if (selectedDate && selectedTime) {
      setGetDiscountMachines([]);
    }
  }, [selectedDate, selectedTime]);

  useMount(() => {
    var availableDates: string[] = [];
    for (let index = 0; index < 7; index++) {
      var nextDay = new Date(Date.now() + index * 24 * 60 * 60 * 1000);
      availableDates = [...availableDates, nextDay.toLocaleDateString('en-UK')]
    }

    setAllDates(prev => [...availableDates])
    getReservedMachinesForStudent();    
  });

  return (
    <div className='laundry-page'>
      <div className='left-panel panel'>
        <h1>Reservations</h1>
        <div className='reservation-form'>
            <div className='selector'>
              <button className='discount-button' onClick={handleGetDiscount}><span>Reserve with discount</span></button>
              <DropdownMenu title={selectedDate || "Select date"} options={allDates} onSelect={selectDate}/>
              <DropdownMenu title={selectedTime || "Select time"} options={Object.values(Timeframes)} onSelect={selectPeriod}/>
            </div>
            <div className='machines'>
                {selectedDate && selectedTime ? <WashingMachinesTabel machines={availabelMachines.filter(m => m.time === selectedTime)} onReservation={reserveMachine} /> : null}
            </div>
            <div>
                {getDiscountMachines ? <DiscountWashingMachinesTabel machines={getDiscountMachines} onReservation={reserveMachine}/> : null}
            </div>
        </div>
      </div>
      {!reservedMachine ? null : <WashingMachineReservationModal machine={reservedMachine} 
        onSubmit={handleReservation}
        onCancel={() => setReservedMachine(undefined)}
      />}
      {!showNotification ? null : <Notification type={showNotification.type} text={showNotification.message || ''} onRemove={() => setShowNotification(undefined)} />}
      <div className='right-panel panel'>
          <AlreadyReservedMachinesCard machines={alreadyReservedMachines} />
      </div>
    </div>
  )
}
