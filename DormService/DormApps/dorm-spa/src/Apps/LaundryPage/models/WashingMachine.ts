export type WashingMachine = {
    _id: string;
    id?: string;
    configurationId: string;
    studentId: string;
    time: string;
    date: string;
    reserved: boolean;
    spinRate?: number;
    washingTemperature?: number;
};

export enum Timeframes {
    MORNING = "Morning",
    NOON = "Noon",
    AFTERNOON = "Afternoon"
}