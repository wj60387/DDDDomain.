﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.ClassicDomain;
using Oldmansoft.ClassicDomain.Util;
using Sample.ConsoleApplication.Domain;

namespace Sample.ConsoleApplication.Repositories
{
    class PersonRepository : Oldmansoft.ClassicDomain.Driver.Mongo.Repository<Domain.Person, Guid, Mapping>, Infrastructure.IPersonRepository
    {
        public PersonRepository(UnitOfWork uow)
            : base(uow)
        {
        }

        public IPagingData<Person> PageByName()
        {
            return Query().Paging().OrderBy(o => o.Name);
        }
    }
}
