using CQSHelper.Interfaces;
using CQSPROJ.CQSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSPROJ.Queries
{
    public class GetUserQuery : Query
    {
        public string Name { get; set; }

        public string Colour { get; set; }
    }

    public class GetUserQueryResult : Result
    {
        public string Name { get; set; }
        public string Colour { get; set; }
        public string BaloonAnimal { get; set; }
    }
}
