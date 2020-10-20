using System;

namespace Reservations.Common.Shared
{
    public class Check
    {
        public static void NotNull<T>(T value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
