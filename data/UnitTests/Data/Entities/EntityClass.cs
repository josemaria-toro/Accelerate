using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Data.Entities;

[ExcludeFromCodeCoverage]
internal class EntityClass : Entity
{
    public Boolean Boolean { get; set; }
    public DateTime DateTime { get; set; }
    public Decimal Decimal { get; set; }
    public Double Double { get; set; }
    public Guid Guid { get; set; }
    public Int16 Int16 { get; set; }
    public Int32 Int32 { get; set; }
    public Int64 Int64 { get; set; }
    public Single Single { get; set; }
    public String String { get; set; }
    [Column("property_with_attribute")]
    public String PropertyWithAttribute { get; set; }
}