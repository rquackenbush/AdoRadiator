using System;

namespace AdoRadiator.Core.Models;

public class CurrentIteration
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public Uri Uri { get; set; }

    public CurrentIterationAttributes? Attributes { get; set; }
}
