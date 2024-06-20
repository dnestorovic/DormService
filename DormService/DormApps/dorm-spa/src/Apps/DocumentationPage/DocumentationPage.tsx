import React, { useEffect, useState } from 'react'
import BaseService from '../../services/BaseService';
import { useMount } from 'react-use';
import { DocumentationList } from './models/DocumentationListModel';
import DocumentationService from './services/DocumentationService';
import DocumentForm from './DocumentForm';


export default function DocumentationPage() {
  const [firstName, setFirstName] = useState<string>('Teodora');
  const [lastName, setLastName] = useState<string>('Vasic');
  const [username, setUsername] = useState<string>('teodoravasic');
  const [email, setEmail] = useState<string>('tekisooj@gmail.com');
  const [isAdmin, setIsAdmin] = useState<boolean>(false);
  const [documents, setDocuments] = useState<DocumentationList | null>(null);
  const documentationService = DocumentationService;

  useEffect(() => {
    const fetchData = async () => {
        try {
            const data = await documentationService.fetchDocumentationList(username);
            setDocuments(data);
        } catch (error) {
            console.error('Error fetching documentation list:', error);
        }
    };

    fetchData();
  }, [documentationService]);

  const handleUpload = (key: keyof DocumentationList, file: File) => {
      // Handle the upload logic here
      console.log(`Uploading ${key}`, file);
  };

  const handleDownload = (key: keyof DocumentationList) => {
      // Handle the download logic here
      console.log(`Downloading ${key}`);
  };

  const handleDelete = (key: keyof DocumentationList) => {
      // Handle the delete logic here
      console.log(`Deleting ${key}`);
  };

  return (
      <div>
          {documents ? (
              <div>
                  {Object.keys(documents).map((key) => (
                      <DocumentForm
                          key={key}
                          document={documents[key as keyof DocumentationList]}
                          onUpload={(file) => handleUpload(key as keyof DocumentationList, file)}
                          onDownload={() => handleDownload(key as keyof DocumentationList)}
                          onDelete={() => handleDelete(key as keyof DocumentationList)}
                      />
                  ))}
              </div>
          ) : (
              <p>Loading...</p>
          )}
      </div>
  );
};
