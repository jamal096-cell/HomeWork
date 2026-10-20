using System;

namespace MaintenanceSystem.Core
{
    // الفئة المجردة التي تمثل الجهاز العتادي وتطبق الواجهة الأساسية
    public abstract class HardwareDevice : IFixableAsset
    {
        // 1. الحقول الخاصة (Encapsulation) - جميع الحقول خاصة تماماً
        private readonly string _serialIdentifier;
        private readonly DateTime _registrationDate;
        private string _currentStatus;
        private string _deviceModelName;

        // 2. الخصائص للتحكم بالوصول إلى البيانات
        // رقم الجهاز وتاريخ التسجيل للقراءة فقط بعد إنشاء الكائن (شرط إلزامي في الوظيفة)
        public string SerialIdentifier => _serialIdentifier;
        public DateTime RegistrationDate => _registrationDate;

        public string CurrentStatus
        {
            get => _currentStatus;
            set => _currentStatus = value; // تسمح بتعديل حالة الصيانة لاحقاً
        }

        public string DeviceModelName
        {
            get => _deviceModelName;
            set => _deviceModelName = value;
        }

        // 3. المشيد (Constructor) لتهيئة القيم الأساسية والحساسة عند الإنشاء
        protected HardwareDevice(string serialIdentifier, string modelName, string initialStatus)
        {
            _serialIdentifier = serialIdentifier;
            _deviceModelName = modelName;
            _currentStatus = initialStatus;
            _registrationDate = DateTime.Now; // تسجيل التاريخ تلقائياً عند الدخول للمركز
        }

        public virtual void InitiateDiagnosticProcess()
        {
            Console.WriteLine($"[Diagnostic]: Initiating inspection for Serial: {_serialIdentifier}");
        }

        public virtual void UpdateOperationStatus(string newStatus)
        {
            _currentStatus = newStatus;
            Console.WriteLine($"[Update]: Device {_serialIdentifier} status changed to: {_currentStatus}");
        }


        // 5. الدالة المجردة الإلزامية لحساب تكلفة الإصلاح (Abstract Method)
        // ستعيد كل فئة فرعية تعريفها بطريقتها الخاصة (Polymorphism لاحقاً)
        public abstract decimal EvaluateRepairTariff();
    }
}
