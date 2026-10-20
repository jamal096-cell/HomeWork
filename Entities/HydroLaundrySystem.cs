    using System;
using MaintenanceSystem.Core;

namespace MaintenanceSystem.Entities
{
    // الفئة الفرعية الثانية: ترث من الفئة المجردة
    public class HydroLaundrySystem : HardwareDevice
    {
        private int _drumCapacityKg;
        private bool _drainPumpDefect;

        public HydroLaundrySystem(string serialIdentifier, string modelName, string initialStatus, int drumCapacity, bool drainPumpDefect)
            : base(serialIdentifier, modelName, initialStatus)
        {
            _drumCapacityKg = drumCapacity;
            _drainPumpDefect = drainPumpDefect;
        }

        // تعدد الأشكال الخاص بالغسالة
        public override decimal EvaluateRepairTariff()
        {
            decimal setupFee = 12000;
            decimal capacitySurcharge = _drumCapacityKg * 1500; // رسوم إضافية حسب الحجم
            decimal pumpFixCost = _drainPumpDefect ? 35000 : 0;

            return setupFee + capacitySurcharge + pumpFixCost;
        }
    }
}
