using System.ComponentModel.DataAnnotations;

namespace antheap1.Models;

public class Organization
{
    public int Id {get; set; }   
    public string Name {get; set; } = null!;
    public string Nip {get; set; } = null!;
    public string? WorkingAddress {get; set; }
}