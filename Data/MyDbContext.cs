using Microsoft.EntityFrameworkCore;
using antheap1.Models;

namespace antheap1.Data;

public class MyDbContext: DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
    {}

    public DbSet<Organization> Organizations => Set<Organization>();
}