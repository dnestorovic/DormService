import React, { useState } from 'react'
import { useMount } from 'react-use'
import { UserMeals, NewUserMeals, Item, NewOrderItem } from "./models/UserMealsModel";
import CanteenService from './services/CanteenService';
import { OrderMeals, OrderMealsItem } from './models/OrderMealsModel';

export default function CanteenPage() {
  const [userMealsData, setUserMealsData] = useState<UserMeals>();
  const [selectedMealType, setSelectedMealType] = useState<string>('');
  const [amount, setAmount] = useState<number>(0);
  const [orderMeals, setOrderMeals] = useState<OrderMeals>();

  useMount(() => {
    CanteenService.getUserMealsByUsername("Natalija")
    .then(setUserMealsData);

    CanteenService.getOrderMealsByUsername("Natalija")
    .then(setOrderMeals);
  });

  const handleAddClick = () => {

    const newOrderItem : NewOrderItem = {
      username : "Natalija",
      mealType: selectedMealType,
      numberOfMeals: amount
    }

    var successfullTransaction = CanteenService.addNewItemToOrder(newOrderItem);
    
    console.log(successfullTransaction);
    console.log(newOrderItem)

  }
  const handleBuyClick = () => {

    

  }
  const handleDeleteClick = () => {

 

  }

  console.log(userMealsData);
  
  return (
    <div className='canteen-page'>
      <div className='left-pannel'>

        <div className='title'>Buy New Meals</div>
        <div className="pattern">
          <div className='new-item'>
            <div className='field' title='Select meal type'>
              <div>
                <label htmlFor="mealType">Meal type:</label>
                <select className="select-type"
                  id="mealType"
                  value={selectedMealType}
                  onChange={(e) => setSelectedMealType(e.target.value)}
                >
                  <option value="" disabled>Select meal type</option>
                  <option value="Breakfast">Breakfast</option>
                  <option value="Lunch">Lunch</option>
                  <option value="Dinner">Dinner</option>
                </select>
              </div>
            </div>

            <div className="field" title='Enter number of meals you want to buy'>
              <div>
                <label htmlFor="amount">Number of meals:</label>
                <input className="inputAmount"
                  type="number"
                  id="amount"
                  value={amount}
                  onChange={(e) => setAmount(parseFloat(e.target.value))}
                />
              </div>
            </div>
            <button className="add-button" onClick={handleAddClick}>Add to cart!</button>
          </div>
        </div>

        <div className="title">Order</div>
        <div className="basket">
          {orderMeals && (
            <div>
              <div>
                {orderMeals?.items.map((item: OrderMealsItem, index: number) => (
                  <div key={index} className="item">
                    <div className="item-attribute">
                      <label>Meal Type:</label>
                      <span>{item.mealType}</span>
                    </div>
                    <div className="item-attribute">
                      <label>Number of Meals:</label>
                      <span>{item.numberOfMeals}</span>
                    </div>
                    <div className="item-attribute">
                      <label>Meal Price:</label>
                      <span>{item.mealPrice.toFixed(2)} rsd</span>
                    </div>
                  </div>
                ))}
              </div>
              <label>Total Price:</label>
              <span>{orderMeals?.totalPrice} rsd</span>
            </div>
          )}
          <div className="buttons">
            <button className="delete-button" onClick={handleBuyClick}>Delete Order</button>
            <button className="buy-button" onClick={handleDeleteClick}>Buy Meals!</button>
          </div>
        </div>   
      </div>
      <div className='right-pannel'>
        <div className='block-shadow'>
          <div className='title'>User Meals</div>
          <div className='user-meals-report'>
            <div className='user-meal'>
              <label>Breakfast:</label> 
              <span>{userMealsData?.breakfast || "No Data Found"}</span>
            </div>
            <div className='user-meal'>
              <label>Lunch:</label> 
              <span>{userMealsData?.lunch || "No Data Found"}</span>
            </div>
            <div className='user-meal'>
              <label>Dinner:</label> 
              <span>{userMealsData?.dinner || "No Data Found"}</span>
            </div>
          </div>
        </div>
      </div>

    </div>
  )
}