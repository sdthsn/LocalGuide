using Data.Repositories;
using DataModel.Contract;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
//using System.Drawing.Imaging;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoUrl url = new MongoUrl("mongodb://localhost:27017/naturalBeauty-dev");
            MongoClient client = new MongoClient(url);
            var databse = url.DatabaseName;
            var repo = new SpotsRepository(client, databse, "spots");
            IMongoDatabase db = client.GetDatabase(databse);
         
            try
            {
                //FileStream fs = File.Open("D:\\arc_de_triomphe.jpg", FileMode.Open);

                Stream inputStream = File.Open("D:\\arc_de_triomphe.jpg", FileMode.Open);
                

                var id = repo.FileUploadAsync("Arc de Triomphe", inputStream).Result;
               


                var data = new Spot
                {
                    CreatedOn = DateTime.Now,
                    Name = "Arc de Triomphe",
                    ShortSummery = "The Arc de Triomphe is one of the most famous monuments in Paris.",
                    Description = "he monument stands in the centre of the Place Charles de Gaulle, at the western end of the Champs-&Eacute;lys&eacute;es. It should not be confused with a smaller arch, the Arc de Triomphe du Carrousel, which stands west of the Louvre. The Arc de Triomphe honours those who fought and died for France in the French Revolutionary and the Napoleonic Wars, with the names of all French victories and generals inscribed on its inner and outer surfaces. Beneath its vault lies the Tomb of the Unknown Soldier from World War I.",
                    Imageinfo = id

                };

                try
                {

                    repo.CreateAsync(data).ConfigureAwait(false);
                    var result = repo.GetByIdAsync(data.Id.ToString()).Result;
                    Console.WriteLine(result.Name);
                    Console.WriteLine(repo.FileDownloadAsync(id).Result.ToJson());
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            Console.ReadKey();

        }
     
    }
}
