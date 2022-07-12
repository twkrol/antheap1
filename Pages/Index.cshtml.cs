using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using antheap1.Data;
using antheap1.Models;
using antheap1.Services;

namespace antheap1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Data.MyDbContext _context;
    
    [TempData]
    public string? Message {get; set; }

    [BindProperty]
    public string? nip {get; set; }

    public IEnumerable<Organization>? organizations {get; set; }

    public IndexModel(ILogger<IndexModel> logger, Data.MyDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult OnGet()
    {
        var service = new OrganizationService(_context);
        organizations = service.GetAll();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (nip is not null)
        {
            string? status = null;
            
            nip = nip.Replace("-", String.Empty);

            Organization? item = RejestrWL.FindOrganizationByNIP(nip, ref status);
            if (item is null){
                Message = status;
            }
            else
            {
                var service = new OrganizationService(_context);

                var organization = service.GetByNIP(item.Nip);
                if (organization is null)
                {
                    service.Create(item);
                }
            }
        }
        
        return RedirectToPage("./Index");
    }
}
