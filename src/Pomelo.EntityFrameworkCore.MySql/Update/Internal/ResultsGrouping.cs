﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.Update.Internal
{
    public enum ResultsGrouping
    {
        OneResultSet,
        OneCommandPerResultSet
    }
}
