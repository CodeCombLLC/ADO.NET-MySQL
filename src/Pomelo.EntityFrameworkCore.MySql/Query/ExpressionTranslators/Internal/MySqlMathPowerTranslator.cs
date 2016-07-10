﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class MySqlMathPowerTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public MySqlMathPowerTranslator()
            : base(typeof(Math), "Pow", "POWER")
        {
        }
    }
}
