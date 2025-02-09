using System.Diagnostics.CodeAnalysis;
using BookWise.Catalog.Infrastructure.LogAudit.Enums;

namespace BookWise.Catalog.Infrastructure.LogAudit.Dtos;

[ExcludeFromCodeCoverage]
public sealed class LogAuditCommand
{
    public AuditoriaOperacao Operacao { get; set; }

    public string? Descricao { get; set; }

    public string? ValorMinimo { get; set; }

    public string? ValorNovo { get; set; }

    public LogAuditCommand(AuditoriaOperacao operacao, string? descricao, string valorMinimo = "", string valorNovo = "")
    {
        Operacao = operacao;
        Descricao = descricao;
        ValorMinimo = valorMinimo;
        ValorNovo = valorNovo;
    }
}
