export type UserMeals = 
{
    username: string,
    breakfast: number,
    lunch: number,
    dinner: number
};

export type NewUserMeals = 
{
    username: string,
    items: Item
};

export type Item = {
    mealType: string,
    numberOfMeals: number
}
