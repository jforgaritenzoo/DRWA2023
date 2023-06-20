using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class KelasService
{
    private readonly IMongoCollection<Kelas> _kelasService;

    public KelasService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _kelasService = mongoDatabase.GetCollection<Kelas>(
            bookStoreDatabaseSettings.Value.KelasCollectionName);
    }

    public async Task<List<Kelas>> GetAsync() =>
        await _kelasService.Find(_ => true).ToListAsync();

    public async Task<Kelas?> GetAsync(string id) =>
        await _kelasService.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Kelas newKelas) =>
        await _kelasService.InsertOneAsync(newKelas);

    public async Task UpdateAsync(string id, Kelas updatedKelas) =>
        await _kelasService.ReplaceOneAsync(x => x.Id == id, updatedKelas);

    public async Task RemoveAsync(string id) =>
        await _kelasService.DeleteOneAsync(x => x.Id == id);
}