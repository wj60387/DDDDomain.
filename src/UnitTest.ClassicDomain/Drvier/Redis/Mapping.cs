﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ClassicDomain.Drvier.Redis
{
    class Mapping : Oldmansoft.ClassicDomain.Driver.Redis.Context
    {
        protected override void OnModelCreating()
        {
            Add<Domain.Book, Guid>(o => o.Id);
        }
    }

    class MappingCustomConnectionName : Oldmansoft.ClassicDomain.Driver.Redis.Context<string>
    {
        protected override void OnModelCreating(string parameter)
        {
            ConnectionName = parameter;
            Add<Domain.Book, Guid>(o => o.Id);
        }
    }
}
