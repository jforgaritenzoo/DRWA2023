using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class PresensiMengajar
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    [MinLength(6)]
    public string NIP { get; set; } = null!;

    [Required]
    public string tgl { get; set; } = null!;

    [Required]
    public bool Kehadiran { get; set; }

    [Required]
    public string Kelas { get; set; } = null!;
}
