using System.ComponentModel.DataAnnotations;

namespace Calculator_REST.Entity;

public class ExceptionEntity
{
    [Key] public int ExceptionId { get; set; }
}