using Analytica.Models;
using JsonFileDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytica.Data
{
    public class AppDBContext : DBContext
    {
        public AppDBContext(IConfiguration configuration) : base(configuration.GetConnectionString("JsonDBPath"))
        {
            NewsEntity = new Dataset<NewsEntity>(_database);
        }

        public Dataset<NewsEntity> NewsEntity { get; set; }
    }
}
