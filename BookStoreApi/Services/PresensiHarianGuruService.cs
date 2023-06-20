using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class PresensiHarianGuruService
{
    private readonly IMongoCollection<PresensiHarianGuru> _presensiHarianGuruService;

    public PresensiHarianGuruService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _presensiHarianGuruService = mongoDatabase.GetCollection<PresensiHarianGuru>(
            bookStoreDatabaseSettings.Value.PresensiHarianGuruCollectionName);
    }

    public async Task<List<PresensiHarianGuru>> GetAsync() =>
        await _presensiHarianGuruService.Find(_ => true).ToListAsync();

    public async Task<PresensiHarianGuru?> GetAsync(string id) =>
        await _presensiHarianGuruService.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PresensiHarianGuru newPresensiHarianGuru) =>
        await _presensiHarianGuruService.InsertOneAsync(newPresensiHarianGuru);

    public async Task UpdateAsync(string id, PresensiHarianGuru updatedPresensiHarianGuru) =>
        await _presensiHarianGuruService.ReplaceOneAsync(x => x.Id == id, updatedPresensiHarianGuru);

    public async Task RemoveAsync(string id) =>
        await _presensiHarianGuruService.DeleteOneAsync(x => x.Id == id);
}