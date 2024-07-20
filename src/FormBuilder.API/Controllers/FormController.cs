using FormBuilder.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.API.Controllers;

[Route("api/forms")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public FormController(ApplicationDbContext context)
    {
        _context = context;
    }
}
