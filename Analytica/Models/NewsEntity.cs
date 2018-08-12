using AnalyticLib;
using JsonFileDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analytica.Models
{
    public class NewsEntity : ITable
    {
        public int Id { get; set; }
        public News News { get; set; }
    }
}
