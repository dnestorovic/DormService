import React, { useEffect, useState } from 'react';
import { DocumentationList } from './models/DocumentationListModel';
import DocumentationService from './services/DocumentationService';
import DocumentForm from './DocumentForm';
import GradeDegreeSelection from './GradeDegreeSelection';
import { useMount } from 'react-use';
import { useNavigate } from 'react-router-dom';
import { getRole } from '../../Utils/TokenUtil';
import { NotificationType, Notification } from '../../components/Notifications/Notification';

export default function DocumentationPage() {
    const [documents, setDocuments] = useState<DocumentationList | null>(null);
    const [grade, setGrade] = useState<string>(''); // State for grade
    const [degree, setDegree] = useState<string>(''); // State for degree
    const [PO, setPO] = useState<string>(''); // State for PO input value
    const [ESPB, setESPB] = useState<string>(''); // State for ESPB input value
    const [C, setC] = useState<string>(''); // State for C input value
    const [BZI, setBZI] = useState<string>(''); // State for BZI input value
    const [BB, setBB] = useState<string>('___');
    const [keysToShowDocumentList, setKeysToShow] = useState<Array<keyof DocumentationList>>([]);
    const [showNotification, setShowNotification] = useState<{type: NotificationType, message: string}>();


    const username: string = localStorage.getItem("username") ?? "";
    const email: string = localStorage.getItem("email") ?? "";

    const documentationService = DocumentationService;

    const navigate = useNavigate();
    useMount(() => {
        //Admin user should not be able to see this pae so it should be redirected to login page
        if (localStorage.getItem("username") === null || getRole() === "Administrator") {
            navigate('/login');
            return ;
        }
    });
    useEffect(() => {
        //On any change regarding the uploaded/deleted filea we should update the display
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


    //Send upload gile request to backend and show notification with response
    const handleUpload = async (key: keyof DocumentationList, file: File) => {
        console.log(`Uploading ${key}`, file);

        await documentationService.uploadFile(username, email, file, key).then(async () => {
            setShowNotification({type: NotificationType.Success, message: "Document uploaded successfully!"}) // OK
            await documentationService.fetchDocumentationList(username).then(response=>{
            
                setDocuments(response);
            })
        }).catch(() => setShowNotification({type: NotificationType.Error, message: "Something went wrong!"})); // Something went wrong

    };

    //Send request to download file to backend
    const handleDownload = async (key: keyof DocumentationList) => {
        console.log(`Downloading ${key}`);
        await documentationService.downloadFile(username, key);
    };

    //Send delete request to backend and show notification with response
    const handleDelete = async (key: keyof DocumentationList) => {
        console.log(`Deleting ${key}`);
        await documentationService.deleteFile(username, key).then(()=>{
            setShowNotification({type: NotificationType.Success, message: "Document deleted successfully!"})
        }).catch(()=> {
            setShowNotification({type: NotificationType.Error, message: "Something went wrong!"})
        })
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
            if(PO === ""){
                return;
            }
            if(parseFloat(PO)<6 || parseFloat(PO)>10){
                alert("Average grade must be between 6.0 and 10.0");
                return;
            }
            setBB((parseFloat(PO) * 8).toString());
        } else if ((degree === "I" && (grade === "2" || grade === "3" || grade === "4")) || degree === "II" || degree === "III") {
            if(PO==="" || ESPB==="" || C===""){
                return;
            }           
            if(parseFloat(PO)<6 || parseFloat(PO)>10){
                alert("Average grade must be between 6.0 and 10.0");
                return;
            }
            if(parseFloat(ESPB)<0){
                alert("Number of earned ESPB points must be >= 0");
                return;
            }
            if(parseFloat(C)<=0){
                alert("Number of years of studies must be greater than 0");
                return;
            }
            setBB((parseFloat(PO) * 5 + parseFloat(ESPB) / parseFloat(C) * 0.8).toString());
        } else if (grade === '5+' && degree === 'I') {
            if(PO===""||BZI===""){
                return;
            }           
            if(parseFloat(PO)<6 || parseFloat(PO)>10){
                alert("Average grade must be between 6.0 and 10.0");
                return;
            }           
            if(parseFloat(BZI)<=0){
                alert("Number of leftover exams must be greater than 0");
                return;
            }
            setBB((parseFloat(PO) * 8 - parseFloat(BZI) * 2).toString());
        }
    };

    const updateKeysToShow = (selectedGrade: string, selectedDegree: string) => {
        // Function to update form keys to show based on grade and degree
        const newKeysToShow: Array<keyof DocumentationList> = [];
        if(selectedGrade === "" || selectedDegree ===""){
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
                            {!showNotification ? null : <Notification type={showNotification.type} text={showNotification.message || ''} onRemove={() => setShowNotification(undefined)} />}

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
                                <br></br>
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
                                <br></br>
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
                                <br></br>
                                <h3>You have {BB} points</h3>
                            </div>
                        </div>
                    )}

                </div>
            </div>
        </div>
    );
}
