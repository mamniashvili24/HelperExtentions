using System;

namespace HelperExtentions
{
    public static class TryCatchExceptions
    {
        public static void TryCatchFinally<ExeptionType>(this Action tryAction, Action<ExeptionType> catchAction, Action finallyAction)
            where ExeptionType : Exception
        {
            try
            {
                tryAction();
            }
            catch (ExeptionType ex)
            {
                catchAction(ex);
            }
            finally
            {
                finallyAction();
            }
        }

        public static void TryCatchFinally(this Action tryAction, Action<Exception> catchAction, Action finallyAction)
        {
            TryCatchFinally<Exception>(tryAction, catchAction, finallyAction);
        }

        public static void TryCatch<ExeptionType>(this Action tryAction, Action<ExeptionType> catchAction)
            where ExeptionType : Exception
        {
            try
            {
                tryAction();
            }
            catch (ExeptionType ex)
            {
                catchAction(ex);
            }
        }

        public static void TryCatch(this Action tryAction, Action<Exception> catchAction)
        {
            TryCatch<Exception>(tryAction, catchAction);
        }

        public static void TryFinally(this Action tryAction, Action finallyAction)
        {
            try
            {
                tryAction();
            }
            finally
            {
                finallyAction();
            }
        }
    }
}