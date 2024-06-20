import React, { useState } from 'react';
import { Document } from './models/DocumentModel';

type DocumentFormProps = {
    document: Document | null;
    onUpload: (file: File) => void;
    onDownload: () => void;
    onDelete: () => void;
};

const DocumentForm: React.FC<DocumentFormProps> = ({ document, onUpload, onDownload, onDelete }) => {
    const [file, setFile] = useState<File | null>(null);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files.length > 0) {
            setFile(e.target.files[0]);
        }
    };

    const handleUpload = () => {
        if (file) {
            onUpload(file);
        }
    };

    return (
        <div>
            <h3>{document?.title}</h3>
            <input type="file" onChange={handleFileChange} />
            <button onClick={handleUpload} disabled={!file}>Upload</button>
            <button onClick={onDownload} disabled={!document?.content}>Download</button>
            <button onClick={onDelete} disabled={!document}>Delete</button>
        </div>
    );
};

export default DocumentForm;