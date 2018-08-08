namespace Model.Storage
{
    class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string UnitMeasure { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            string str = $"ProductId: {ProductId}\n";
            str += $"Name: {Name}\n";
            str += $"UnitMeasure: {UnitMeasure}\n";
            str += $"UnitPrice: {UnitPrice}\n";
            str += $"Quantity: {Quantity}\n";

            return str;
        }
    }
}
