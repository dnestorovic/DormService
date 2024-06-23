import React, { useState, useEffect } from 'react';
import { Document } from './models/DocumentModel';

type DocumentFormProps = {
    id: string;
    document: Document | null;
    onUpload: (file: File) => void;
    onDownload: () => void;
    onDelete: () => void;
};

const DocumentForm: React.FC<DocumentFormProps> = ({ id, document, onUpload, onDownload, onDelete }) => {
    const [file, setFile] = useState<File | null>(null);
    const [isDocumentAvailable, setIsDocumentAvailable] = useState<boolean>(!!document);
    const [fileName, setFileName] = useState<string>('');
    
    const wordsArray = id.split(/(?=[A-Z])/).map(s => s.toUpperCase()).join(" ").replace("AVG", "AVERAGE");

    useEffect(() => {
        setIsDocumentAvailable(!!document);
    }, [document]);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files.length > 0) {
            const selectedFile = e.target.files[0];
            setFile(selectedFile);
            setFileName(selectedFile.name); // Set the file name for display in the button
        } else {
            setFile(null);
            setFileName('');
        }
    };

    const handleUpload = () => {
        if (file) {
            onUpload(file);
            setIsDocumentAvailable(true);
            setFile(null);
            setFileName('');
        }
    };

    const handleDelete = () => {
        onDelete();
        setIsDocumentAvailable(false);
        setFile(null);
        setFileName('');
    };

    return (
        <div className="document-form">
            <h3>{wordsArray}</h3>
            {!isDocumentAvailable && (
                <>
                    <label className="custom-file-upload" htmlFor="inputFile">
                        <input className="input-file" id="inputFile" type="file" onChange={handleFileChange}  />
                    </label>
                    {file && (
                        <button className="document-button" onClick={handleUpload}>
                            Upload
                            
                        </button>
                    )}
                </>
            )}
            {isDocumentAvailable && (
                <>
                    <button className="document-button" onClick={onDownload} disabled={!document?.content}>
                        Download
                    </button>
                    <button className="document-button" onClick={handleDelete} disabled={!document}>
                        Delete
                    </button>
                </>
            )}
        </div>
    );
};

export default DocumentForm;
