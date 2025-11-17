using System;

namespace AdoRadiator.Core.Models;

public class IterationNode
{
    public int Id { get; set; }

    public Guid Identifier { get; set; }

    public string Name { get; set; }

    public IterationNodeAttributes Attributes { get; set; }

    public string Path { get; set; }

    public IterationNode[] Children { get; set; }
}
