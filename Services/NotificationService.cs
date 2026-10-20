using System;
using MaintenanceSystem.Core;

namespace MaintenanceSystem.Services
{
    // 1. تعريف التفويض المناسب للإشعار بتغير حالة الجهاز (بديل الأسلوب التقليدي)
    public delegate void AssetStatusMutationHandler(HardwareDevice device, string previousStatus);

    public class NotificationService
    {
        // 2. إطلاق حدث داخل النظام عند تحقق شرط محدد (مثل اكتمال الإصلاح)
        public event AssetStatusMutationHandler OnRepairsCompleted;

        // دالة لإطلاق الحدث عند اكتمال الإصلاح
        public void TriggerStatusAlert(HardwareDevice device, string oldStatus)
        {
            // الشرط المحدد: إذا تحولت الحالة إلى "Completed" أو "جاهز للتسليم"
            if (device.CurrentStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase) && OnRepairsCompleted != null)
            {
                OnRepairsCompleted(device, oldStatus);
            }
        }
    }
}
