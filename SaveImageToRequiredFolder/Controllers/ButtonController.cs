using Microsoft.AspNetCore.Mvc;
using System;
using HidLibrary;

namespace SaveImageToRequiredFolder.Controllers2
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButtonController : ControllerBase
    {
        [HttpPost()]
        public IActionResult ListenToButtonPressed()
        {

            // Continuous loop to read keyboard input
               const int VendorId = 0x5131;
               const int ProductId = 0x2019;
               var device = HidDevices.Enumerate(VendorId, ProductId).FirstOrDefault();

               if (device == null)
               {
                   Console.WriteLine("Device not found.");
               }

               // Open the device
               device.OpenDevice();

               if (!device.IsConnected)
               {
                   Console.WriteLine("Failed to connect to the device.");
               }
               else
               {
                   Console.WriteLine("Device connected");
               }

               while (true)
               {
                   try
                   {
                       HidReport report = device.ReadReport();
                       Console.WriteLine(((int)report.ReadStatus));
                       if (report != null && report.Data.Length > 0)
                       {
                           //Console.WriteLine("Raw Data: " + BitConverter.ToString(report.Data));

                           byte buttonState = report.Data[0];
                           if (buttonState ==1)
                           {
                               Console.WriteLine("Button pressed");
                           }
                           else
                           {
                               Console.WriteLine("Button not pressed: State = " + buttonState);
                           }
                       }
                   }
                   catch (Exception ex)
                   {
                       Console.WriteLine("Error reading report: " + ex.Message);
                   }
               }
                      
            }
        }

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
    }