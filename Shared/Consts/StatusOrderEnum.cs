namespace Shared.Consts;

public enum StatusOrderEnum
{
    Default,
    Solicitado,
    OrçamentoEnviado,
    OrçamentoAprovado,
    DataSelecionada,
    AguardandoPagamento,
    PagamentoConfirmado,
    EmExecução,
    Concluído,
    Cancelado,
    OrçamentoRejeitado
}

public class StatusOrderConst
{
    public const string Default = "Default";
    public const string Solicitado = "Solicitado";
    public const string OrçamentoEnviado = "Orçamento Enviado";
    public const string OrçamentoAprovado = "Orçamento Aprovado";
    public const string DataSelecionada = "Data Selecionada";
    public const string AguardandoPagamento = "Aguardando Pagamento";
    public const string PagamentoConfirmado = "Pagamento Confirmado";
    public const string EmExecução = "Em Execução";
    public const string Concluído = "Concluído";
    public const string Cancelado = "Cancelado";
    public const string OrçamentoRejeitado = "Orçamento Rejeitado";

    public static string GetNameWithType(StatusOrderEnum status)
    {
        return status switch
        {
            StatusOrderEnum.Default => Default,
            StatusOrderEnum.Solicitado => Solicitado,
            StatusOrderEnum.OrçamentoEnviado => OrçamentoEnviado,
            StatusOrderEnum.OrçamentoAprovado => OrçamentoAprovado,
            StatusOrderEnum.DataSelecionada => DataSelecionada,
            StatusOrderEnum.AguardandoPagamento => AguardandoPagamento,
            StatusOrderEnum.PagamentoConfirmado => PagamentoConfirmado,
            StatusOrderEnum.EmExecução => EmExecução,
            StatusOrderEnum.Concluído => Concluído,
            StatusOrderEnum.Cancelado => Cancelado,
            StatusOrderEnum.OrçamentoRejeitado => OrçamentoRejeitado,
            _ => Default
        };
    }
}