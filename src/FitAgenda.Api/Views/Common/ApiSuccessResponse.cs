namespace FitAgenda.Api.Views.Common;

public class ApiSuccessResponse<T>
{
    public bool Sucesso { get; set; }
    public T? Dados { get; set; }
}
