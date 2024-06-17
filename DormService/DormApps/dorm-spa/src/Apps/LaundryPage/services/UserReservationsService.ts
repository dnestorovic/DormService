import { WashingMachine } from '../models/WashingMachine';
import BaseService from './../../../services/BaseService';

interface IUserReservationService {
    getWashingMachinesByDate: (date: string) => Promise<WashingMachine[]>;
}

const UserReservationService: () => IUserReservationService = () => {
    const baseUrl = 'http://localhost:8000/api/WashingMachine';

    const getWashingMachinesByDate = (date: string) => {
        return BaseService.get(baseUrl + "/all/" + date);
    };
    
    return { getWashingMachinesByDate };
};

export default UserReservationService();