namespace AdoRadiator.Core.Models;

public class AdoListResponse<TItem>
{
    public int Count { get; set; }

    public TItem[]? Value { get; set; }
}
