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

        public static void TryCatchThrow<ExeptionType>(this Action tryAction, Action catchAction)
            where ExeptionType : Exception
        {
            try
            {
                tryAction();
            }
            catch (ExeptionType)
            {
                catchAction();
                throw;
            }
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

        public static TReturnType TryCatch<TReturnType, TExeptionType>(this Func<TReturnType> tryFunc, Func<TExeptionType, TReturnType> catchFunc)
            where TExeptionType : Exception
        {
            TReturnType result;

            try
            {
                result = tryFunc();
            }
            catch (TExeptionType ex)
            {
                result = catchFunc(ex);
            }

            return result;
        }
    }
}