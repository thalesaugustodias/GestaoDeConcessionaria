using NToastNotify;

namespace GestaoDeConcessionaria.Web.Extensions
{
   public static class ToastNotificationExtensions
{
    public static void AddSuccessToastMessageCustom(this IToastNotification toast, string message)
    {
        var options = new ToastrOptions
        {
            Title = "Sucesso"           
        };
        toast.AddSuccessToastMessage(message, options);
    }

    public static void AddErrorToastMessageCustom(this IToastNotification toast, string message)
    {
        var options = new ToastrOptions
        {
            Title = "Erro"            
        };
        toast.AddErrorToastMessage(message, options);
    }

    public static void AddWarningToastMessageCustom(this IToastNotification toast, string message)
    {
        var options = new ToastrOptions
        {
            Title = "Atenção"            
        };
        toast.AddWarningToastMessage(message, options);
    }

    public static void AddInfoToastMessageCustom(this IToastNotification toast, string message)
    {
        var options = new ToastrOptions
        {
            Title = "Informação"            
        };
        toast.AddInfoToastMessage(message, options);
    }

    public static void AddAlertToastMessageCustom(this IToastNotification toast, string message)
    {
        var options = new ToastrOptions
        {
            Title = "Alerta"           
        };
        toast.AddAlertToastMessage(message, options);
    }
}
}
