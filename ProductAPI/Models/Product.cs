namespace ProductAPI.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public record Product
{
    public Product (string sellerId, string productName, string description, int valuation, DateTime createAt, int status)
    {
        SellerId = sellerId;
        ProductName = productName;
        Description = description;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }

    public Product (string id, string productName, string description, string sellerId, int valuation, DateTime createAt, int status)
    {
        Id = id;
        ProductName = productName;
        Description = description;
        SellerId = sellerId;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string SellerId { get; set; }
    public int Valuation { get; set; }
    public DateTime CreateAt { get; set; }
    public int Status { get; set; }
}

public record ProductDTO
{
    public ProductDTO (string sellerId, string productName, string description, int valuation, DateTime createAt, int status)
    {
        SellerId = sellerId;
        ProductName = productName;
        Description = description;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }
    
    public string SellerId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int Valuation { get; set; }
    public DateTime CreateAt { get; set; }
    public int Status { get; set; }
}
