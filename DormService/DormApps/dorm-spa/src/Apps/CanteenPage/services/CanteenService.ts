import BaseService from "../../../services/BaseService";
import { NewUserMeals, UserMeals } from "../models/UserMealsModel";

interface ICanteenService {
    getUserMealsByUsername : (username : string) => Promise<UserMeals>; 
    addNewItemToOrder: (newMealItem: NewUserMeals) => Promise<boolean>;
}

const CanteenService: () => ICanteenService = () => {


    const getUserMealsByUsername = (username : string) => {
        return BaseService.get(`http://localhost:8002/api/v1/UserMeals/${username}`);
    };

    const addNewItemToOrder = (newMealItem : NewUserMeals) => {
        return BaseService.put(`http://localhost:8002/api/v1/OrderMeals`, newMealItem);
    }

    return {getUserMealsByUsername, addNewItemToOrder};
}

export default CanteenService();