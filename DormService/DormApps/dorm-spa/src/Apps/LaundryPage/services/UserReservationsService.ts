import { WashingMachine } from '../models/WashingMachine';
import BaseService from './../../../services/BaseService';

interface IUserReservationService {
    getWashingMachinesByDate: (date: string) => Promise<WashingMachine[]>;
    reserveWashingMachine: (machine: WashingMachine, studentId: string, studnetEmail: string) => Promise<boolean>;
    getMachineWithDiscount: () => Promise<any>;
    getWashingMachinesByStudentId: (id: string) => Promise<WashingMachine[]>;
}

const UserReservationService: () => IUserReservationService = () => {
    const baseUrl = 'http://localhost:8000/WashingMachine';

    const getWashingMachinesByDate = (date: string) => {
        return BaseService.get(baseUrl + "/all/" + date);
    };
    
    const reserveWashingMachine = (machine: WashingMachine, studentId: string, studnetEmail: string) => {
        return BaseService.put<WashingMachine>(baseUrl, {...machine, id: machine._id, studentId: studentId, emailAddress: studnetEmail});
    };

    const getMachineWithDiscount = () => {
        return BaseService.get(baseUrl + "/economic");
    }

    const getWashingMachinesByStudentId = (id: string) => {
        return BaseService.get(baseUrl + "/student/" + id);
    }

    return { getWashingMachinesByDate, reserveWashingMachine, getMachineWithDiscount, getWashingMachinesByStudentId };
};

export default UserReservationService();