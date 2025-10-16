using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoneyFlow.Entities;

namespace MoneyFlow.Models;

public class ServiceVM
{
    public int ServiceId { get; set; }
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
}