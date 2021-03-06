﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using DutchTreat.ViewModels;
using AutoMapper;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  public class OrdersController : Controller
  {
    private readonly IDutchRepository _repository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;

    public OrdersController(IDutchRepository repository,
      ILogger<OrdersController> logger,
      IMapper mapper
      )
    {
      this._repository = repository;
      this._logger = logger;
      this._mapper = mapper;
    }
    [HttpGet]
    public IActionResult Get(bool includeItems = true)
    {
      try
      {
        var results = _repository.GetAllOrders(includeItems);
        return Ok(_mapper.Map<IEnumerable<Order>, List<OrderViewModel>>(results));
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders:{ex}");
        return BadRequest($"Failed to get orders");
      }
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      try
      {
        var order = _repository.GetOrderById(id);
        if (order != null)
        {
          return Ok(_mapper.Map<Order, OrderViewModel>(order));
        }
        else
        {
          return NotFound();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders:{ex}");
        return BadRequest($"Failed to get orders");
      }
    }

    [HttpPost]
    public IActionResult Post([FromBody]OrderViewModel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var newOrder = _mapper.Map<OrderViewModel, Order>(model);

          //var newOrder = new Order()
          //{
          //  OrderDate = model.OrderDate,
          //  OrderNumber = model.OrderNumber,
          //  Id = model.OrderId
          //};

          if (newOrder.OrderDate == DateTime.MaxValue)
          {
            newOrder.OrderDate = DateTime.Now;
          }
          _repository.AddEntity(newOrder);
          if (_repository.SaveAll())
          {

            //var vm = new OrderViewModel()
            //{
            //  OrderDate = newOrder.OrderDate,
            //  OrderId = newOrder.Id,
            //  OrderNumber = newOrder.OrderNumber
            //};
            
            return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
          }
        }
        else
        {
          return BadRequest(ModelState);
        }

      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to save new order:{ex}");
      }
      return BadRequest("Failed to save new order");
    }
  }
}