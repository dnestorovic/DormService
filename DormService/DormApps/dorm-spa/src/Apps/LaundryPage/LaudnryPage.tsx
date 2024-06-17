import React, { useState } from 'react'
import { ModalDialog } from '../../components/Modals/ModalDialog';
import DropdownMenu from './components/DropdownMenu/DropdownMenu';
import { useMount } from 'react-use';
import { Timeframes, WashingMachine } from './models/WashingMachine';
import UserReservationsService from './services/UserReservationsService';
import WashingMachinesTabel from './components/WashingMachinesTabel/WashingMachinesTabel';
import WashingMachineReservationModal from './components/WashingMachineReservationModal/WashingMachineReservationModal';
import { Notification, NotificationType } from '../../components/Notifications/Notification';

export default function LaudnryPage() {

  const [reservedMachine, setReservedMachine] = useState<WashingMachine>();
  const [showNotification, setShowNotification] = useState<{type: NotificationType, message: string}>();


  const [selectedDate, setSelectedDate] = useState<string>();
  const [selectedTime, setSelectedTime] = useState<string>();
  const [allDates, setAllDates] = useState<string[]>([]);

  const [availabelMachines, setAvailabelMachines] = useState<WashingMachine[]>([]);
  

  const getWashingMachines = (date: string) => {
    console.log("Called");
    UserReservationsService.getWashingMachinesByDate(date.replaceAll("/", "."))
      .then(machines => setAvailabelMachines([...machines]))
      .catch(() => setShowNotification({type: NotificationType.Error, message: "Machines for the given date not availabel"}))
  }

  const selectDate = (date: string) => {
    setSelectedDate(prev => date);
    getWashingMachines(date);
  }

  const selectPeriod = (time: string) => {
      setSelectedTime(prev => time);
  }

  const reserveMachine = (machine: WashingMachine) => {
      setReservedMachine(prev => machine);
  }

  const handleReservation = (reservation: WashingMachine) => {
      setReservedMachine(undefined);
      UserReservationsService.reserveWashingMachine(reservation)
      .then(() => {
        setShowNotification({type: NotificationType.Success, message: "Washing machine successfully reserved"})
        selectedDate && getWashingMachines(selectedDate);
      })
      .catch(() => setShowNotification({type:  NotificationType.Error, message: "Washing machine cannot be reserved"}));
  }

  useMount(() => {
    var availableDates: string[] = [];
    for (let index = 0; index < 7; index++) {
      var nextDay = new Date(Date.now() + index * 24 * 60 * 60 * 1000);
      availableDates = [...availableDates, nextDay.toLocaleDateString('en-UK')]
    }

    setAllDates(prev => [...availableDates])
  });

  return (
    <div className='laundry-page'>
      <div className='left-panel panel'>
        <h1>Reservations</h1>
        <div className='reservation-form'>
            <div className='selector'>
              <DropdownMenu title={selectedDate || "Select date"} options={allDates} onSelect={selectDate}/>
              <DropdownMenu title={selectedTime || "Select time"} options={Object.values(Timeframes)} onSelect={selectPeriod}/>
            </div>
            <div className='machines'>
                {selectedDate && selectedTime ? <WashingMachinesTabel machines={availabelMachines.filter(m => m.time === selectedTime)} onReservation={reserveMachine} /> : null}
            </div>
        </div>
      </div>
      <div className='right-panel panel'>B</div>
      {!reservedMachine ? null : <WashingMachineReservationModal machine={reservedMachine} 
        onSubmit={handleReservation}
        onCancel={() => setReservedMachine(undefined)}
      />}
      {!showNotification ? null : <Notification type={showNotification.type} text={showNotification.message || ''} onRemove={() => setShowNotification(undefined)} />}
    </div>
  )
}
