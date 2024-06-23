import { DocumentationList } from "../models/DocumentationListModel";
import { Document } from "../models/DocumentModel";

interface IDocumentationService {
    fetchDocumentationList: (username: string) => Promise<DocumentationList>;
    uploadFile: (username: string, email: string, file: File, fieldName: string) => Promise<any>;
    downloadFile: (username: string, fileName: string) => Promise<any>;
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
        try {
            const response = await fetch(`http://localhost:8005/DocumentationList/${username}`);
            
            if (!response.ok) {
                throw new Error(`Failed to fetch documentation list for ${username}`);
            }

            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
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
        } else {
            throw new Error('Unexpected content type in response');
    }
} catch (error) {
    console.error('Error fetching documentation list:', error);
    throw error;
}
};

    const uploadFile = async (username: string, email: string, file: File, title: string) => {
        const formData = new FormData();
        formData.append("file", file);
        formData.append("title", title);
    
        const url = `http://localhost:8005/DocumentationList/upload/${username}?emailAddress=${email}}`;
        
        return await fetch(url, {
            method: 'POST',
            body: formData
        }).then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response;
        });
    };

    const downloadFile = async (username: string, fileName: string) => {
        const url = `http://localhost:8005/DocumentationList/get-document/${username}/${fileName}`;
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
    
        const blob = await response.blob();
    
        const downloadUrl = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = downloadUrl;
        a.download = fileName; 
        document.body.appendChild(a);
        a.click();
        a.remove();
    };

    const deleteFile = async(username: string, fileName: string) => {
        const url = `http://localhost:8005/DocumentationList/delete/${username}/${fileName}`
        return await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response);
    }

    const getMissingDocumentationList = async (username: string): Promise<any> => {
        const url = `http://localhost:8005/DocumentationList/get-missing/${username}`;
        return await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json());
    }

    return {fetchDocumentationList, uploadFile, downloadFile, deleteFile, getMissingDocumentationList};
    
}

export default DocumentationService();