using BlazingPizza.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialsController(PizzaStoreContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
    {
        return (await dbContext.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
    }

    
}