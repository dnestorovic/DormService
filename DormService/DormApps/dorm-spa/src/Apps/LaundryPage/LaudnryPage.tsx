import React, { useState } from 'react'
import { ModalDialog } from '../../components/Modals/ModalDialog';
import DropdownMenu from './components/DropdownMenu/DropdownMenu';
import { useMount } from 'react-use';
import { Timeframes, WashingMachine } from './models/WashingMachine';
import UserReservationsService from './services/UserReservationsService';

export default function LaudnryPage() {

  const [showModal, setShowModal] = useState<boolean>(false);
  const [showNotification, setShowNotification] = useState<{type?: string, message?: string}>({});


  const [selectedDate, setSelectedDate] = useState<string>();
  const [selectedTime, setSelectedTime] = useState<string>();
  const [allDates, setAllDates] = useState<string[]>([]);

  const [availabelMachines, setAvailabelMachines] = useState<WashingMachine[]>([]);
  

  const selectDate = (date: string) => {
    setSelectedDate(prev => date);
    UserReservationsService.getWashingMachinesByDate(date)
      .then(machines => setAvailabelMachines([...machines]))
      .catch(() => setShowNotification({type: "error", message: "Machines for the given date not availabel"}))
  }

  const selectPeriod = (time: string) => {
      setSelectedTime(prev => time);
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
                {selectedDate && selectedTime ? availabelMachines.map(m => <span>{m._id}</span>) : null}
            </div>
        </div>
      </div>
      <div className='right-panel panel'>B</div>
    </div>
  )
}
