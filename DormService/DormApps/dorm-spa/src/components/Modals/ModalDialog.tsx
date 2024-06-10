import React, { ReactNode } from 'react';

type ModalDialogProps = {
    header?: string;
    rootClass?: string;
    submitText?: string;
    cancelText?: string;
    onSubmit?: () => void;
    onCancel?: () => void;
    children?: ReactNode
    readonly?: boolean;
};

export const ModalDialog: React.FC<ModalDialogProps> = ({
    header,
    rootClass,
    submitText,
    cancelText,
    onSubmit,
    onCancel,
    children,
    readonly = false
}) => {
    return (
        <div className="modal-shadow">
            <div className='modal-background'>
                <div className={[rootClass, 'modal-dialog'].join(' ')}>
                    <div className="header">{header || ''}</div>
                    <div className="main">{children}</div>
                    <div className="footer">
                        <button onClick={onCancel}>
                            <span>{cancelText || 'Cancel'}</span>
                        </button>
                        {!readonly && (
                            <button onClick={onSubmit} className="primary">
                                <span>{submitText || 'Submit'}</span>
                            </button>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};