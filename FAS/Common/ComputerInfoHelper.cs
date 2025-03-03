using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace FAS.Common
{
    public class ComputerInfoHelper
    {
        public static string ComputerInfo()
        {
            string info = string.Empty;
            string cpu = GetCPUInfo();
            string baseBoard = GetBaseBoardInfo();
            string bios = GetBIOSInfo();
            string mac = GetMACInfo();
            //string datetime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            info = string.Concat(cpu, baseBoard, bios, mac);
            return info;
        }

        private static string GetCPUInfo()
        {
            string info = string.Empty;
            info = GetHardWareInfo("Win32_Processor", "ProcessorId");
            return info;
        }

        private static string GetBaseBoardInfo()
        {
            string info = string.Empty;
            info = GetHardWareInfo("Win32_BaseBoard", "SerialNumber");
            return info;
        }

        private static string GetBIOSInfo()
        {
            string info = string.Empty;
            info = GetHardWareInfo("Win32_BIOS", "SerialNumber");
            return info;
        }

        private static string GetMACInfo()
        {
            string info = string.Empty;
            info = GetMacAddressByNetwork();
            return info;
        }

        private static string GetHardWareInfo(string typePath, string Key)
        {
            try
            {
                ManagementClass mageClass = new ManagementClass(typePath);
                ManagementObjectCollection mageObjectColl = mageClass.GetInstances();
                PropertyDataCollection Properties = mageClass.Properties;
                foreach (PropertyData property in Properties)
                {
                    if (property.Name == Key)
                    {
                        foreach (ManagementObject mageObject in mageObjectColl)
                        {
                            return mageObject.Properties[property.Name].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return string.Empty;
        }

        private static string GetMacAddressByNetwork()
        {
            string Key = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\";
            string macAddress = string.Empty;
            try
            {
                NetworkInterface[] Nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in Nics)
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet && adapter.GetPhysicalAddress().ToString().Length != 0)
                    {
                        string fRegistryKey = Key + adapter.Id + "\\Connection";
                        RegistryKey rKey = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rKey != null)
                        {
                            string fPnpInstanceID = rKey.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rKey.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 && fPnpInstanceID.Substring(0, 3) == "PCI")
                            {
                                macAddress = adapter.GetPhysicalAddress().ToString();
                                for (int i = 1; i < 6; i++)
                                {
                                    macAddress = macAddress.Insert(3 * i - 1, ":");
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return macAddress;
        }
    }
}
