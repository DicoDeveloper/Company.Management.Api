using Company.Management.Domain.Companies.Entities;
using MongoDB.Driver;

namespace Company.Management.Infrastructure.Data.EF;

public class MongoDbContext(string connectionString)
{
    private readonly IMongoDatabase _database = new MongoClient(connectionString).GetDatabase("CompanyManagement");

    public IMongoCollection<MongoCompany> Companies => _database.GetCollection<MongoCompany>("Companies");
}