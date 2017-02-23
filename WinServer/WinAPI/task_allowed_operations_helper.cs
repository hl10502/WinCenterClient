namespace WinAPI
{
    using System;

    public static class task_allowed_operations_helper
    {
        public static string ToString(task_allowed_operations x)
        {
            if (x == task_allowed_operations.cancel)
            {
                return "cancel";
            }
            return "unknown";
        }
    }
}

