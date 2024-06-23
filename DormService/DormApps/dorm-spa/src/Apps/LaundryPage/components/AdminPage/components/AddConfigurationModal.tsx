import React, { useState } from 'react'
import { ModalDialog } from '../../../../../components/Modals/ModalDialog'
import { WashingMachineConfigurationRequest } from '../../../models/WashingMachine';

type Props = {
    onCancel: () => void;
    onSubmit: (machine: WashingMachineConfigurationRequest) => void;
}

const AddConfigurationModal: React.FC<Props> = ({ onCancel, onSubmit }) => {

    const [request, setRequest] = useState<WashingMachineConfigurationRequest>({});

    const handleInput = (value: string, prop: keyof WashingMachineConfigurationRequest) => {
        setRequest((prev) => ({ ...prev, [prop]: value }));
    };

    const handleSubmit = () => {
        if (request.expirationDate === undefined || request.startDate === undefined || request.responsiblePersonEmail === undefined || request.manufacturer === undefined) {
            alert("All fields must be populated");
            return;
        }

        if (!request.responsiblePersonEmail.includes("@") 
            || request.responsiblePersonEmail.indexOf("@") < 1 
            || request.responsiblePersonEmail.indexOf("@") > request.responsiblePersonEmail.length - 2) {
            alert("Invalid email address");
            return;           
        }

        onSubmit(request);
    }

  return (
    <ModalDialog header='Add new washing machine' onCancel={onCancel} onSubmit={handleSubmit}>
        <div className='add-configuration-form'>
            <div className="form-field">
                <label>Manufacturer:</label>
                <input
                    value={request.manufacturer}
                    onChange={(event) => handleInput(event.currentTarget.value, 'manufacturer')}
                />
            </div>
            <div className="form-field">
                <label>Start date:</label>
                <input
                    value={request.startDate}
                    onChange={(event) => handleInput(event.currentTarget.value, 'startDate')}
                />
            </div>
            <div className="form-field">
                <label>Expiration date:</label>
                <input
                    value={request.expirationDate}
                    onChange={(event) => handleInput(event.currentTarget.value, 'expirationDate')}
                />
            </div>
            <div className="form-field">
                <label>Responsible person email:</label>
                <input
                    value={request.responsiblePersonEmail}
                    onChange={(event) => handleInput(event.currentTarget.value, 'responsiblePersonEmail')}
                />
            </div>
        </div>
    </ModalDialog>
  )
}

export default AddConfigurationModal;