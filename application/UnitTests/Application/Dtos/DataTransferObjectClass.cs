using System;
using System.Diagnostics.CodeAnalysis;

namespace Accelerate.Application.Dtos;

[ExcludeFromCodeCoverage]
internal class DataTransferObjectClass : DataTransferObject
{
    public String Property { get; set; }
}