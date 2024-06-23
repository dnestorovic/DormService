import React, { useState } from 'react'
import { useMount } from 'react-use'
import { DefaultStudentDebts, StudentDebts } from './models/DebtsModel';
import PaymentService from './services/PaymentService';
import { Notification, NotificationType } from '../../components/Notifications/Notification';
import { ModalDialog } from '../../components/Modals/ModalDialog';
import { getRole } from '../../Utils/TokenUtil';
import { useNavigate } from 'react-router-dom';
  
export default function PaymentPage() {
  
  const [debtData, setDebtData] = useState<StudentDebts>();
  const [debtUpdate, setDebtUpdate] = useState<StudentDebts>(DefaultStudentDebts);

  // Username value that only admin use for delete and create
  const [usernameToModify, setUsernameToModify] = useState<string>('');
  // Username value that is used in payment slip
  const [usernameToPay, setUsernameToPay] = useState<string>('');
  
  const [selectedOption, setSelectedOption] = useState<string>('');
  const [amount, setAmount] = useState<number>(0);
  const [bankAccount1, setBankAccount1] = useState<string>('');
  const [bankAccount2, setBankAccount2] = useState<string>('');
  const [bankAccount3, setBankAccount3] = useState<string>('');
  
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  const [showNotification, setShowNotification] = useState<{type: NotificationType, message: string}>();
  const [showConfiramtionDialog, setShowConfiramtionDialog] = useState<boolean>(false);

  const firstName = localStorage.getItem("first-name") ?? "";
  const lastName = localStorage.getItem("last-name") ?? "";
  const username = localStorage.getItem("username") ?? "";
  const email = localStorage.getItem("email") ?? "";

  const navigate = useNavigate();
  useMount(() => {

    // Payment page is visible only for logged in users
    if (localStorage.getItem("username") === null) {
      navigate('/login');
    }

    const userIsAdmin = checkIfUserIsAdmin(); 
    setIsAdmin(userIsAdmin);

    // Student can see his debts
    if(!userIsAdmin)
    {
      fetchStudentDebts();
    }

  });

  const fetchStudentDebts = () => {

    PaymentService.getDebtsByUsername(username)
      .then(setDebtData);
  
  }

  const onPay = () => {

    PaymentService.updateStudentDebts(debtUpdate, email)
    .then(() => {
      setShowNotification({type: NotificationType.Success, message: "Transaction completed successfully!"});
      fetchStudentDebts();
    })  // 200 OK
    .catch(() => setShowNotification({type: NotificationType.Error, message: "Transaction failed!"})); // 404 Not Found
  
  }

  const handlePayClick = () => {

    // Mandatory fields in payment slip
    if(selectedOption === '' || amount === 0 || 
      (isAdmin && username === '') || 
      (!isAdmin && (bankAccount1 === '' || bankAccount2 === '' || bankAccount3 === '' || firstName === '' || lastName === ''))
    ){
      alert("All fields must be filled!");
      return;
    }
    
    const requestBody = {
      purposeOfPayment: selectedOption,
      amount: amount
    };

    const currentDebt : StudentDebts = {
      studentID : (isAdmin === true) ? usernameToPay : username,
      credit: (requestBody.purposeOfPayment === "Credit") ? amount : 0,
      rent: (requestBody.purposeOfPayment === "Rent") ? amount : 0,
      internet: (requestBody.purposeOfPayment === "Internet") ? amount : 0,
      airConditioning: (requestBody.purposeOfPayment === "Air Conditioning") ? amount : 0,
      phone: (requestBody.purposeOfPayment === "Phone") ? amount : 0,
      cleaning: (requestBody.purposeOfPayment === "Cleaning") ? amount : 0,
    }
    
    setDebtUpdate(currentDebt);
    setShowConfiramtionDialog(true);

  };


  const handleDeleteClick = () => {

    // Mandatory field
    if(isAdmin && username === '')
    {
      alert("Username field must be filled!");
      return;
    }

    const requestBody = {
      usernameToDelete : usernameToModify
    };

    PaymentService.deleteStudentByUsername(requestBody.usernameToDelete)
      .then(() => setShowNotification({type: NotificationType.Success, message: "Student successfully deleted!"}))  // 200 OK
      .catch(() => setShowNotification({type: NotificationType.Error, message: "The specified username does not exist!"})); // 404 Not Found
  
  }


  const handleCreateClick = () => {

    // Mandatory field
    if(isAdmin && username === '')
    {
        alert("Username field must be filled!");
        return;
    }

    const requestBody = {
      usernameToCreate : usernameToModify
    };

    // Student with default debts
    const studentDebts : StudentDebts = {
      studentID : requestBody.usernameToCreate,
      credit: 0,
      rent: 0,
      internet: 0,
      airConditioning: 0,
      phone: 0,
      cleaning: 0,
    }

    PaymentService.createDefaultStudent(studentDebts)
      .then(() => setShowNotification({type: NotificationType.Success, message: "Student successfully created!"}))  // 201 Created
      .catch(() => setShowNotification({type: NotificationType.Error, message: "The specified username already exists!"})); // 500 Username exists
  }


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
              readOnly
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
              readOnly
            />
          </div>
          }
          {isAdmin && 
          <div className="field" title='Enter students username!'>
            <label htmlFor="usernameToPay">Username:</label>
            <input
              type="text"
              id="usernameToPay"
              value={usernameToPay}
              onChange={(e) => setUsernameToPay(e.target.value)}
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
              <option value="" disabled>Select payment purpose</option>
              <option value="Credit">Credit</option>
              <option value="Rent">Rent</option>
              <option value="Internet">Internet</option>
              <option value="Air Conditioning">Air conditioning</option>
              <option value="Phone">Phone</option>
              <option value="Cleaning">Cleaning</option>
            </select>
          </div>
          <div className="field" title='Enter the amount you want to pay!'>
            <label htmlFor="amount">Amount:</label>
            <input
              type="number"
              id="amount"
              min="0"
              value={amount}
              onChange={(e) => setAmount(parseFloat(e.target.value))}
            />
            <span>rsd</span>
          </div>
          <button className='pay-button' title='Click here to make a transaction!' onClick={handlePayClick}>Pay</button>
          {!showNotification ? null : <Notification type={showNotification.type} text={showNotification.message || ''} onRemove={() => setShowNotification(undefined)} />}
          {!showConfiramtionDialog ? null : <ModalDialog header='Are you sure?' submitText='Yes' onSubmit={() => {onPay();  setShowConfiramtionDialog(false);}} onCancel={() => setShowConfiramtionDialog(false)}>Are you sure you want to continue with this payment?</ModalDialog> }
        </div>
      </div>

      <div className='right-pannel'>
        {!isAdmin &&
        <div className='block-shadow'>
          <div className='title'>Credit and Debts</div>
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
                value={usernameToModify}
                onChange={(e) => setUsernameToModify(e.target.value)}
              />
          </div>
          <button className='admin-button' title='Click here to create student!' onClick={handleCreateClick}>Create</button>
          <button className='admin-button' title='Click here to delete student!' onClick={handleDeleteClick}>Delete</button>
        </div>
        }
      </div>
    </div>
  )
}

const checkIfUserIsAdmin = (): boolean => {
  const role = getRole();
  return role === "Administrator"; 
};