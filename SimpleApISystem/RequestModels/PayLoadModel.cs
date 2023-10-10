namespace SimpleApISystem.RequestModels
{
    public class PayloadModel
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string GroupId { get; set; }
        public string DataType { get; set; }
        public Data Data { get; set; }
        public long Timestamp { get; set; }
    }

    public class Data
    {
        public bool FullPowerMode { get; set; }
        public bool ActivePowerControl { get; set; }
        public int FirmwareVersion { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public bool Occupancy { get; set; }
    }

}
