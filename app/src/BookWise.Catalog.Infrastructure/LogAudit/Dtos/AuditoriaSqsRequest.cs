﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BookWise.Catalog.Infrastructure.LogAudit.Dtos;

[ExcludeFromCodeCoverage]
public sealed class AuditoriaSqsRequest
{
    [JsonPropertyName("data_hora_operacao")]
    public DateTimeOffset DataHoraDaOperacao { get; set; }

    [JsonPropertyName("ip")]
    public string? Ip { get; set; }

    [JsonPropertyName("origem_operacao")]
    public string? OrigemDaOperacao { get; set; }

    [JsonPropertyName("tipo_operacao")]
    public string? TipoOperacao { get; set; }
}
