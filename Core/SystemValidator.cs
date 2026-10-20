using System;
using System.Text.RegularExpressions;

namespace MaintenanceSystem.Core
{
    // فئة ثابتة بالكامل تحتوي على دوال للتحقق من صحة البيانات المدخلة
    public static class SystemValidator
    {
        // خاصية ثابتة لحساب قيمة إجمالية أو عدد أجهزة معين (مثلاً لحساب حد الضمان المالي)
        public static decimal LuxuryTaxRate => 0.05m; 

        // دالة ثابتة للتحقق من صحة الرقم التسلسلي (مثلاً يجب أن يبدأ بـ SN ويتبعه أرقام)
        public static bool ValidateSerialIdentifier(string serial)
        {
            if (string.IsNullOrWhiteSpace(serial)) return false;
            return Regex.IsMatch(serial, @"^SN-\d{4,8}$");
        }

        // دالة ثابتة للتحقق من منطقية تاريخ التسجيل
        public static bool IsRegistrationDateValid(DateTime date)
        {
            return date <= DateTime.Now;
        }
    }
}
