//using System;
//using System.Linq;
//using HidLibrary;

//class Program
//{
//    // Replace these with your device's vendor ID and product ID
//    const int VendorId = 0x5131;  // Example Vendor ID
//    const int ProductId = 0x2019; // Example Product ID

//    static void Main(string[] args)
//    {
//        try
//        {
//            // Find the device
//            var device = HidDevices.Enumerate(VendorId, ProductId).FirstOrDefault();

//            if (device == null)
//            {
//                Console.WriteLine("Device not found.");
//                return;
//            }

//            // Open the device
//            device.OpenDevice();

//            if (!device.IsConnected)
//            {
//                Console.WriteLine("Failed to connect to the device.");
//                return;
//            }
//            else
//            {
//                Console.WriteLine("Device connected");
//            }

//            // Read reports
//            while (true)
//            {
//                try
//                {
//                    HidReport report = device.ReadReport();
//                    Console.WriteLine(((int)report.ReadStatus));
//                    if (report != null && report.Data.Length > 0)
//                    {
//                        //Console.WriteLine("Raw Data: " + BitConverter.ToString(report.Data));

//                        byte buttonState = report.Data[0];
//                        if (buttonState == 1)
//                        {
//                            Console.WriteLine("Button pressed");
//                        }
//                        else
//                        {
//                            Console.WriteLine("Button not pressed: State = " + buttonState);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Error reading report: " + ex.Message);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Error: " + ex.Message);
//        }
//    }
//}



using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Press the '2' key to simulate button press.");

        // Continuous loop to read keyboard input
        while (true)
        {
            // Read a key press from the console
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // Check if the pressed key is '2'
            if (keyInfo.KeyChar == '2')
            {
                Console.WriteLine("Button pressed");
            }
        }
    }
}




//using System;
//using System.Linq;
//using HidLibrary;

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Find your HID device using its VID and PID
//        HidDevice device = HidDevices.Enumerate(0x5131, 0x2019).FirstOrDefault();

//        if (device != null)
//        {
//            device.OpenDevice();
//            Console.WriteLine("Device opened. Monitoring for button presses...");

//            // Start a loop to continuously read reports
//            while (true)
//            {
//                // Read a report
//                HidDeviceData data = device.Read(1000);
//                Console.WriteLine(device.);

//                if (data.Status == HidDeviceData.ReadStatus.Success)
//                {
//                    // Print the raw data as a string of hexadecimal values
//                    if (data.Data != null && data.Data.Length > 0)
//                    {
//                        string hexString = BitConverter.ToString(data.Data);
//                        //Console.WriteLine(hexString);

//                        // Check if the data indicates a button press
//                        if (data.Data[0] != 0) // Replace 0x02 with the actual value that indicates a button press
//                        {
//                            Console.WriteLine("Button was pressed!");
//                        }
//                    }
//                }
//                else
//                {
//                    Console.WriteLine("Failed to read data or no data available.");
//                }
//            }

//            device.CloseDevice();
//        }
//        else
//        {
//            Console.WriteLine("Device not found.");
//        }
//    }
//}



//using System;
//using System.Linq;
//using HidLibrary;

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Find your HID device using its VID and PID
//        HidDevice device = HidDevices.Enumerate(0x5131, 0x2019).FirstOrDefault();

//        if (device != null)
//        {
//            device.OpenDevice();

//            // Start an infinite loop to continuously read reports
//            while (true)
//            {
//                // Read a report
//                HidReport report = device.ReadReport();
//                Console.WriteLine(BitConverter.ToString(report.Data));
//                // Check if the button was pressed
//                if (report.Data != null && report.Data.Length > 0 && report.Data[0] == 01)
//                {
//                    Console.WriteLine("Button was pressed!");
//                }
//            }
//        }
//        else
//        {
//            Console.WriteLine("Device not found.");
//        }
//    }
//}
