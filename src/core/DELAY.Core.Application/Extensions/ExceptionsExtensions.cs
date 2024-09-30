using System.Text;

namespace DELAY.Core.Application.Extensions
{
    public static class ExceptionsExtensions
    {
        /// <summary>
        /// Форматированный вывод сообщения об ошибке
        /// </summary>
        /// <param name="exc">Исключение</param>
        /// <param name="addStackTrace">Включать в вывод трассировку стека</param>
        /// <returns></returns>
        public static string Format(this Exception exc, bool addStackTrace = true)
        {
            if (exc == null)
            {
                return string.Empty;
            }

            var stackTrace = new StringBuilder();

            if (addStackTrace && exc.StackTrace != null)
            {
                string[] tempTrace = exc.StackTrace.Split('\n');

                foreach (string trace in tempTrace)
                {
                    stackTrace.Append(trace);
                }
            }

            return exc.Message + (exc.InnerException != null ? " (" + exc.InnerException.Format() + ")" : "") + (stackTrace.Length > 0 ? ". Трассировки стека:" + stackTrace : "");
        }
    }
}
