import React, { useState } from 'react'
import { WashingMachine } from '../../models/WashingMachine'
import { useMount } from 'react-use';

type TableProps = {
    machines: WashingMachine[];
    onReservation: (machine: WashingMachine) => void;
}

const WashingMachinesTabel: React.FC<TableProps> = ({ machines, onReservation }) => {
    const [showMachines, setShowMachines] = useState<boolean>(false);


    useMount(() => {
        machines.forEach(machine => {
            if (!machine.reserved) {
                setShowMachines(prev => true);
            }
        });
    });

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