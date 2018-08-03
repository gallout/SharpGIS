using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Core.Spectrum.Domain.Enums
{
    /// <summary>
    /// Статус объекта.
    /// </summary>
    [Serializable]
    public enum VeloObjectStatus
    {
        /// <summary>
        /// Установлен.
        /// </summary>
        Set,

        /// <summary>
        /// Требуется.
        /// </summary>
        Required,

        /// <summary>
        /// Демонтировать.
        /// </summary>
        Dismantle,

        /// <summary>
        /// Мобильный.
        /// </summary>
        Mobile,

        /// <summary>
        /// Праздничный
        /// </summary>
        Celebrate

    }

    public class VeloObjectStatusStrings
    {
        public static Dictionary<VeloObjectStatus, string> Strings =
            new Dictionary<VeloObjectStatus, string> {
                { VeloObjectStatus.Set, "Установлена" },
                { VeloObjectStatus.Required, "Требуется" },
                { VeloObjectStatus.Dismantle, "Демонтирована" },
                { VeloObjectStatus.Mobile, "Мобильная" },
                { VeloObjectStatus.Celebrate, "Праздничная" }
            };

        public static string GetStatusName(VeloObjectStatus status)
        {
            return Strings[status];
        }

        public static VeloObjectStatus GetStatusByName(string name)
        {
            return Strings.FirstOrDefault(s => s.Value == name).Key;
        }
    }
}
