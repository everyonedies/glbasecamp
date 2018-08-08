namespace Model.Storage
{
    class ProductBatch
    {
        public int Id { get; set; }
        public int BatchId_FK { get; set; }
        public int ProductId_FK { get; set; }
        public int ProductQuantity { get; set; }
    }
}
