import React from 'react'
import { WashingMachine } from '../../models/WashingMachine';

type props = {
    machines: WashingMachine[];
}

const AlreadyReservedMachinesCard: React.FC<props> = ({ machines }) =>  {
  return (
    <div className='student-reserved-machines-card'>
        <div className='card-shadow'>
            <h1>Hey there...</h1>
            {machines.length !== 0 ? 
            <div>
            <p>You have already reserved washing machines at:</p>
            <ul>
                {machines.sort((a, b) => a.date > b.date ? 1 : -1).map(machine => 
                <li>
                    <div className='icon' />
                    <div className='info'>
                        <span>{machine.date}</span><span>at {machine.time}</span>
                    </div>
                </li>)}
            </ul>
            </div> : 
            <p>You don't have reserved washing machines for the next 7 days</p>}    
        </div>
    </div>
  )
}

export default AlreadyReservedMachinesCard;