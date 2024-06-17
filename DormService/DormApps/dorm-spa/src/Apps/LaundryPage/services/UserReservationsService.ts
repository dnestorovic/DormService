import { WashingMachine } from '../models/WashingMachine';
import BaseService from './../../../services/BaseService';

interface IUserReservationService {
    getWashingMachinesByDate: (date: string) => Promise<WashingMachine[]>;
    reserveWashingMachine: (machine: WashingMachine) => Promise<boolean>;

}

const UserReservationService: () => IUserReservationService = () => {
    const baseUrl = 'http://localhost:8000/WashingMachine';

    const getWashingMachinesByDate = (date: string) => {
        return BaseService.get(baseUrl + "/all/" + date);
    };
    
    const reserveWashingMachine = (machine: WashingMachine) => {
        
        return BaseService.put<WashingMachine>(baseUrl, {...machine, id: machine._id, studentId: "Momcilo"});
    };

    return { getWashingMachinesByDate, reserveWashingMachine };
};

export default UserReservationService();