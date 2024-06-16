import BaseService from "../../../services/BaseService";
import { StudentDebts } from "../models/DebtsModel";

interface IPaymentService {
    getDebtsByStudentID : (studentID : string) => Promise<StudentDebts>;
    updateStudentDebts : (debtUpdate : StudentDebts) => Promise<boolean>; 
}

const PaymentService: () => IPaymentService = () => {
    

    const getDebtsByStudentID = (studentID : string) => {
        return BaseService.get(`http://localhost:8001/api/v1/Debts/${studentID}`);
    };

    const updateStudentDebts = (debtUpdate : StudentDebts) => {
        return BaseService.put(`http://localhost:8001/api/v1/Debts`, debtUpdate);
    };

    return {getDebtsByStudentID, updateStudentDebts};
}

export default PaymentService();