using MongoDB.Driver;
using Drivers.Api.Models;
using Microsoft.Extensions.Options;
using Drivers.Api.Configurations;
using Drivers.Api.Controllers.Requests;

namespace Drivers.Api.Services;

public class DriverService
{
	private readonly IMongoCollection<Driver> _driversCollection;

	public DriverService(IOptions<DatabaseSettings> databaseSettings)
	{
		var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

		// Connect to mongo DB
		var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

		_driversCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.ConnectionString);
	}

	public async Task<List<Driver>> GetAsync() =>
		await _driversCollection.Find(_ => true).ToListAsync();

	public async Task<Driver> AddAsync(DriverRequest request)
	{
		var driver = new Driver();

		driver.Name = request.Name;
		driver.Number = request.Number;
		driver.Team = request.Team;

		await _driversCollection.InsertOneAsync(driver);
		return driver;
    }
}
