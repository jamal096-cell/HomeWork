using System;

namespace MaintenanceSystem.Core
{
    // الواجهة الأساسية لإدارة الأصول القابلة للإصلاح (بديل IAppliance التقليدي)
    public interface IFixableAsset
    {
        // عمليات أساسية مشتركة بين جميع الأجهزة
        void InitiateDiagnosticProcess();
        void UpdateOperationStatus(string newStatus);
    }
}
