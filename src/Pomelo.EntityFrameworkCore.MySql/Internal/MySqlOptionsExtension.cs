// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Internal
{
    public class MySqlOptionsExtension : RelationalOptionsExtension
    {
        public MySqlOptionsExtension()
        {
            this.MaxBatchSize = 1;
        }

        public MySqlOptionsExtension([NotNull] MySqlOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(IServiceCollection services)
                  => Check.NotNull(services, nameof(services)).AddEntityFrameworkMySql();
    }
}
