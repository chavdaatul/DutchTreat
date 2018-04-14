using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat.Data;
using Microsoft.Extensions.Logging;
using AutoMapper;
using DutchTreat.Data.Entities;

namespace DutchTreat.ViewModels
{
  [Route("/api/orders/{orderid}/items")]
  public class OrderItemsController : Controller
  {
    private readonly IDutchRepository _repository;
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IMapper _mapper;

    public OrderItemsController(IDutchRepository repository,
      ILogger<OrderItemsController> logger,
      IMapper mapper
      )
    {
      this._repository = repository;
      this._logger = logger;
      this._mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(int orderId)
    {
      var order = _repository.GetOrderById(orderId);
      if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
      else return NotFound();
    }

    [HttpGet("{Id}")]    
    public IActionResult Get(int orderId, int Id)
    {
      var order = _repository.GetOrderById(orderId);
      if (order != null)
      {
        var item = order.Items.Where(i => i.Id == Id).FirstOrDefault();
        if (item != null)
        {
          return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
        }
        else { return NotFound(); }
        
      }
      else return NotFound();
    }

  }
}