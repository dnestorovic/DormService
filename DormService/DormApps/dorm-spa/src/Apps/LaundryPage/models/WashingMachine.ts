export type WashingMachine = {
    _id: string;
    id?: string;
    configurationId: string;
    studentId: string;
    emailAddress: string;
    time: string;
    date: string;
    reserved: boolean;
    spinRate?: number;
    washingTemperature?: number;
    price?: number;
};

export type WashingMachineConfigurations = {
    _id: string;
    manufacturer: string;
    startDate: string;
    expirationDate: string;
    utilizationFactor: number;
    responsiblePersonEmail: string;
}

export type WashingMachineConfigurationRequest = {
    manufacturer?: string;
    startDate?: string;
    expirationDate?: string;
    responsiblePersonEmail?: string;
}

export enum Timeframes {
    MORNING = "Morning",
    NOON = "Noon",
    AFTERNOON = "Afternoon"
}