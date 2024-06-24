import React from 'react'
import { WashingMachine } from '../../models/WashingMachine'

type TableProps = {
    machines: WashingMachine[];
    onReservation: (machine: WashingMachine) => void;
}

const WashingMachinesTabel: React.FC<TableProps> = ({ machines, onReservation }) => {
    return (
        <div className='washing-machines-table'>
        {machines.map(machine => 
            <div key={machine._id} title={machine.reserved ? 'Already reserved' : ''} className={[machine.reserved ? 'not-available' : '', 'washing-machine'].join(" ")}>
                <button onClick={() => onReservation(machine)} />
            </div>)}
        </div>
    )
}

export default WashingMachinesTabel;