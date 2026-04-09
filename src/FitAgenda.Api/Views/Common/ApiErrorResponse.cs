namespace FitAgenda.Api.Views.Common;

public class ApiErrorResponse
{
    public bool Sucesso { get; set; }
    public IEnumerable<string> Erros { get; set; } = [];
}
