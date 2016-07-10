// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MySqlEntityFrameworkServicesBuilderExtensions
    {
        public static IServiceCollection AddEntityFrameworkMySql([NotNull] this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddRelational();

            services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IDatabaseProvider, DatabaseProvider<MySqlDatabaseProviderServices, MySqlOptionsExtension>>());

            services.TryAdd(new ServiceCollection()
                .AddSingleton<MySqlValueGeneratorCache>()
                .AddSingleton<MySqlTypeMapper>()
                .AddSingleton<MySqlSqlGenerationHelper>()
                .AddSingleton<MySqlModelSource>()
                .AddSingleton<MySqlAnnotationProvider>()
                .AddSingleton<MySqlMigrationsAnnotationProvider>()
               .AddScoped<MySqlConventionSetBuilder>()
                .AddScoped<IMySqlUpdateSqlGenerator, MySqlUpdateSqlGenerator>()
                .AddScoped<MySqlModificationCommandBatchFactory>()
                .AddScoped<MySqlDatabaseProviderServices>()
                .AddScoped<MySqlRelationalConnection>()
                .AddScoped<MySqlDatabaseCreator>()
                .AddScoped<MySqlHistoryRepository>()
                .AddScoped<MySqlMigrationsSqlGenerationHelper>()
                .AddScoped<MySqlModificationCommandBatchFactory>()
                .AddQuery());

            return services;
        }

        private static IServiceCollection AddQuery(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<MySqlQueryCompilationContextFactory>()
                .AddScoped<MySqlCompositeMemberTranslator>()
                .AddScoped<MySqlCompositeMethodCallTranslator>()
                .AddScoped<MySqlQuerySqlGenerationHelperFactory>();
        }
    }
}
