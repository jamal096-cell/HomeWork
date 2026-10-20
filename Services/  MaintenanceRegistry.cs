using System;
using System.Collections.Generic;
using MaintenanceSystem.Core;

namespace MaintenanceSystem.Services
{
    public class MaintenanceRegistry
    {
        private readonly List<HardwareDevice> _trackedDevices;
        private readonly NotificationService _notificationService;

        // Static property to track globally active repairs count
        public static int GlobalActiveRepairsCount { get; private set; }

        public MaintenanceRegistry(NotificationService notificationService)
        {
            _trackedDevices = new List<HardwareDevice>();
            _notificationService = notificationService;
        }

        // Clean English implementation for registering a device
        public void RegisterDevice(HardwareDevice device)
        {
            // Validating via the static validator class
            if (!SystemValidator.ValidateSerialIdentifier(device.SerialIdentifier))
            {
                Console.WriteLine($"[Error]: Serial identifier {device.SerialIdentifier} is invalid! Must match SN-XXXX format.");
                return;
            }

            _trackedDevices.Add(device);
            GlobalActiveRepairsCount++; // Increment the static counter
            Console.WriteLine($"[Registry]: Asset {device.SerialIdentifier} has been successfully registered.");
        }

        // Method to mutate device status and trigger notification event
        public void ModifyDeviceStatus(string serial, string newStatus)
        {
            var device = _trackedDevices.Find(d => d.SerialIdentifier == serial);
            if (device != null)
            {
                string oldStatus = device.CurrentStatus;
                device.UpdateOperationStatus(newStatus);

                // Trigger the alert system via notification service
                _notificationService.TriggerStatusAlert(device, oldStatus);
                
                if (newStatus.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                {
                    GlobalActiveRepairsCount--; // Decrement active count upon completion
                }
            }
        }

        // Polymorphic cost aggregation
        public decimal CalculateAggregateRepairCosts()
        {
            decimal totalAccumulatedCost = 0;
            foreach (var device in _trackedDevices)
            {
                totalAccumulatedCost += device.EvaluateRepairTariff();
            }
            return totalAccumulatedCost;
        }

        // Clean English implementation for displaying data snapshot
        public void DisplayRegistrySnapshot()
        {
            Console.WriteLine("\n--- Current Maintenance Registry Snapshot ---");
            foreach (var device in _trackedDevices)
            {
                Console.WriteLine($"Asset: {device.DeviceModelName} | ID: {device.SerialIdentifier} | Status: {device.CurrentStatus} | Cost: {device.EvaluateRepairTariff():N0} SYP");
            }
        }
    }
}
