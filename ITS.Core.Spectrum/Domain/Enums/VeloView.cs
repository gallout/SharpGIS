using System;
namespace ITS.Core.Spectrum.Domain.Enums
{
    [Serializable]
    public enum VeloView
    {
        None = 0,
        View1 = 1,
        View2 = 2

    }

    public static class VeloViewStrings
    {
        private static readonly string STR_None = "Не указан";
        private static readonly string STR_View1 = "Краткосрочные";
        private static readonly string STR_View2 = "Долгосрочные";

        /// <summary>
        /// Возвращает значение перечисления, соответствующее заданной строке описания.
        /// </summary>
        /// <param name="name">Строка описания типа.</param>
        /// <returns>Соответствующий тип.</returns>
        public static VeloView GetValue(string name)
        {
            if (name == STR_None)
                return VeloView.None;
            if (name == STR_View1)
                return VeloView.View1;
            if (name == STR_View2)
                return VeloView.View2;

            return VeloView.None;
        }
        /// <summary>
        /// Возвращает строку описания, соответствующую заданному значению перечисления.
        /// </summary>
        /// <param name="stationType">Предоставленное значение перечисления.</param>
        /// <returns>Соответствующий строка описания.</returns>
        public static string GetString(VeloView veloView)
        {
            switch (veloView)
            {
                case VeloView.View1:
                    return STR_View1;
                case VeloView.View2:
                    return STR_View2;

                case VeloView.None:
                default:
                    return STR_None;
            }
        }
    }
}
