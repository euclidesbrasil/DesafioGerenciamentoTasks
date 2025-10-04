using System.Diagnostics.CodeAnalysis;

namespace ArquiteturaDesafio.Core.Domain.Common;
[ExcludeFromCodeCoverage]
public abstract class BaseEntityNoRelational
{
    public int Id { get; set; }
}
