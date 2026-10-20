using System;
using MaintenanceSystem.Core;

namespace MaintenanceSystem.Entities
{
    // الفئة الفرعية الثالثة: ترث من الفئة المجردة
    public class ClimateControlModule : HardwareDevice
    {
        private double _horsePowerRating;
        private bool _inverterBoardMalfunction;

        public ClimateControlModule(string serialIdentifier, string modelName, string initialStatus, double horsePower, bool inverterBoardMalfunction)
            : base(serialIdentifier, modelName, initialStatus)
        {
            _horsePowerRating = horsePower;
            _inverterBoardMalfunction = inverterBoardMalfunction;
        }

        // تعدد الأشكال الخاص بالمكيف
        public override decimal EvaluateRepairTariff()
        {
            decimal diagnosticFee = 20000;
            decimal powerFactor = (decimal)(_horsePowerRating * 5000);
            decimal electronicCost = _inverterBoardMalfunction ? 90000 : 10000; // تنظيف فلاتر أو صيانة لوحة

            return diagnosticFee + powerFactor + electronicCost;
        }
    }
}
