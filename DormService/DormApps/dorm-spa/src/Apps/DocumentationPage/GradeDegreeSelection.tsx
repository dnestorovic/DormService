import React, { ChangeEvent } from 'react';
import DropdownMenu from '../DocumentationPage/DropDownMenu';


interface GradeDegreeSelectionProps {
    grade: string;
    onGradeChange: (grade: string) => void;
    degree: string;
    onDegreeChange: (degree: string) => void;
}

const GradeDegreeSelection: React.FC<GradeDegreeSelectionProps> = ({ grade, onGradeChange, degree, onDegreeChange }) => {
    const gradeOptions = ["1", "2", "3", "4", "5+"];
    const degreeOptions = ["I", "II", "III"];

    const handleGradeChange = (selectedGrade: string) => {
        onGradeChange(selectedGrade);
    };

    const handleDegreeChange = (selectedDegree: string) => {
        onDegreeChange(selectedDegree);
    };

    return (
        <div className="grade-degree-selection">
            <label className="input-label">Grade of studies:</label>
            <DropdownMenu 
                title={"Select Grade of Studies"}
                options={gradeOptions}
                selectedValue={grade} // Pass selected grade as selectedValue
                onSelect={handleGradeChange}
            />
            <label className="input-label">Degree of studies:</label>
            <DropdownMenu 
                title={"Select Degree of Studies"}
                options={degreeOptions}
                selectedValue={degree} // Pass selected degree as selectedValue
                onSelect={handleDegreeChange}
            />
        </div>
    );
};

export default GradeDegreeSelection;
