namespace DELAY.Core.Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperTrim(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));

            return str.Trim().ToUpper();
        }

        /// <summary>
        /// Срез двух первых значений интервала
        /// </summary>
        /// <param name="str">Строковое представление интервала значений</param>
        /// <param name="divisor">Делитель</param>
        /// <returns>Массив из двух первых элементов</returns>
        public static string[] SliceCouple(this string str, string divisor = ";")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return ["", ""];
            }

            var arr = str.Split(divisor);
            if (arr.Length > 1)
            {
                return [arr[0], arr[1]];
            }
            else
            {
                return [arr[0], ""];
            }
        }
        /// <summary>
        /// Срез значений интервала
        /// </summary>
        /// <param name="str">Строковое представление интервала значений</param>
        /// <param name="divisor">Делитель</param>
        /// <param name="count">Кол-во элементов</param>
        /// <returns>Массив среза значений элементов размером count</returns>
        public static string[] Slice(this string str, string divisor = ";", int count = 2)
        {
            if (count == 0) return [""];

            if (string.IsNullOrWhiteSpace(str))
            {
                return ["", ""];
            }
            var arr = str.Split(divisor);
            var res = new string[count];
            var bound = count > arr.Length ? arr.Length : count;

            for (int i = 0; i < bound; i++)
            {
                res[i] = arr[i];
            }
            return res;
        }
    }
}
