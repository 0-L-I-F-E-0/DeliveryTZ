namespace DeliveryTZ
{
    class ModelFile
    {
        internal int ID { get; private set; }
        internal double Weight { get; private set; }
        internal string? Street { get; private set; }
        internal string? House { get; private set; }
        internal DateTime Time { get; private set; }

        public override string ToString()
        {
            return string.Join(';',ID,Weight,Street,House,Time.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        internal static ModelFile ValuesInLINQ(string line)
        {
            string[] massline = line.Split(';');
            return new ModelFile()
            {
                ID = int.Parse(massline[0]),
                Weight = double.Parse(massline[1]),
                Street = massline[2].Split(',')[0],
                House = massline[2].Split(',')[1],
                Time = DateTime.Parse(massline[3])
            };
        }

        internal static ModelFile StreetAndTimeinLINQ(string line)
        {
            string[] massline = line.Split(';');
            return new ModelFile()
            {
                Street = massline[2].Split(',')[0],
                Time = DateTime.Parse(massline[3])
            };
        }

        internal static ModelFile IDInLINQ(string line)
        {
            string[] massline = line.Split(';');
            return new ModelFile()
            {
                ID = int.Parse(massline[0]),
            };
        }
    }
}
