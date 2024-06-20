import React from 'react'
import { WashingMachine } from '../../models/WashingMachine';

type TableProps = {
    machines: WashingMachine[];
    onReservation: (machine: WashingMachine) => void;
}

const DiscountWashingMachinesTabel: React.FC<TableProps> = ({ machines, onReservation }) =>  {
  return (
    <div className='discount-machines-table'>
        <ul className='machines'>
            {machines.map(machine => machine.reserved ? null :
            <li key={machine._id} className='machine'>
                <div className='info'>
                    <span>{machine.date}</span>
                    <span>{machine.time}</span>
                </div>
                <div title='Reserve' className='actions'>
                    <button onClick={() => onReservation(machine)}/>
                </div>
            </li>
            )}
        </ul>
    </div>
  )
}

export default DiscountWashingMachinesTabel;
