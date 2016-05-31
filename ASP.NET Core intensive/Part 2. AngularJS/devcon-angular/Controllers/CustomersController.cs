using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DevCon.Controllers
{
    public class Customer{
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
    
    
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {            
            return new Customer[]{
                new Customer {Id = 1, Name = "Ivan Ivanov", Age = 20}, 
                new Customer {Id = 2, Name = "Petr Petrov", Age = 28}, 
                new Customer {Id = 3, Name = "Denis Denisov", Age = 14}, 
                new Customer {Id = 4, Name = "Ivan Ivanov", Age = 20}, 
                new Customer {Id = 5, Name = "Sergey Pugachev", Age = 31}, 
                new Customer {Id = 6, Name = "Stas Pavlov", Age = 17}, 
                new Customer {Id = 7, Name = "Mik Chernomordikov", Age = 99}, 
                new Customer {Id = 8, Name = "Ivan Ivanov", Age = 11}, 
                new Customer {Id = 9, Name = "Petr Ivanov", Age = 18}, 
                new Customer {Id = 10, Name = "Maks Sidorov", Age = 24},                
            };
        }
    }
}
