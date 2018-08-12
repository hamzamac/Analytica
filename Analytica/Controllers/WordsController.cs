using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analytica.Data;
using Analytica.Models;
using AnalyticLib;
using JsonFileDB;
using Microsoft.AspNetCore.Mvc;

namespace Analytica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly AppDBContext _db;

        public WordsController(IDBContext db)
        {
            _db = (AppDBContext) db;
        }

        // GET api/values
        [HttpGet("database/{command}")]
        public IActionResult InitialiseDatabase(string command)
        {
            if(command.Equals("initialize"))
            {
                NewsFetcher newsFetcher = new NewsFetcher();
                var news = newsFetcher.GetNews(newsFetcher.PlockUri());
                //clear db

                _db.NewsEntity.GetAll().ToList().ForEach(ne => _db.NewsEntity.Remove(ne.Id));

                // add new data
                news.ForEach(n => _db.NewsEntity.Add(new NewsEntity() { News = n }));

                _db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult<List<WordStats>> GetAll()
        {
            var news = _db.NewsEntity.GetAll().ToList().Select( n => n.News).ToList();

            TextAnalyzer text = new TextAnalyzer(news);

            var wc = text.SplitText().WordCount()
                .OrderByDescending(w => w.Value).Select(w => new WordStats() { Word = w.Key, Frequency = w.Value }).ToList();

            return wc;
        }


        [HttpGet("year/{year:int:length(4)}")]
        public ActionResult<List<WordStats>> GetByYear(int year)
        {
            var news = _db.NewsEntity
                .GetAll()
                .ToList()
                .Where(n => n.News.Date.Year == year)
                .Select(n => n.News)
                .ToList();

            TextAnalyzer text = new TextAnalyzer(news);

            var wc = text.SplitText()
                .WordCount()
                .OrderByDescending(w => w.Value)
                .Select(w => new WordStats() { Word = w.Key, Frequency = w.Value })
                .ToList();

            return wc;
        }

        [HttpGet("month/{month:int:range(1,12)}")]
        public ActionResult<List<WordStats>> GetByMonth(int month)
        {
            var news = _db.NewsEntity
                .GetAll()
                .ToList()
                .Where(n => n.News.Date.Month == month)
                .Select(n => n.News)
                .ToList();

            TextAnalyzer text = new TextAnalyzer(news);

            var wc = text.SplitText()
                .WordCount()
                .OrderByDescending(w => w.Value)
                .Select(w => new WordStats() { Word = w.Key, Frequency = w.Value })
                .ToList();

            return wc;
        }

        [HttpGet("year/{year:int:length(4)}/month/{month:int:range(1,12)}")]
        public ActionResult<List<WordStats>> GetByMonthInAYear(int year, int month)
        {
            var news = _db.NewsEntity
                .GetAll()
                .ToList()
                .Where(n => n.News.Date.Month == month && n.News.Date.Year == year)
                .Select(n => n.News)
                .ToList();

            TextAnalyzer text = new TextAnalyzer(news);

            var wc = text.SplitText().WordCount()
                .OrderByDescending(w => w.Value).Select(w => new WordStats() { Word = w.Key, Frequency = w.Value }).ToList();
            // .ToDictionary(w => w.Key, w => w.Value);

            return wc;
        }
     
    }
}
