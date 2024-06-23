import BaseService from "../../../services/BaseService";
import { StudentDebts } from "../models/DebtsModel";

interface IPaymentService {
    getDebtsByUsername : (username : string) => Promise<StudentDebts>;
    updateStudentDebts : (debtUpdate : StudentDebts, emailAddress : string) => Promise<boolean>;
    deleteStudentByUsername : (username : string) => Promise<StudentDebts>; 
    createDefaultStudent : (studentDebts : StudentDebts) => Promise<StudentDebts>; 
}

const PaymentService: () => IPaymentService = () => {
    

    const getDebtsByUsername = (username : string) => {
        return BaseService.get(`http://localhost:8001/api/Debts/${username}`);
    };

    const updateStudentDebts = (debtUpdate : StudentDebts, emailAddress : string) => {
        return BaseService.put(`http://localhost:8001/api/Debts?emailAddress=${emailAddress}`, debtUpdate);
    };

    
    const deleteStudentByUsername = (username : string) => {
        return BaseService.delete(`http://localhost:8001/api/Debts/${username}`);
    };

    const createDefaultStudent = (studentDebts : StudentDebts) => {
        return BaseService.post(`http://localhost:8001/api/Debts/`, studentDebts);
    }

    return {getDebtsByUsername, updateStudentDebts, deleteStudentByUsername, createDefaultStudent};
}

export default PaymentService();