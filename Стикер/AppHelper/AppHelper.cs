using System.IO.IsolatedStorage;
using System.Windows.Media;

namespace Стикер
{
    static class AppHelper
    {
        static AppHelper()
        {
            Navigation = 0;
            ToRateCount = 0;
            Storage = IsolatedStorageSettings.ApplicationSettings;
        }

        
        public static IsolatedStorageSettings Storage { get; set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public static int Navigation { get; set; }
       
        /// <summary>
        /// Количество ответов для оценки
        /// </summary>
        public static sbyte ToRateCount { get; set; }

    }
}