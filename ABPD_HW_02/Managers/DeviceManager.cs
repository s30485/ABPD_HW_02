using System.Text.RegularExpressions;
using ABPD_HW_02.Models;

namespace ABPD_HW_02.Managers;

public class DeviceManager
{
    private readonly List<Device> _devices = new();
    private readonly string _filePath;
    private const int MaxDevices = 15; //max count is 15
    
    public DeviceManager(string filePath)
    {
        _filePath = filePath;
        LoadDevices();
    }
    
    private void LoadDevices()
    {
        if (!File.Exists(_filePath)) return;//check if file exists
        foreach (var line in File.ReadLines(_filePath)) //built in static class File 
        {
            try
            {
                var parts = line.Split(',');
                if (parts.Length < 3) continue;
                
                var type = parts[0];
                if (!int.TryParse(parts[1].Replace("SW-", "").Replace("P-", "").Replace("ED-", ""), out int id)) continue; 
                //TryParse() attempts to convert a string into an integer.
                //used out int because it allows the method to return a value via a parameter, not sure if its 100% needed though
                var name = parts[2];
                
                Device device = null;
                //this massive if statement parses the recorded devices to a list of devices
                if (type == "SW" && parts.Length == 4 && int.TryParse(parts[3], out int battery))
                {
                    device = new Smartwatch { Id = id, Name = name, BatteryPercentage = battery };
                }
                else if (type == "P")
                {
                    string os = parts.Length == 4 ? parts[3] : null;
                    device = new PersonalComputer { Id = id, Name = name, OperatingSystem = os };
                }
                else if (type == "ED" && parts.Length == 5 && Regex.IsMatch(parts[3], @"^(\d{1,3}\.){3}\d{1,3}$"))
                {
                    device = new EmbeddedDevice { Id = id, Name = name, IpAddress = parts[3], NetworkName = parts[4] };
                }
                
                if (device != null && _devices.Count < MaxDevices) //if wrong line -> ignore
                {
                    _devices.Add(device);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing line: \"{line}\". Exception: {ex.Message}");
            }
        }
    }

    public void AddDevice(Device device)
    {
        if (_devices.Count >= MaxDevices) throw new InvalidOperationException("Device storage is full.");
        _devices.Add(device);
    }

    public void RemoveDevice(int id) => _devices.RemoveAll(d => d.Id == id);

    public void EditDeviceData(int id, string property, object newValue) //I didn't find the materials in teams so I used youtube to learn boxing and unboxing, I don't know if its correctly implemented though
    //If i understood correctly, boxing is turning a value type into an object type variable
    //unboxing is the opposite, so transforming a reference object type variable to a value type
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"No device found with ID {id}.");
            return;
        }

        try
        {
            //used boxing by passing in object newValue. Then unbox when setting the property.
            if (device is Smartwatch sw && property.Equals("Battery"))
            {
                //newValue is expected to be an int
                int battery = (int)newValue; //unboxing
                sw.BatteryPercentage = battery;
            }
            else if (device is PersonalComputer pc && property.Equals("OS"))
            {
                //newValue is expected to be a string
                string osValue = (string)newValue; //unboxing
                pc.OperatingSystem = osValue;
            }
            else if (device is EmbeddedDevice ed)
            {
                //possibly editing IP or NetworkName, depending on property
                if (property.Equals("IPAddress"))
                {
                    string ip = (string)newValue; //unboxing
                    ed.IpAddress = ip;
                }
                else if (property.Equals("NetworkName"))
                {
                    string netName = (string)newValue;
                    ed.NetworkName = netName;
                }
            }
            else
            {
                Console.WriteLine($"Property '{property}' not recognized or not applicable for device with ID {id}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error editing device data (ID={id}). Reason: {ex.Message}");
        }
    }
    public void TurnOnDevice(int id) //turn on device with a given id
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"No device found with ID {id}.");
            return;
        }
        try
        {
            device.TurnOn();
        }
        catch (Exception ex)
        {
            //catch any exception thrown during turn on
            Console.WriteLine($"Could not turn on device (ID={id}). Reason: {ex.Message}");
        }
    }
    public void TurnOffDevice(int id) //turn off device with a given id
    {
        var device = _devices.FirstOrDefault(d => d.Id == id);
        if (device == null)
        {
            Console.WriteLine($"No device found with ID {id}.");
            return;
        }
        try
        {
            device.TurnOff();
        }
        catch (Exception ex)
        {
            //generally turning off doesn't throw exceptions, but just in case
            Console.WriteLine($"Could not turn off device (ID={id}). Reason: {ex.Message}");
        }
    }
    public void ShowAllDevices()
    {
        foreach (var device in _devices) Console.WriteLine(device);
    }
    
    public void SaveDevicesData()
    {
        var lines = _devices.Select(d => d switch
        {
            Smartwatch sw => $"SW,{sw.Id},{sw.Name},{sw.BatteryPercentage}",
            PersonalComputer pc => $"P,{pc.Id},{pc.Name},{pc.OperatingSystem}",
            EmbeddedDevice ed => $"ED,{ed.Id},{ed.Name},{ed.IpAddress},{ed.NetworkName}",
            _ => ""
        }).ToList();
        //this is needed to transform the device objects to a csv-formatted string
        
        File.WriteAllLines(_filePath, lines);
    }
}