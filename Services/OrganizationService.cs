using antheap1.Models;
using antheap1.Data;

namespace antheap1.Services;

public class OrganizationService
{
    private readonly MyDbContext _context;

    public OrganizationService(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Organization> GetAll()
    {
        return _context.Organizations.OrderByDescending(x=>x.Id).ToList<Organization>();
    }

    public Organization? GetByNIP(string nip)
    {
        return _context.Organizations.SingleOrDefault(item => item.Nip == nip);
    }

    public Organization Create(Organization item)
    {
        _context.Organizations.Add(item);
        _context.SaveChanges();

        return item;
    }

    public void DeleteById(int id)
    {
        var item = _context.Organizations.Find(id);
        if (item is not null)
        {
            _context.Organizations.Remove(item);
            _context.SaveChanges();
        }
    }
}