using System;

namespace AdoRadiator.Core.Models;

public class CurrentIterationAttributes
{
    public DateTime StartDate { get; set; }

    public DateTime FinishDate { get; set; }

    public string? TimeFrame { get; set; }
}
