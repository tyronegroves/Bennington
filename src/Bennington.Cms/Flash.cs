using System;

namespace Bennington.Cms
{
    public static class Flash
    {
        private static Action<string> notificationFunction;

        public static void SetNotificationFunction(Action<string> notificationFunction)
        {
            Flash.notificationFunction = notificationFunction;
        }

        public static void Notify(string value)
        {
            if (value != null)
                notificationFunction(value);
        }
    }
}