using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class HardwareChecker
    {
        private static readonly string[] _requiredComponents =
        {
        "Win32_Processor",
        "Win32_BIOS",
        "Win32_BaseBoard",
        "Win32_VideoController",
        "Win32_DiskDrive",
        "Win32_NetworkAdapter"
    };

        private static string HashingHardwareID(string ToHash)
        {
            try
            {
                byte[] KeyToHashWith = Encoding.ASCII.GetBytes("WhatDidYouDo");
                using (HMACSHA256 SHA256Hashing = new HMACSHA256(KeyToHashWith))
                {
                    var TheHash = SHA256Hashing.ComputeHash(Encoding.UTF8.GetBytes(ToHash));
                    return BitConverter.ToString(TheHash).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Hashing error: {ex.Message}");
            }
        }

        public static string GetHardwareID()
        {
            try
            {
                var hardwareInfo = new StringBuilder();

                // CPU Information
                var cpuQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject cpu in cpuQuery.Get())
                {
                    hardwareInfo.Append(cpu["ProcessorType"].ToString());
                    hardwareInfo.Append(cpu["ProcessorId"].ToString());
                    hardwareInfo.Append(cpu["NumberOfCores"].ToString());
                    hardwareInfo.Append(cpu["NumberOfLogicalProcessors"].ToString());
                }

                // BIOS Information
                var biosQuery = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (ManagementObject bios in biosQuery.Get())
                {
                    hardwareInfo.Append(bios["Manufacturer"].ToString());
                    hardwareInfo.Append(bios["Version"].ToString());
                    hardwareInfo.Append(bios["SerialNumber"].ToString());
                }



                // GPU Information
                var gpuQuery = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject gpu in gpuQuery.Get())
                {
                    hardwareInfo.Append(gpu["Name"].ToString());
                    hardwareInfo.Append(gpu["PNPDeviceID"].ToString());
                }

                // Disk Information
                var diskQuery = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject disk in diskQuery.Get())
                {
                    hardwareInfo.Append(disk["Model"].ToString());
                    hardwareInfo.Append(disk["SerialNumber"].ToString());
                    hardwareInfo.Append(disk["Size"].ToString());
                }

                // Network Adapter Information
                var netQuery = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
                foreach (ManagementObject net in netQuery.Get())
                {
                    if (net["MACAddress"] != null)
                    {
                        hardwareInfo.Append(net["MACAddress"].ToString());
                        hardwareInfo.Append(net["PNPDeviceID"].ToString());
                    }
                }

                return HashingHardwareID(hardwareInfo.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Hardware information collection error: {ex.Message}");
            }
        }

        public static bool ValidateHardwareID(string expectedID)
        {
            try
            {
                var currentID = GetHardwareID();
                return currentID == expectedID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Hardware validation error: {ex.Message}");
            }
        }

        public static Dictionary<string, string> GetDetailedHardwareInfo()
        {
            var hardwareInfo = new Dictionary<string, string>();

            try
            {
                // CPU Information
                var cpuQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject cpu in cpuQuery.Get())
                {
                    hardwareInfo["CPU_Type"] = cpu["ProcessorType"].ToString();
                    hardwareInfo["CPU_ID"] = cpu["ProcessorId"].ToString();
                    hardwareInfo["CPU_Cores"] = cpu["NumberOfCores"].ToString();
                    hardwareInfo["CPU_Threads"] = cpu["NumberOfLogicalProcessors"].ToString();
                }

                // BIOS Information
                var biosQuery = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (ManagementObject bios in biosQuery.Get())
                {
                    hardwareInfo["BIOS_Manufacturer"] = bios["Manufacturer"].ToString();
                    hardwareInfo["BIOS_Version"] = bios["Version"].ToString();
                    hardwareInfo["BIOS_Serial"] = bios["SerialNumber"].ToString();
                }


                // GPU Information
                var gpuQuery = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject gpu in gpuQuery.Get())
                {
                    hardwareInfo["GPU_Name"] = gpu["Name"].ToString();
                    hardwareInfo["GPU_ID"] = gpu["PNPDeviceID"].ToString();
                }

                // Disk Information
                var diskQuery = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject disk in diskQuery.Get())
                {
                    hardwareInfo["Disk_Model"] = disk["Model"].ToString();
                    hardwareInfo["Disk_Serial"] = disk["SerialNumber"].ToString();
                    hardwareInfo["Disk_Size"] = disk["Size"].ToString();
                }

                // Network Adapter Information
                var netQuery = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
                foreach (ManagementObject net in netQuery.Get())
                {
                    if (net["MACAddress"] != null)
                    {
                        hardwareInfo["Network_MAC"] = net["MACAddress"].ToString();
                        hardwareInfo["Network_ID"] = net["PNPDeviceID"].ToString();
                    }
                }

                return hardwareInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Detailed hardware information collection error: {ex.Message}");
            }
        }
    }
}
