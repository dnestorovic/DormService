import BaseService from "../../../services/BaseService";
import { StudentDebts } from "../models/DebtsModel";

interface IPaymentService {
    getDebtsByUsername : (username : string) => Promise<StudentDebts>;
    updateStudentDebts : (debtUpdate : StudentDebts) => Promise<boolean>;
    deleteStudentByUsername : (username : string) => Promise<StudentDebts>; 
    createDefaultStudent : (studentDebts : StudentDebts) => Promise<StudentDebts>; 
}

const PaymentService: () => IPaymentService = () => {
    

    const getDebtsByUsername = (username : string) => {
        return BaseService.get(`http://localhost:8001/api/v1/Debts/${username}`);
    };

    const updateStudentDebts = (debtUpdate : StudentDebts) => {
        return BaseService.put(`http://localhost:8001/api/v1/Debts`, debtUpdate);
    };

    
    const deleteStudentByUsername = (username : string) => {
        return BaseService.delete(`http://localhost:8001/api/v1/Debts/${username}`);
    };

    const createDefaultStudent = (studentDebts : StudentDebts) => {
        return BaseService.post(`http://localhost:8001/api/v1/Debts/`, studentDebts);
    }

    return {getDebtsByUsername, updateStudentDebts, deleteStudentByUsername, createDefaultStudent};
}

export default PaymentService();