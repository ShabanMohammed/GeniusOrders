namespace GeniusOrders.Entities;

public class Decision
{
    public int Id { get; set; }
    public int DecisionNumber { get; set; }
    public int DecisionYear { get; set; }
    public DateOnly DecisionDate { get; set; }
    public string Content { get; set; } = default!;
    public string? Attachment { get; set; }





}