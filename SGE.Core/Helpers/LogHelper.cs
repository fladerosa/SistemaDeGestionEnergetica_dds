using System;
using NLog;

namespace SGE.Core.Helpers
{
    public class LogHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Muestra un mensaje informativo
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void LogInformationMessage(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Muestra un mensaje de éxito
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void LogSuccessMessage(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Muestra un mensaje de advertencia
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void LogWarningMessage(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Muestra un mensaje de error
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void LogErrorMessage(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Muestra un mensaje de error
        /// </summary>
        /// <param name="message">Mensaje a mostrar</param>
        public static void LogErrorMessage(string message, Exception exception)
        {
            logger.Error(message, exception);
        }
    }
}
