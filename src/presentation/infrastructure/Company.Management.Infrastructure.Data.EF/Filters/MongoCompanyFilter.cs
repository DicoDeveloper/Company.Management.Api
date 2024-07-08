using Company.Management.Application.Companies.Dtos;
using Company.Management.Domain.Companies.Entities;
using MongoDB.Driver;

namespace Company.Management.Infrastructure.Data.EF.Filters;

public static class MongoCompanyFilter
{
    public static IAggregateFluent<MongoCompany> Create(
        IAggregateFluent<MongoCompany> fluentAggregate,
        MongoCompanyFilterDto filters
    )
    {
        var builderCompany = Builders<MongoCompany>.Filter;

        fluentAggregate = fluentAggregate.Match(builderCompany.Eq(
            company =>
                company.IsDeleted,
                false
        ));

        fluentAggregate = FilterByNameIfNameHasValue(fluentAggregate, filters, builderCompany);
        fluentAggregate = FilterBySizeTypeIfSizeTypeHasValue(fluentAggregate, filters, builderCompany);

        return fluentAggregate;
    }

    private static IAggregateFluent<MongoCompany> FilterByNameIfNameHasValue(
        IAggregateFluent<MongoCompany> fluentAggregate,
        MongoCompanyFilterDto filters,
        FilterDefinitionBuilder<MongoCompany> builderCompany
    )
    {
        if (string.IsNullOrWhiteSpace(filters.Name))
            return fluentAggregate;

        var nameFilter = builderCompany.Eq(
            company =>
                company.Name,
                filters.Name
        );

        return fluentAggregate.Match(nameFilter);
    }

    private static IAggregateFluent<MongoCompany> FilterBySizeTypeIfSizeTypeHasValue(
        IAggregateFluent<MongoCompany> fluentAggregate,
        MongoCompanyFilterDto filters,
        FilterDefinitionBuilder<MongoCompany> builderCompany
    )
    {
        if (string.IsNullOrWhiteSpace(filters.SizeType))
            return fluentAggregate;

        var plateFilter = builderCompany.Eq(
            company =>
                company.SizeType,
                filters.SizeType
        );

        return fluentAggregate.Match(plateFilter);
    }
}