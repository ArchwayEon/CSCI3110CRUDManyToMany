using System.ComponentModel.DataAnnotations;

namespace CSCI3110CRUDManyToMany.Models.Entities;

public class Internship
{
    public int Id { get; set; }
    [StringLength(20)]
    public string Name { get; set; } = String.Empty;
    [StringLength(256)]
    public string Description { get; set; } = String.Empty;
    public string StudentENumber { get; set; } = String.Empty;
    public Student? Student { get; set; }
}


