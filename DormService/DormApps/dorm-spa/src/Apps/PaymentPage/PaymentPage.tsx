import React, { useState } from 'react'
import { useMount } from 'react-use'
import { StudentDebts } from './models/DebtsModel';
import PaymentService from './services/PaymentService';
  
export default function PaymentPage() {
  const [debtData, setDebtData] = useState<StudentDebts>();
  const [firstName, setFirstName] = useState<string>('Momcilo');
  const [lastName, setLastName] = useState<string>('Knezevic');
  const [username, setUsername] = useState<string>();
  const [usernameToDelete, setUsernameToDelete] = useState<string>('');
  const [selectedOption, setSelectedOption] = useState<string>('');
  const [amount, setAmount] = useState<number>(0);
  const [bankAccount1, setBankAccount1] = useState<string>('');
  const [bankAccount2, setBankAccount2] = useState<string>('');
  const [bankAccount3, setBankAccount3] = useState<string>('');
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  useMount(() => {
    const userIsAdmin = checkIfUserIsAdmin(); 
    setIsAdmin(userIsAdmin);

    if(!userIsAdmin)
    {
      PaymentService.getDebtsByUsername("Momcilo")
        .then(setDebtData);
    }
  });

  const handlePayClick = () => {
    const requestBody = {
      purposeOfPayment: selectedOption,
      amount: amount
    };

    const debtUpdate : StudentDebts = {
      studentID : "Momcilo",            // TODO : username from token
      credit: (requestBody.purposeOfPayment === "Credit") ? amount : 0,
      rent: (requestBody.purposeOfPayment === "Rent") ? amount : 0,
      internet: (requestBody.purposeOfPayment === "Internet") ? amount : 0,
      airConditioning: (requestBody.purposeOfPayment === "Air Conditioning") ? amount : 0,
      phone: (requestBody.purposeOfPayment === "Phone") ? amount : 0,
      cleaning: (requestBody.purposeOfPayment === "Cleaning") ? amount : 0,
    }
    
    console.log(debtUpdate);

    var successfulTransaction = PaymentService.updateStudentDebts(debtUpdate);
    console.log(successfulTransaction);
    //TODO pop up notification for success and failure

  };

  const handleDeleteClick = () => {
    const requestBody = {
      username : usernameToDelete
    };

    var successfulDeletion = PaymentService.deleteStudentByUsername(requestBody.username);
    console.log(successfulDeletion);
    //TODO pop up notification for success and failure
  }

  console.log(debtData);

  return (
    <div className='payment-page'>
      <div className='left-pannel'>
        <div className='title'>Payment slip</div>
        <div className='payment-slip'>
          {!isAdmin &&
          <div className="field" title='Enter your first name!'>
            <label htmlFor="firstName">First Name:</label>
            <input
              type="text"
              id="firstName"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            />
          </div>
          }
          {!isAdmin &&
          <div className="field" title='Enter your last name!'>
            <label htmlFor="lastName">Last Name:</label>
            <input
              type="text"
              id="lastName"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
          </div>
          }
          {isAdmin && 
          <div className="field" title='Enter students username!'>
            <label htmlFor="username">Username:</label>
            <input
              type="text"
              id="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </div>
          }
          {!isAdmin &&
          <div className="field" title='Enter your bank account!'>
            <label htmlFor="bankAccount">Bank account:</label>
            <input
              type="text"
              id="bankAccount1"
              value={bankAccount1}
              onChange={(e) => {
                const formattedAccount = e.target.value.replace(/\D/g, '').slice(0, 3);
                setBankAccount1(formattedAccount);
              }}
              maxLength={3}
            />
            <span>-</span>
            <input
              type="text"
              id="bankAccount2"
              value={bankAccount2}
              onChange={(e) => {
                let formattedAccount = e.target.value.replace(/\D/g, '');
                if (formattedAccount.length <= 13) {
                  setBankAccount2(formattedAccount);
                }
              }}
              maxLength={13}
            />
            <span>-</span>
            <input
              type="text"
              id="bankAccount3"
              value={bankAccount3}
              onChange={(e) => {
                const formattedAccount = e.target.value.replace(/\D/g, '').slice(0, 2);
                setBankAccount3(formattedAccount);
              }}
              maxLength={2}
            />
          </div>
          }
          <div className="field" title='Choose the purpose of your payment!'>
            <label htmlFor="paymentPurpose">Purpose of payment:</label>
            <select
              id="paymentPurpose"
              value={selectedOption}
              onChange={(e) => setSelectedOption(e.target.value)}
            >
              <option value="" disabled>Select debt type</option>
              <option value="Credit">Credit</option>
              <option value="Rent">Rent</option>
              <option value="Internet">Internet</option>
              <option value="Air conditioning">Air conditioning</option>
              <option value="Phone">Phone</option>
              <option value="Cleaning">Cleaning</option>
            </select>
          </div>
          <div className="field" title='Enter the amount you want to pay!'>
            <label htmlFor="amount">Amount:</label>
            <input
              type="number"
              id="amount"
              value={amount}
              onChange={(e) => setAmount(parseFloat(e.target.value))}
            />
            <span>rsd</span>
          </div>
          <button className='pay-button' title='Click here to make a transaction!' onClick={handlePayClick}>Pay</button>
        </div>
      </div>


      <div className='right-pannel'>
        
        {!isAdmin &&
        <div className='block-shadow'>
          <div className='title'>Debts</div>
          <div className='debts-report'>
            <div className='debt'>
              <label>Credit:</label> 
              <span>{debtData?.credit} rsd</span>
            </div>
            <div className='debt'>
              <label>Rent:</label> 
              <span>{debtData?.rent} rsd</span>
            </div>
            <div className='debt'>
              <label>Internet:</label> 
              <span>{debtData?.internet} rsd</span>
            </div>
            <div className='debt'>
              <label>Air conditioning:</label> 
              <span>{debtData?.airConditioning} rsd</span>
            </div>
            <div className='debt'>
              <label>Phone:</label> 
              <span>{debtData?.phone} rsd</span>
            </div>
            <div className='debt'>
              <label>Cleaning:</label> 
              <span>{debtData?.cleaning} rsd</span>
            </div>
          </div>
        </div>
        }
        
        {isAdmin &&
        <div className='admin-view'>
          <div className="field" title='Enter the username of the student you want to delete!'>
              <label htmlFor="username">Username:</label>
              <input
                type="text"
                id="usernameToDelete"
                value={usernameToDelete}
                onChange={(e) => setUsernameToDelete(e.target.value)}
              />
          </div>
          <button className='delete-button' title='Click here to delete student!' onClick={handleDeleteClick}>Delete</button>
        </div>
        }
      </div>

    </div>
  )
}

const checkIfUserIsAdmin = (): boolean => {
  // TODO with token
  return true; 
};