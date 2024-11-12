using System.ComponentModel;

namespace ConsoleApp
{
    internal enum OrderStatus
    {
        [Description("Новый заказ с RKeeper")]
        Received = 1,

        [Description("Заказ отправлен на RKeeper для закрытия")]
        WaitingForSaveInRK = 2,

        [Description("Заказ не обработан при закрытии на RK")]
        FailedInSaving = 3,

        [Description("Заказ закрыт на RKeeper")]
        SavedInRK = 4,
    }
}
