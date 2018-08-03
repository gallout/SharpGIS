using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Core.Spectrum.Domain.Enums
{
    [Serializable]
    public enum VeloType
    {
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3,
        Type5 = 4,
        Type6 = 5,
        Type7 = 6,
        Type8 = 7,
        None = 8
    }

    public static class VeloTypeStrings
    {
        private static readonly string STR_None = "Не указан";
        private static readonly string STR_Type1 = "Горизонтальные";
        private static readonly string STR_Type2 = "Вертикальные";
        private static readonly string STR_Type3 = "П-образные";
        private static readonly string STR_Type4 = "Двухуровневые";
        private static readonly string STR_Type5 = "Велосипедная клетка";
        private static readonly string STR_Type6 = "Запирающиеся шкафчики";
        private static readonly string STR_Type7 = "Подвесная";
        private static readonly string STR_Type8 = "Дизайнерское решение";
        

        /// <summary>
        /// Возвращает значение перечисления, соответствующее заданной строке описания.
        /// </summary>
        /// <param name="name">Строка описания типа.</param>
        /// <returns>Соответствующий тип.</returns>
        public static VeloType GetValue(string name)
        {
            if (name == STR_Type1)
                return VeloType.Type1;
            if (name == STR_None)
                return VeloType.None;
            if (name == STR_Type2)
                return VeloType.Type2;
            if (name == STR_Type3)
                return VeloType.Type3;
            if (name == STR_Type4)
                return VeloType.Type4;
            if (name == STR_Type5)
                return VeloType.Type5;
            if (name == STR_Type6)
                return VeloType.Type6;
            if (name == STR_Type7)
                return VeloType.Type7;
            if (name == STR_Type8)
                return VeloType.Type8;

            return VeloType.None;
        }
        /// <summary>
        /// Возвращает строку описания, соответствующую заданному значению перечисления.
        /// </summary>
        /// <param name="stationType">Предоставленное значение перечисления.</param>
        /// <returns>Соответствующий строка описания.</returns>
        public static string GetString(VeloType veloType)
        {
            switch (veloType)
            {
                case VeloType.Type1:
                    return STR_Type1;
                case VeloType.Type2:
                    return STR_Type2;
                case VeloType.Type3:
                    return STR_Type3;
                case VeloType.Type4:
                    return STR_Type4;
                case VeloType.Type5:
                    return STR_Type5;
                case VeloType.Type6:
                    return STR_Type6;
                case VeloType.Type7:
                    return STR_Type7;
                case VeloType.Type8:
                    return STR_Type8;

                case VeloType.None:
                default:
                    return STR_None;
            }
        }
    }
}
