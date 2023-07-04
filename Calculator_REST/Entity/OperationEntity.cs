using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculator_REST.Entity;

public class OperationEntity
{
    [Key] public int OperationId { get; set; }
    public string? Expression { get; set; }
    public long? Result { get; set; }
    public bool? Exception { get; set; }
    [ForeignKey("ExceptionId")] public int ExceptionId;
}