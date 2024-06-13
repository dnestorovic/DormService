import BaseService from "../../../services/BaseService";
import { StudentDebts } from "../models/DebtsModel";

interface IPaymentService {
    getDebtsByStudentID : (studentID : string) => Promise<StudentDebts>; 
}

const PaymentService: () => IPaymentService = () => {
    

    const getDebtsByStudentID = (studentID : string) => {
        return BaseService.get(`http://localhost:8001/api/v1/Debts/${studentID}`);
    };

    return {getDebtsByStudentID};
}

export default PaymentService();