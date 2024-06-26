﻿using Canteen.API.GrpcServices;
using Canteen.API.OrderMealsInfo.Entities;
using Canteen.API.OrderMealsInfo.Repositories;
using Canteen.API.UserMealsInfo.Entities;
using Canteen.API.UserMealsInfo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Mailing.Data;
using Mailing;

namespace Canteen.API.OrderMealsInfo.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderMealsController : ControllerBase
    {
        private readonly IOrderMealsRepository _repository;
        private readonly IUserMealsRepository _userMealsRepository;
        private readonly ILogger<OrderMealsController> _logger;
        private readonly PaymentGrpcService _paymentGrpcService;
        private readonly IEmailService _emailService;

        public OrderMealsController(IOrderMealsRepository repository, IUserMealsRepository userMealsRepository, ILogger<OrderMealsController> logger, PaymentGrpcService paymentGrpcService, IEmailService emailService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userMealsRepository = userMealsRepository ?? throw new ArgumentNullException(nameof(userMealsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _paymentGrpcService = paymentGrpcService ?? throw new ArgumentNullException(nameof(paymentGrpcService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(OrderMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderMeals>> GetOrder(string username)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
               return Forbid();
            }

            var order = await _repository.GetOrder(username);
            return Ok(order ?? new OrderMeals(username));
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderMeals), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderMeals>> UpdateOrder([FromBody] NewOrderItem order)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != order.Username)
            {
                return Forbid();
            }
            var oldOrder = await _repository.GetOrder(order.Username) ?? new OrderMeals(order.Username);

            var exists = oldOrder.Items.Find(p => p.MealType == order.MealType);
            if (exists != null)
            {
                exists.NumberOfMeals += order.NumberOfMeals;
            }
            else
            {
                var orderItem = new OrderMealsItem(numberOfMeals: order.NumberOfMeals, mealType: order.MealType);
                oldOrder.Items.Add(orderItem);
            }        
            
            return Ok(await _repository.UpdateOrder(oldOrder));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteOrder(string username)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
                return Forbid();
            }

            await _repository.DeleteOrder(username);
            return Ok();

        }


        [Route("[action]")]
        [HttpHead]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserMeals>> Checkout(string username, string emailAddress)
        {
            if (User.FindFirst(ClaimTypes.Name).Value != username)
            {
                return Forbid();
            }

            var order = await _repository.GetOrder(username);
            if (order == null)
            {
                return BadRequest();
            }

            List<OrderMealsItem> orderMealItems = order.Items;
            var breakfastNum = 0;
            var lunchNum = 0;
            var dinnerNum = 0;

            // Check which meal type an how many meals are ordered
            foreach (var item in orderMealItems)
            {
                if (item.MealType == "Breakfast")
                {
                    breakfastNum = item.NumberOfMeals;
                }

                if (item.MealType == "Lunch")
                {
                    lunchNum = item.NumberOfMeals;
                }

                if (item.MealType == "Dinner")
                {
                    dinnerNum = item.NumberOfMeals;
                }
            }


            var mealsToAdd = new UserMeals()
            {
                Username = username,
                Breakfast = breakfastNum,
                Lunch = lunchNum,
                Dinner = dinnerNum
            };
            try
            {
                var response = await _paymentGrpcService.ReduceCredit(username, Decimal.ToInt32(order.TotalPrice));

                if (response.SuccessfulTransaction)
                {
                    // If credit is successfully reduced, meals info should be updated, and order deleted
                    await _userMealsRepository.UpdateUserMeals(mealsToAdd);
                    await DeleteOrder(username);

                    // Send confirmation email
                    Email email = new(emailAddress, "Hi " + username + ",\nYou successfuly bought new meals.\nRegards,\nDormService", "Order meals confirmation");
                    var emailSent = await _emailService.SendEmail(email);
                    if (!emailSent)
                    {
                        await _emailService.SendEmail(email);
                    }
                  
                    return Accepted();
                }
            }
            catch (RpcException e)
            {
                _logger.LogInformation("Error while calling Payment service: {message}",  e.Message);
            }

            return BadRequest();

        }
    }
}
