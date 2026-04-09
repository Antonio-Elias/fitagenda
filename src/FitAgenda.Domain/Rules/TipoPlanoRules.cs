using FitAgenda.Domain.Enums;

namespace FitAgenda.Domain.Rules;

public static class TipoPlanoRules
{
    public static int ObterLimitePlano(TipoPlano plano)
    {
        return plano switch
        {
            TipoPlano.Mensal => 12,
            TipoPlano.Trimestral => 20,
            TipoPlano.Anual => 30,
            _ => throw new ArgumentOutOfRangeException(nameof(plano), "Tipo de plano desconhecido.")
        };
    }
}
