export type WashingMachine = {
    _id: string;
    configurationId: string;
    time: string;
    date: string;
    reserved: boolean;
    spinRate: number;
    washingTemperature: number;
};

export enum Timeframes {
    MORNING = "Morning",
    NOON = "Noon",
    AFTERNOON = "Afternoon"
}