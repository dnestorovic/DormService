import React, { useState } from 'react'
import { ModalDialog } from '../../../../components/Modals/ModalDialog'
import DropdownMenu from '../DropdownMenu/DropdownMenu'
import { WashingMachine } from '../../models/WashingMachine'
import { useMount } from 'react-use'

type Props = {
    machine: WashingMachine;
    onCancel: () => void;
    onSubmit: (machine: WashingMachine) => void;
}

const WashingMachineReservationModal: React.FC<Props> = ({ machine, onCancel, onSubmit }) => {
    const [reservation, setReservation] = useState<WashingMachine>({...machine, spinRate: undefined, washingTemperature: undefined});

    const updateSpinRate = (sr: string) => {
        setReservation({
            ...reservation, 
            spinRate: +sr
        });
    }

    const updateTemperature = (t: string) => {
        setReservation({
            ...reservation, 
            washingTemperature: +t
        });
    }

    const handleReservation = () => {
        if (reservation.spinRate === undefined || reservation.washingTemperature === undefined) {
            alert("All washing parameters must be selected");
            return;
        }

        onSubmit(reservation)
    }

    return (
        <ModalDialog header='Reserve washing machine' submitText='Confirm' onCancel={onCancel} onSubmit={handleReservation}
             rootClass='reservation-dialog'>
            <p>Before reservation, please select washing parameters... </p>
            <div className='parameters'>
                <DropdownMenu title={reservation?.washingTemperature?.toString() || 'Select temperature'} options={['30', '40', '60', '90']} onSelect={updateTemperature} />
                <DropdownMenu title={reservation?.spinRate?.toString() || 'Select spin rate'} options={['1400', '1200', '1000', '800', '600']} onSelect={updateSpinRate} />    
            </div>
        </ModalDialog>
    )
}

export default WashingMachineReservationModal;