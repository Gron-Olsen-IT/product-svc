namespace ProductAPI.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{
    public Product (string sellerId, int valuation, DateTime createAt, int status)
    {
        SellerId = sellerId;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }
    
    [BsonId]
    public string? Id { get; set; }
    public string SellerId { get; set; }
    public int Valuation { get; set; }
    public DateTime CreateAt { get; set; }
    public int Status { get; set; }
}
