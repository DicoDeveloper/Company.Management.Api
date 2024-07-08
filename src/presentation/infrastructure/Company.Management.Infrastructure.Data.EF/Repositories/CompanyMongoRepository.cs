using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Companies.Interfaces;
using Company.Management.Application.Core.Pagination;
using Company.Management.Domain.Companies.Entities;
using Company.Management.Infrastructure.Data.EF.Filters;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Company.Management.Infrastructure.Data.EF.Repositories;

public class CompanyMongoRepository : ICompanyMongoRepository
{
    private readonly MongoDbContext _mongoDbContext;

    private const string CountFacetName = "count";
    private const string DataFacetName = "data";

    public CompanyMongoRepository(MongoDbContext mongoDbContext)
        => _mongoDbContext = mongoDbContext;

    public async Task<(int, IList<MongoCompany>)> FetchAsync(
        Paginated paginated,
        string? name = null,
        string? sizeType = null,
        CancellationToken cancellationToken = default
    )
    {
        var fluentAggregate = _mongoDbContext.Companies
            .Aggregate();

        var filters = new MongoCompanyFilterDto
        {
            Paginated = paginated,
            Name = name,
            SizeType = sizeType
        };

        fluentAggregate = MongoCompanyFilter.Create(
            fluentAggregate,
            filters
        );

        var sort = Builders<MongoCompany>.Sort.Ascending(_ => _.Name);

        var (countFacet, dataFacet) = GetCountAndDataFacets(
            sort,
            filters.Paginated
        );

        var aggregation = await fluentAggregate
            .Facet(countFacet, dataFacet)
            .ToListAsync(cancellationToken);

        var count = aggregation.First()
            .Facets.First(_ => _.Name == CountFacetName)
            .Output<AggregateCountResult>()
            ?.FirstOrDefault()
            ?.Count;

        if (count is null)
            return (0, new List<MongoCompany>());

        var data = aggregation.First()
            .Facets.First(x => x.Name == DataFacetName)
            .Output<MongoCompany>();

        return ((int)count.Value, data.ToList());
    }

    public async Task<MongoCompany> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _mongoDbContext.Companies
                .Find(
                    company =>
                    company.Id == id &&
                    company.IsDeleted == false
                ).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(MongoCompany entity)
        => await _mongoDbContext.Companies.InsertOneAsync(entity);

    public async Task UpdateAsync(MongoCompany entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<MongoCompany>.Filter.Eq(e => e.Id, entity.Id);
        var update = Builders<MongoCompany>.Update.Set(s => s.Name, entity.Name)
                                                  .Set(s => s.SizeType, entity.SizeType);

        await _mongoDbContext.Companies.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<MongoCompany>.Filter.Eq(e => e.Id, id);
        var update = Builders<MongoCompany>.Update.Set(s => s.IsDeleted, true);

        await _mongoDbContext.Companies.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    private static (AggregateFacet<MongoCompany, AggregateCountResult>?, AggregateFacet<MongoCompany, MongoCompany>?) GetCountAndDataFacets<MongoCompany>(
        SortDefinition<MongoCompany> sortDefinition,
        Paginated? paginated
    )
    {
        var countFacet = AggregateFacet.Create(
            CountFacetName,
            PipelineDefinition<MongoCompany, AggregateCountResult>.Create(
                new[]
                {
                    PipelineStageDefinitionBuilder.Count<MongoCompany>()
                }
            )
        );

        var dataFacet = AggregateFacet.Create(
                DataFacetName,
                PipelineDefinition<MongoCompany, MongoCompany>.Create(
                    new[]
                    {
                        PipelineStageDefinitionBuilder.Sort(sortDefinition),
                        PipelineStageDefinitionBuilder.Skip<MongoCompany>(paginated!.Page * paginated.Size),
                        PipelineStageDefinitionBuilder.Limit<MongoCompany>(paginated.Size),
                    }
                )
            );

        return (countFacet, dataFacet);
    }
}