import React, { useState } from 'react'
import { ModalDialog } from '../../components/Modals/ModalDialog';

export default function LaudnryPage() {

  const [showModal, setShowModal] = useState<boolean>(false);

  const handleClick = () => {
      setShowModal(prev => true);
  }

  const onSubmitClick = () => {
      alert("Clicked on submit");
  }

  const onCancelClick = () => {
      alert("Clicked on cancel");
      setShowModal(prev => false);
  }


  return (
    <>
      <button onClick={handleClick}>LaudnryPage</button>
      <button onClick={() => alert("Test Click!")}>Test click</button>
      {showModal && <ModalDialog header='Test dynamic header' submitText='Custom submit' cancelText='Custom cancel' onCancel={onCancelClick} onSubmit={onSubmitClick}>
        <p>This is an example of a modal dialog with configurable header, content and actions</p>  
      </ModalDialog>}
    </>
  )
}
