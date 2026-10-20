using System;
using MaintenanceSystem.Core;
using MaintenanceSystem.Entities;
using MaintenanceSystem.Services;

namespace MaintenanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ASSET MAINTENANCE REGISTRY SYSTEM ===");

            // 1. Initialize notification service and event subscription
            NotificationService notifier = new NotificationService();
            notifier.OnRepairsCompleted += Notifier_OnRepairsCompleted;

            // 2. Initialize the central maintenance registry
            MaintenanceRegistry registry = new MaintenanceRegistry(notifier);

            Console.WriteLine("\n--- [STEP 1] Registering Hardware Devices ---");

            // Create testing instances with valid serial numbers (Pattern: SN-XXXX)
            HardwareDevice coolingUnit = new ThermalCoolingUnit("SN-202601", "Rotary Cooling V5", "In Progress", gasVolume: 2.5, compressorFailure: true);
            HardwareDevice laundrySystem = new HydroLaundrySystem("SN-202602", "Hydro Digital Laundry", "In Progress", drumCapacity: 9, drainPumpDefect: false);
            HardwareDevice climateModule = new ClimateControlModule("SN-202603", "Smart Inverter AC", "In Progress", horsePower: 2.0, inverterBoardMalfunction: true);

            // Create an instance with an invalid serial number to test the static validator
            HardwareDevice invalidDevice = new ThermalCoolingUnit("INVALID-123", "Demo Model", "In Progress", gasVolume: 1.0, compressorFailure: false);

            // Register valid assets into the system
            registry.RegisterDevice(coolingUnit);
            registry.RegisterDevice(laundrySystem);
            registry.RegisterDevice(climateModule);
            
            // Attempt to register the invalid asset (Should be rejected by SystemValidator)
            registry.RegisterDevice(invalidDevice);

            // 3. Display current system snapshot and evaluate aggregate costs via Polymorphism
            registry.DisplayRegistrySnapshot();

            decimal initialTotal = registry.CalculateAggregateRepairCosts();
            Console.WriteLine($"\n[Metric]: Total Estimated Repair Budget: {initialTotal:N0} SYP");
            Console.WriteLine($"[Metric]: Active Devices Currently Under Repair: {MaintenanceRegistry.GlobalActiveRepairsCount}");

            Console.WriteLine("\n--- [STEP 2] Updating Status & Triggering Events ---");

            // Initiate diagnostic for a specific asset (Interface method call)
            coolingUnit.InitiateDiagnosticProcess();

            // Update status to 'Completed' to trigger the status mutation event
            Console.WriteLine("\n[Update]: Finalizing maintenance for Hydro Laundry System...");
            registry.ModifyDeviceStatus("SN-202602", "Completed");

            // 4. Display final system snapshot after updates
            registry.DisplayRegistrySnapshot();
            Console.WriteLine($"\n[Metric]: Active Devices Currently Under Repair Post-Update: {MaintenanceRegistry.GlobalActiveRepairsCount}");

            Console.WriteLine("\n=======================================================");
            Console.WriteLine("System execution completed. All OOP principles tested successfully.");
            Console.ReadLine();
        }

        // Event handler execution via Delegate
        private static void Notifier_OnRepairsCompleted(HardwareDevice device, string previousStatus)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n📢 [SYSTEM EVENT]: Instant Notification Alert!");
            Console.WriteLine($"   Asset [{device.SerialIdentifier}] (Model: {device.DeviceModelName}) has been successfully repaired.");
            Console.WriteLine($"   Status Mutation: {previousStatus} -> {device.CurrentStatus}");
            Console.WriteLine($"   Final Evaluated Tariff: {device.EvaluateRepairTariff():N0} SYP");
            Console.ResetColor();
        }
    }
}
