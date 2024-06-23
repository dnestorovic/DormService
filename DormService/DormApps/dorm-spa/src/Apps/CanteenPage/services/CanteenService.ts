import BaseService from "../../../services/BaseService";
import { OrderMeals } from "../models/OrderMealsModel";
import { NewOrderItem, UserMeals } from "../models/UserMealsModel";

interface ICanteenService {
    getUserMealsByUsername : (username : string) => Promise<UserMeals>; 
    addNewItemToOrder: (newMealItem: NewOrderItem) => Promise<OrderMeals>;
    getOrderMealsByUsername : (username : string) => Promise<OrderMeals>;
    checkoutOrder : (userame : string, email: string) => Promise<boolean>;
    deleteOrder : (userame : string) => Promise<boolean>;
}

const CanteenService: () => ICanteenService = () => {


    const getUserMealsByUsername = (username : string) => {
        return BaseService.get(`http://localhost:8002/api/UserMeals/${username}`);
    };

    const addNewItemToOrder = (newMealItem : NewOrderItem) => {
        return BaseService.put(`http://localhost:8002/api/OrderMeals`, newMealItem);
    }

    const getOrderMealsByUsername = (username : string) => {
        return BaseService.get(`http://localhost:8002/api/OrderMeals/${username}`)
    }
    const checkoutOrder = (username : string, email: string) => {
        return BaseService.head(`http://localhost:8002/api/OrderMeals/Checkout?username=${username}&emailAddress=${email}`)
    }
    const deleteOrder = (username : string) => {
        return BaseService.delete(`http://localhost:8002/api/OrderMeals/${username}`)
    }

    return {getUserMealsByUsername, addNewItemToOrder, getOrderMealsByUsername, checkoutOrder, deleteOrder};
}

export default CanteenService();