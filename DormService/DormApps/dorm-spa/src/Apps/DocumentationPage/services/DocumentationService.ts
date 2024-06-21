import BaseService from "../../../services/BaseService";
import { DocumentationList } from "../models/DocumentationListModel";
import { Document } from "../models/DocumentModel";

interface IDocumentationService {
    fetchDocumentationList: (username: string) => Promise<DocumentationList>;
    uploadFile: (username: string, file: File, fieldName: string) => Promise<any>;
    downloadFile: (username: string, fileName: string) => Promise<Blob>;
    deleteFile: (username: string, fileName: string) => Promise<any>;
    getMissingDocumentationList: (username: string) => Promise<any>;
}

const transformDocument = (doc: any): Document | null => {
    if (!doc) return null;
    return {
        title: doc.title,
        content: doc.content ? new File([doc.content], doc.title) : null
    };
};

const DocumentationService: () => IDocumentationService = () => {

    const fetchDocumentationList = async (username: string): Promise<DocumentationList> => {
        const response = await fetch(`http://localhost:8005/api/v1/DocumentationList/${username}`);
        const data = await response.json();

        const transformedData: DocumentationList = {
            applicationForm: transformDocument(data.applicationForm),
            avgGradeCertificate: transformDocument(data.avgGradeCertificate),
            incomeCertificate: transformDocument(data.incomeCertificate),
            unemploymentCertificate: transformDocument(data.unemploymentCertificate),
            facultyDataForm: transformDocument(data.facultyDataForm),
            firstTimeStudentCertificate: transformDocument(data.firstTimeStudentCertificate),
            highSchoolFirstYearCertificate: transformDocument(data.highSchoolFirstYearCertificate),
            highSchoolSecondYearCertificate: transformDocument(data.highSchoolSecondYearCertificate),
            highSchoolThirdYearCertificate: transformDocument(data.highSchoolThirdYearCertificate),
            highSchoolFourthYearCertificate: transformDocument(data.highSchoolFourthYearCertificate)
        };

        return transformedData;
    };

    const uploadFile = (username: string, file: File, fieldName: string) => {
        const formData = new FormData();
        formData.append(fieldName, file);
        const url = `http://localhost:8005/api/v1/DocumentationList/upload/${username}/${file.name.replace('.pdf', '')}`
        return fetch(url, {
            method: 'POST',
            body: formData
        }).then(data => data.json());
    }

    const downloadFile = (username: string, fileName: string) => {
        const url = `http://localhost:8005/api/v1/DocumentationList/get-document/${username}/${fileName}`
        return fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.blob());
    }

    const deleteFile = (username: string, fileName: string) => {
        const url = `http://localhost:8005/api/v1/DocumentationList/delete/${username}/${fileName}`
        return fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json());
    }

    const getMissingDocumentationList = (username: string): Promise<any> => {
        const url = `http://localhost:8005/api/v1/DocumentationList/get-missing/${username}`;
        return fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json());
    }

    return {fetchDocumentationList, uploadFile, downloadFile, deleteFile, getMissingDocumentationList};
    
}

export default DocumentationService();