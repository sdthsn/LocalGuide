using Data.Repositories;
using DataModel.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CityViews.Controllers
{
    [RoutePrefix("City")]
    public class SpotsController : ApiController
    {
        private readonly MongoUrl _url;
        private readonly MongoClient _client;
        private readonly SpotsRepository _repo;
        private readonly IMongoDatabase _db;
        public SpotsController()
        {

            _url = new MongoUrl("mongodb://localhost:27017/naturalBeauty-dev");
            _client = new MongoClient(_url);
            var databse = _url.DatabaseName;
            _repo = new SpotsRepository(_client, databse, "spots");
            _db = _client.GetDatabase(databse);

        }
        [HttpGet]
        [Route("info")]
        public async Task<IHttpActionResult> GetInfo()
        {
            IList<OutputModel> models = new List<OutputModel>();
            var data = await _repo.GetAsync();
            foreach (var item in data)
            {
                var model = new OutputModel {Name= item.Name,ShortSummery=item.ShortSummery,Description=item.Description,Id=item.Imageinfo};
                models.Add(model);
            }
            return Ok(models);

        }
        [HttpGet]
        [Route("download")]
        public async Task<IHttpActionResult> GetImage( string id)
        {
            var data = await _repo.FileDownloadAsync(new ObjectId(id));
            MemoryStream ms = new MemoryStream(data);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return ResponseMessage (response);

        }

        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> Post(string name,string shortSummery ,string description  )
        {
            var content = await Request.Content.ReadAsStreamAsync();
            var id = await _repo.FileUploadAsync(name, content);

            var data = new Spot
            {
                CreatedOn = DateTime.Now,
                Name = name,
                ShortSummery = shortSummery,
                Description = description,
                Imageinfo = id

            };
            await _repo.CreateAsync(data);

            return Ok("File uploaded successfully");
        }
        
    }
    public class OutputModel
    {
        public string Name { get; set; }
        public string ShortSummery { get; set; }
        public string Description { get; set; } 
        public ObjectId Id { get; set; }
    }
}
