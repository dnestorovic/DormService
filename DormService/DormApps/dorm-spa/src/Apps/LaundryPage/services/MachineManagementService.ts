import { WashingMachineConfigurationRequest, WashingMachineConfigurations } from '../models/WashingMachine';
import BaseService from './../../../services/BaseService';

interface IMachineManagementService {
    getWashingMachinesConfigurations: () => Promise<WashingMachineConfigurations[]>;
    addWashingMachineConfiguration: (request: WashingMachineConfigurationRequest) => Promise<any>;
    deleteWashingMachineConfiguration: (id: string) => Promise<any>;
}

const MachineManagementService: () => IMachineManagementService = () => {
    const baseUrl = 'http://localhost:8000/room';

    const getWashingMachinesConfigurations = () => {
        return BaseService.get(baseUrl);
    };

    const addWashingMachineConfiguration = (request: WashingMachineConfigurationRequest) => {
        return BaseService.post<WashingMachineConfigurationRequest>(baseUrl, request);
    };

    const deleteWashingMachineConfiguration = (id: string) => {
        return BaseService.delete(`${baseUrl}/${id}`);
    };
    
    
    return { getWashingMachinesConfigurations, addWashingMachineConfiguration, deleteWashingMachineConfiguration };
};

export default MachineManagementService();