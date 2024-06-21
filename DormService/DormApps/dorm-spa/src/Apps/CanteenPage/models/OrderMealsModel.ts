export type OrderMeals = 
{
    username: string,
    items: OrderMealsItem[],
    totalPrice: number
};

export type OrderMealsItem = {
    mealType: string,
    numberOfMeals: number,
    mealPrice: number
}