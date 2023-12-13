namespace ProductAPI.Models;

using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public record Product
{
    public Product(string id, string productName, string description, string sellerId, int valuation, DateTime createAt, int status)
    {
        Id = id;
        ProductName = productName;
        Description = description;
        SellerId = sellerId;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }

    public Product(ProductDTO productDTO)
    {
        ProductName = productDTO.ProductName;
        Description = productDTO.Description;
        SellerId = productDTO.SellerId;
        Valuation = productDTO.Valuation;
        CreateAt = productDTO.CreateAt;
        Status = productDTO.Status;
    }

    /// <summary>
    /// The product id
    /// </summary>
    /// <value>string</value>
    /// <example>65736f30947a96497fb45d61</example>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    /// <summary>
    /// The name of the product
    /// </summary>
    /// <value>string</value>
    /// <example>996 Viking Silver Coin</example>
    public string ProductName { get; set; }
    /// <summary>
    /// A description of the product
    /// </summary>
    /// <value>string</value>
    /// <example>A mint condition silver coin from the late 10th century. The coin was uncovered in Lancashire in 2004.</example>
    public string Description { get; set; }
    /// <summary>
    /// The user Id of the customer selling the product
    /// </summary>
    /// <value>string</value>
    /// <example>65736f30947a96497fb45d61</example>
    public string SellerId { get; set; }
    /// <summary>
    /// The valuation of the product
    /// </summary>
    /// <value>int</value>
    /// <example>10000</example>
    public int Valuation { get; set; }
    /// <summary>
    /// The time the product was created in the database
    /// </summary>
    /// <value>DateTime</value>
    /// <example>2023-12-08T08:47:24.767Z</example>
    public DateTime CreateAt { get; set; }
    /// <summary>
    /// An integer representing the status of the product. 0 = created, 1 = active, 3 = closed
    /// </summary>
    /// <value>int</value>
    /// <example>1</example>
    public int Status { get; set; }
}

public record ProductDTO
{
    public ProductDTO(string sellerId, string productName, string description, int valuation, DateTime createAt, int status)
    {
        SellerId = sellerId;
        ProductName = productName;
        Description = description;
        Valuation = valuation;
        CreateAt = createAt;
        Status = status;
    }

    /// <summary>
    /// The name of the product
    /// </summary>
    /// <value>string</value>
    /// <example>996 Viking Silver Coin</example>
    public string ProductName { get; set; }
    /// <summary>
    /// A description of the product
    /// </summary>
    /// <value>string</value>
    /// <example>A mint condition silver coin from the late 10th century. The coin was uncovered in Lancashire in 2004.</example>
    public string Description { get; set; }
    /// <summary>
    /// The user Id of the customer selling the product
    /// </summary>
    /// <value>string</value>
    /// <example>65736f30947a96497fb45d61</example>
    public string SellerId { get; set; }
    /// <summary>
    /// The valuation of the product
    /// </summary>
    /// <value>int</value>
    /// <example>10000</example>
    public int Valuation { get; set; }
    /// <summary>
    /// The time the product was created in the database
    /// </summary>
    /// <value>DateTime</value>
    /// <example>2023-12-08T08:47:24.767Z</example>
    public DateTime CreateAt { get; set; }
    /// <summary>
    /// An integer representing the status of the product. 0 = created, 1 = active, 3 = closed
    /// </summary>
    /// <value>int</value>
    /// <example>1</example>
    public int Status { get; set; }
}
