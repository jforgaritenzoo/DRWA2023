using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class PresensiMengajarService
{
    private readonly IMongoCollection<PresensiMengajar> _presensiMengajarService;

    public PresensiMengajarService(
        IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _presensiMengajarService = mongoDatabase.GetCollection<PresensiMengajar>(
            bookStoreDatabaseSettings.Value.PresensiMengajarCollectionName);
    }

    public async Task<List<PresensiMengajar>> GetAsync() =>
        await _presensiMengajarService.Find(_ => true).ToListAsync();

    public async Task<PresensiMengajar?> GetAsync(string id) =>
        await _presensiMengajarService.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(PresensiMengajar newPresensiMengajar) =>
        await _presensiMengajarService.InsertOneAsync(newPresensiMengajar);

    public async Task UpdateAsync(string id, PresensiMengajar updatedPresensiMengajar) =>
        await _presensiMengajarService.ReplaceOneAsync(x => x.Id == id, updatedPresensiMengajar);

    public async Task RemoveAsync(string id) =>
        await _presensiMengajarService.DeleteOneAsync(x => x.Id == id);
}