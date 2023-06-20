using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class MapelService
{
    private readonly IMongoCollection<Mapel> _mapelService;

    public MapelService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _mapelService = mongoDatabase.GetCollection<Mapel>(
            bookStoreDatabaseSettings.Value.MapelCollectionName);
    }

    public async Task<List<Mapel>> GetAsync() =>
        await _mapelService.Find(_ => true).ToListAsync();

    public async Task<Mapel?> GetAsync(string id) =>
        await _mapelService.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Mapel newMapel) =>
        await _mapelService.InsertOneAsync(newMapel);

    public async Task UpdateAsync(string id, Mapel updatedMapel) =>
        await _mapelService.ReplaceOneAsync(x => x.Id == id, updatedMapel);

    public async Task RemoveAsync(string id) =>
        await _mapelService.DeleteOneAsync(x => x.Id == id);
}