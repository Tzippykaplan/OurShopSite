using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTO;

public record OrderPostDto(int orderId,DateOnly orderDate,string orderName, int userId,int orderSum,List<OrderItemDto> OrderItems);
public record returnOrderDto(DateOnly orderDate, string orderName, int userId, List<OrderItemDto> OrderItems);
public record returnOrdersListDto( DateOnly orderDate, string orderName);


