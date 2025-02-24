using NToastNotify;

namespace GestaoDeConcessionaria.Web.Extensions
{
    public static class ToastNotificationExtensions
    {
        public static void AddSuccessToastMessageCustom(this IToastNotification toast, string message)
        {
            var options = new ToastrOptions
            {
                Title = "Sucesso",
                IconClass = "fa fa-check-circle",
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                CloseButton = true
            };
            toast.AddSuccessToastMessage(message, options);
        }

        public static void AddErrorToastMessageCustom(this IToastNotification toast, string message)
        {
            var options = new ToastrOptions
            {
                Title = "Erro",
                IconClass = "fa fa-times-circle",
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                CloseButton = true
            };
            toast.AddErrorToastMessage(message, options);
        }

        public static void AddWarningToastMessageCustom(this IToastNotification toast, string message)
        {
            var options = new ToastrOptions
            {
                Title = "Atenção",
                IconClass = "fa fa-exclamation-triangle",
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                CloseButton = true
            };
            toast.AddWarningToastMessage(message, options);
        }

        public static void AddInfoToastMessageCustom(this IToastNotification toast, string message)
        {
            var options = new ToastrOptions
            {
                Title = "Informação",
                IconClass = "fa fa-info-circle",
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                CloseButton = true
            };
            toast.AddInfoToastMessage(message, options);
        }

        public static void AddAlertToastMessageCustom(this IToastNotification toast, string message)
        {
            var options = new ToastrOptions
            {
                Title = "Alerta",
                IconClass = "fa fa-exclamation-circle",
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                CloseButton = true
            };
            toast.AddAlertToastMessage(message, options);
        }
    }
}
