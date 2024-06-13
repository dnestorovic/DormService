import React, { useState } from 'react'
import { useMount } from 'react-use'
import { StudentDebts } from './models/DebtsModel';
import PaymentService from './services/PaymentService';
  
export default function PaymentPage() {
  const [debtData, setDebtData] = useState<StudentDebts>();

  useMount(() => {
    PaymentService.getDebtsByStudentID("Momcilo")
    .then(setDebtData);
  });

  console.log(debtData);

  return (
    <div className='payment-page'>
      <div className='left-pannel'>
        Left 
      </div>
      <div className='right-pannel'>
        <div className='block-shadow'>
          <div className='title'>Debts</div>
          <div className='debts-report'>
            <div className='debt'>
              <label>Credit:</label> 
              <span>{debtData?.credit}</span>
            </div>
            <div className='debt'>
              <label>Rent:</label> 
              <span>{debtData?.rent || "Nema podataka"}</span>
            </div>
            <div className='debt'>
              <label>Internet:</label> 
              <span>{debtData?.internet}</span>
            </div>
            <div className='debt'>
              <label>Air conditioning:</label> 
              <span>{debtData?.airConditioning}</span>
            </div>
            <div className='debt'>
              <label>Phone:</label> 
              <span>{debtData?.phone}</span>
            </div>
            <div className='debt'>
              <label>Cleaning:</label> 
              <span>{debtData?.cleaning}</span>
            </div>
          </div>
        </div>
      </div>

    </div>
  )
}