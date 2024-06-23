import React, { useState } from 'react'
import { WashingMachineConfigurationRequest, WashingMachineConfigurations } from '../../models/WashingMachine'
import { useMount } from 'react-use';
import MachineManagementService from '../../services/MachineManagementService';
import { Notification, NotificationType } from '../../../../components/Notifications/Notification';
import AddConfigurationModal from './components/AddConfigurationModal';
import { ModalDialog } from '../../../../components/Modals/ModalDialog';

export default function AdminLaundryPage() {

  const [currentMachines, setCurrentMachines] = useState<WashingMachineConfigurations[]>([]);
  const [showNotification, setShowNotification] = useState<{type: NotificationType, message: string}>();
  const [showAddForm, setShowAddForm] = useState<boolean>(false);
  const [showDeleteDialog, setShowDeleteDialog] = useState<string>("");


  const fetchConfigurations = () => {
    MachineManagementService.getWashingMachinesConfigurations()
      .then(setCurrentMachines)
      .catch(() => setShowNotification({type: NotificationType.Error, message: "Machines configurations not available"}))
  }

  const handleAdd = () => {
    setShowAddForm(true);
  }

  const handleDeleteConfig = () => {
    MachineManagementService.deleteWashingMachineConfiguration(showDeleteDialog)
    .then(() => {
      setShowNotification({type: NotificationType.Success, message: "Configuration deleted successfully"});
      fetchConfigurations();
      setShowDeleteDialog("");
    })
    .catch(() => setShowNotification({type: NotificationType.Error, message: "Cannot add new configuration"}));
  }

  const handleAddNewConfig = (request: WashingMachineConfigurationRequest) => {
    setShowAddForm(false);
    MachineManagementService.addWashingMachineConfiguration(request)
      .then(() => {
        setShowNotification({type: NotificationType.Success, message: "New configuration added successfully"})
        fetchConfigurations();
      })
      .catch(() => setShowNotification({type: NotificationType.Error, message: "Cannot add new configuration"}));
  }

  useMount(() => {
    fetchConfigurations();
  });

  const card = (config: WashingMachineConfigurations) => {
      return <div className='card' key={config._id}>
        <div className='info'>
            <span>Manufacturer: {config.manufacturer}</span>
            <span>Start date: {config.startDate}</span>
            <span>Expiration date: {config.expirationDate}</span>
            <span>Repairman: {config.responsiblePersonEmail}</span>
        </div>
        <div className='actions'>
          <span title='Utilization factor'>{config.utilizationFactor}</span>
          <button onClick={() => setShowDeleteDialog(config._id)}>Delete</button>
        </div>
      </div>
  }

  return (
    <>
    <div className='admin-laundry-page'>
      <div className='admin-laundry-content'>
          <h1>Machine Management</h1>
          <div className='management-machines'>
              <div className='configurations'>{currentMachines.map(card)}<button onClick={handleAdd}>Add new</button></div>
          </div>
      </div>
    </div>
    {!showNotification ? null : <Notification type={showNotification.type} text={showNotification.message || ''} onRemove={() => setShowNotification(undefined)} /> }
    {!showAddForm ? null : <AddConfigurationModal onCancel={() => setShowAddForm(false)} onSubmit={handleAddNewConfig} />}
    {showDeleteDialog === "" ? null : 
      <ModalDialog header='Are you sure?' onCancel={() => setShowDeleteDialog("")} onSubmit={handleDeleteConfig}>
        <p>This change is permanent. After confirmation, you won't be able to restore deleted machine</p>
      </ModalDialog>}
    </>
  )
}
