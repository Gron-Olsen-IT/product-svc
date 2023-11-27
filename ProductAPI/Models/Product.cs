namespace ProductAPI.Models;
public class Product
{
    public string Id { get; set; }
    public string SellerId { get; set; }
    public int Valuation { get; set; }
    public DateTime CreateAt { get; set; }
    public int Status { get; set; }
}
