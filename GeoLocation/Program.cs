using System;
using System.Device.Location;

/**
 * Note: -
 * 1. We need to have the locatin access permissions enabled in our respective device setings.
 * 2. We also need a reference to the System.Device library for the GeoCoordinate Watcher.
 */

namespace GeoLocation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting GeoCoordinate Wacther...");

            // Use the gecoordinate watcher
            var watcher = new GeoCoordinateWatcher();

            // Determines whether the gecoordinate watcher object has been initialized or started
            watcher.StatusChanged += (s, e) =>
            {
                // Here e is the event informer
                Console.WriteLine($"GeoCoordinateWatcher:StatusChanged:{e.Status}");
            };

            // Determines the position of the device and updates accordingly
            watcher.PositionChanged += (s, e) =>
            {
                Console.WriteLine($"GeoCoordinateWatcher:PositionChanged:{e.Position.Location}");
            };

            // This is the threshold with respect to initial or previous position after whic we ask for the latest position
            watcher.MovementThreshold = 100; // meters

            watcher.Start();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
