using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Core.Domain;
using ITS.Core.Spectrum.Domain.Enums;
using ITS.Core.Domain.FeatureObjects;
using ITS.Core.Enums;

namespace ITS.Core.Spectrum.Domain
{
    [Serializable]
    public class VeloObject : DomainObject<long>
    {
        #region Fields

        private VeloType _veloType;
        private VeloView _veloView;
        private VeloObjectStatus _veloObjectStatus;
        private float _veloLength;
        private float _veloWidth;
        private int _veloSection;
        private DateTime? _dataSet;
        private DateTime? _dataCheck;
        private double _angle;
        
        

        #endregion

        #region Constructors

        public VeloObject() { }

        /*    public VeloObject(VeloType veloType, VeloView veloView, int veloSection , float veloLength, float veloWidth,
                DateTime dataSet, DateTime dataCheck, string veloOrg)
            {
                VeloSection = veloSection; //количество секций
                VeloType = veloType; // тип велопарковки
                VeloView = veloView; // вид велопарковки
                VeloLength = veloLength; // длина велопарковки
                VeloWidth = veloWidth; // ширина велопарковки
                DataSet = dataSet; // дата установки 
                DataCheck = dataCheck; //дата обслуживания
            } */

        #endregion

        #region Data Properties
        public string TypeAsString
        {
            get { return VeloTypeStrings.GetString(VeloType); }
        }

        /// <summary>
        /// Вид.
        /// </summary>
        public VeloView VeloView
        {
            get { return _veloView; }
            set { _veloView = value; }
        }

        public string ViewAsString
        {
            get { return VeloViewStrings.GetString(VeloView); }
        }

        public string StatusAsString
        {
            get { return VeloObjectStatusStrings.GetStatusName(VeloObjectStatus); }
        }



        /// <summary>
        /// Тип.
        /// </summary>
        public VeloType VeloType
        {
            get { return _veloType; }
            set { _veloType = value; }
        }
        


        /// <summary>
        /// Статус объекта.
        /// </summary>
        public VeloObjectStatus VeloObjectStatus
        {
            get { return _veloObjectStatus; }
            set { _veloObjectStatus = value; }
        }

        /// <summary>
        /// Длина.
        /// </summary>
        public float VeloLength
        {
            get { return _veloLength; }
            set { _veloLength = value; }
        }

        /// <summary>
        /// Ширина.
        /// </summary>
        public float VeloWidth
        {
            get { return _veloWidth; }
            set { _veloWidth = value; }
        }

        /// <summary>
        /// Количество секций.
        /// </summary>
        public int VeloSection
        {
            get { return _veloSection; }
            set { _veloSection = value; }
        }

        /// <summary>
        /// Дата установки.
        /// </summary>
        public DateTime? DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        /// <summary>
        /// Дата обслуживания.
        /// </summary>
        public DateTime? DataCheck
        {
            get { return _dataCheck; }
            set { _dataCheck = value; }
        }
        /// <summary>
        /// Угол.
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }


        /// <summary>
        /// Геометрия
        /// </summary>
        public FeatureObject FeatureObject { get; set; }

        #endregion
        
        #region Display Properties
        
        

        #endregion
        
        // Верояно, нужен для копирования объекта по щелчку кнопки на панели. Тогда непонятно, почему он описан иенно здесь.
        public VeloObject GetCopy()
        {
            var newVeloObject = new VeloObject
            {
                VeloLength = this.VeloLength,
                VeloWidth = this.VeloWidth,
                VeloSection = this.VeloSection,
                DataSet = this.DataSet,
                DataCheck = this.DataCheck,
                VeloType = this.VeloType,
                VeloView = this.VeloView,
                VeloObjectStatus = this.VeloObjectStatus,
                Angle=this.Angle
       
                   //не копируем адрес и геометрию
        };
            return newVeloObject;
        }

        #region Overrides of DomainObject<long>

        public override void CopyFrom(DomainObject<long> T)
        {
            var velotype = T as VeloObject;
            if (velotype == null) return;

            VeloLength = velotype.VeloLength;
            VeloWidth = velotype.VeloWidth;
            VeloSection = velotype.VeloSection;
            DataSet = velotype.DataSet;
            DataCheck = velotype.DataCheck;
            VeloView = velotype.VeloView;
            VeloType = velotype.VeloType;
            VeloObjectStatus = velotype.VeloObjectStatus;
            FeatureObject = velotype.FeatureObject;
            Angle = velotype.Angle;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 101;
                hash += hash * 31 + VeloSection.GetHashCode();
                hash += hash * 31 + VeloType.GetHashCode();
                hash += hash * 31 + VeloView.GetHashCode();
                hash += hash * 31 + VeloObjectStatus.GetHashCode();
                hash += hash * 31 + VeloLength.GetHashCode();
                hash += hash * 31 + VeloWidth.GetHashCode();
                hash += hash * 31 + DataSet.GetHashCode();
                hash += hash * 31 + DataCheck.GetHashCode();
                hash += hash * 31 + Angle.GetHashCode();
                hash += hash * 31 + (FeatureObject != null ? FeatureObject.GetHashCode() : string.Empty.GetHashCode());
                return hash;
            }
        }

        public override string ToString()
        {
            string sFeature = "Объект";
            if (FeatureObject != null)
            {
                if (FeatureObject.Geometry.GeometryType == "Point")
                    sFeature = "Точечный объект";
             //   else if (FeatureObject.Geometry.GeometryType == "LineString")
             //       sFeature = "Линейный объект";
             //   else if (FeatureObject.Geometry.GeometryType == "Polygon")
             //       sFeature = "Полигональный объект";
            }
            return string.Format("{0}, {1}, VeloSection = {2}, Тип {3}", sFeature, StatusAsString, VeloSection, TypeAsString);
        }

        #endregion
    }
}
