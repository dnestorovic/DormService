import React, { useEffect, useState } from 'react';
import { DocumentationList } from './models/DocumentationListModel';
import DocumentationService from './services/DocumentationService';
import DocumentForm from './DocumentForm';
import GradeDegreeSelection from './GradeDegreeSelection';

export default function DocumentationPage() {
    const [username, setUsername] = useState<string>('teodoravasic');
    const [email, setEmail] = useState<string>('tekisooj@gmail.com');
    const [documents, setDocuments] = useState<DocumentationList | null>(null);
    const [grade, setGrade] = useState<string>(''); // State for grade
    const [degree, setDegree] = useState<string>(''); // State for degree
    const [PO, setPO] = useState<string>(''); // State for PO input value
    const [ESPB, setESPB] = useState<string>(''); // State for ESPB input value
    const [C, setC] = useState<string>(''); // State for C input value
    const [BZI, setBZI] = useState<string>(''); // State for BZI input value
    const [BB, setBB] = useState<string>('___');
    const [keysToShowDocumentList, setKeysToShow] = useState<Array<keyof DocumentationList>>([]);
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
    }, [documentationService, username]);

    const handleUpload = async (key: keyof DocumentationList, file: File) => {
        console.log(`Uploading ${key}`, file);

        const response = await documentationService.uploadFile(username, email, file, key);

        const data = await documentationService.fetchDocumentationList(username);
        setDocuments(data);
    };

    const handleDownload = async (key: keyof DocumentationList) => {
        console.log(`Downloading ${key}`);
        await documentationService.downloadFile(username, key);
    };

    const handleDelete = async (key: keyof DocumentationList) => {
        console.log(`Deleting ${key}`);
        await documentationService.deleteFile(username, key);
    };

    // Function to handle grade change
    const handleGradeChange = (selectedGrade: string) => {
        setGrade(selectedGrade);
        updateKeysToShow(selectedGrade, degree);
    };

    // Function to handle degree change
    const handleDegreeChange = (selectedDegree: string) => {
        setDegree(selectedDegree);
        updateKeysToShow(grade, selectedDegree);
    };

    // Function to handle input change for PO
    const handlePOChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPO(event.target.value);
    };

    // Function to handle input change for ESPB
    const handleESPBChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setESPB(event.target.value);
    };

    // Function to handle input change for C
    const handleCChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setC(event.target.value);
    };

    // Function to handle input change for BZI
    const handleBZIChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setBZI(event.target.value);
    };

    const handleSubmit = () => {
        // Perform calculations or submit the form based on the input values
        if (grade === '1' && degree === 'I') {
            if(PO === "")
                return;
            setBB((parseFloat(PO) * 8).toString());
        } else if ((degree === "I" && (grade === "2" || grade === "3" || grade === "4")) || degree === "II" || degree === "III") {
            if(PO==="" || ESPB==="" || C==="")
                return;
            setBB((parseFloat(PO) * 5 + parseFloat(ESPB) / parseFloat(C) * 0.8).toString());
        } else if (grade === '5+' && degree === 'I') {
            if(PO===""||BZI==="")
                return;
            setBB((parseFloat(PO) * 8 - parseFloat(BZI) * 2).toString());
        }
    };

    // Function to update keys to show based on grade and degree
    const updateKeysToShow = (selectedGrade: string, selectedDegree: string) => {
        const newKeysToShow: Array<keyof DocumentationList> = [];
        if(selectedGrade == "" || selectedDegree ==""){
            setKeysToShow(newKeysToShow);
            return;
        }
        newKeysToShow.push("applicationForm");
        if (selectedGrade === '1' && selectedDegree === 'I') {
            newKeysToShow.push("firstTimeStudentCertificate");
            newKeysToShow.push("highSchoolFirstYearCertificate");
            newKeysToShow.push("highSchoolSecondYearCertificate");
            newKeysToShow.push("highSchoolThirdYearCertificate");
            newKeysToShow.push("highSchoolFourthYearCertificate");
        } else {
            newKeysToShow.push("avgGradeCertificate");
            newKeysToShow.push("facultyDataForm");
            newKeysToShow.push("incomeCertificate");
            if (selectedDegree === "II" || selectedDegree === "III") {
                newKeysToShow.push("unemploymentCertificate");
            }
        }
        setKeysToShow(newKeysToShow);
    };

    return (
        <div className="documentation-page-container">
            <div className='top-panel panel'>
                <GradeDegreeSelection
                    grade={grade}
                    onGradeChange={handleGradeChange}
                    degree={degree}
                    onDegreeChange={handleDegreeChange}
                />
            </div>
            <div className="parentDiv">
                <div className="left-panel panel">
                    <h1>Documents</h1>
                    {documents ? (
                        <div className="upload-form">
                            {Object.keys(documents).map((key) => {
                                if (keysToShowDocumentList.includes(key as keyof DocumentationList)) {
                                    return (
                                        <DocumentForm
                                            key={key}
                                            id={key as keyof DocumentationList}
                                            document={documents[key as keyof DocumentationList]}
                                            onUpload={(file) => handleUpload(key as keyof DocumentationList, file)}
                                            onDownload={() => handleDownload(key as keyof DocumentationList)}
                                            onDelete={() => handleDelete(key as keyof DocumentationList)}
                                        />
                                    );
                                } else {
                                    return null;
                                }
                            })}
                        </div>
                    ) : (
                        <p>Loading...</p>
                    )}
                </div>
                <div className="right-panel panel">
                    <h1>
                        Calculate your points
                    </h1>
                    {/* Render input fields based on grade and degree */}
                    {grade === '1' && degree === 'I' && (
                        <div>
                            <div className="form-row">
                                <label>Average grade during studies:</label>
                                <input type="text" value={PO} onChange={handlePOChange} />
                            </div>
                            <div className="form-row">
                                <button className="submit-button" onClick={handleSubmit}>Submit</button>
                                <h3>You have {BB} points</h3>
                            </div>
                        </div>
                    )}
                    {( (degree === "I" && (grade === "2" || grade === "3" || grade === "4")) || degree === "II" || degree === "III") && (
                        <div>
                            <div className="form-row">
                                <label>Average grade during studies:</label>
                                <input type="text" value={PO} onChange={handlePOChange} />
                            </div>            
                            <div className="form-row">
                                <label>Number of earned ESPB points:</label>
                                <input type="text" value={ESPB} onChange={handleESPBChange} />
                            </div>
                            <div className="form-row">
                                <label>Number of years of studies:</label>
                                <input type="text" value={C} onChange={handleCChange} />
                            </div>
                            <div className="form-row">
                                <button className="submit-button" onClick={handleSubmit}>Submit</button>
                                <h3>You have {BB} points</h3>
                            </div>
                        </div>
                    )}
                    {grade === '5+' && degree === 'I' && (
                        <div>
                            <div className="form-row">
                                <label>Average grade during studies:</label>
                                <input type="text" value={PO} onChange={handlePOChange} />
                            </div>
                            <div className="form-row">
                                <label>Number of leftover exams:</label>
                                <input type="text" value={BZI} onChange={handleBZIChange} />
                            </div>
                            <div className="form-row">
                                <button className="submit-button" onClick={handleSubmit}>Submit</button>
                                <h3>You have {BB} points</h3>
                            </div>
                        </div>
                    )}

                </div>
            </div>
        </div>
    );
}
