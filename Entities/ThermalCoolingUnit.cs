using System;
using MaintenanceSystem.Core;

namespace MaintenanceSystem.Entities
{
    // الفئة الفرعية الأولى: ترث من الفئة المجردة (Inheritance)
    public class ThermalCoolingUnit : HardwareDevice
    {
        // حقول خاصة فريدة بهذا النوع من الأجهزة (التغليف)
        private double _refrigerantGasVolume; 
        private bool _compressorFailure;

        public ThermalCoolingUnit(string serialIdentifier, string modelName, string initialStatus, double gasVolume, bool compressorFailure) 
            : base(serialIdentifier, modelName, initialStatus)
        {
            _refrigerantGasVolume = gasVolume;
            _compressorFailure = compressorFailure;
        }

        // تعدد الأشكال: إعادة تعريف الدالة المجردة بطريقة خاصة (Polymorphism)
        public override decimal EvaluateRepairTariff()
        {
            decimal baseCost = 15000; // تكلفة الفحص الثابتة
            decimal gasCost = (decimal)(_refrigerantGasVolume * 2500); // تكلفة الغاز بناءً على الحجم
            decimal componentCost = _compressorFailure ? 75000 : 0; // تكلفة الضاغط إن كان تالفاً

            return baseCost + gasCost + componentCost;
        }
    }
}
