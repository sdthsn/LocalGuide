using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoAndMVCTutorial.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Index()
        {
            var theModel = GetThePictures();
            return View(theModel);
        }

        public ActionResult AddPicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase theFile)
        {
            if (theFile.ContentLength > 0)
            {
                //get the file's name
                string theFileName = Path.GetFileName(theFile.FileName);

                //get the bytes from the content stream of the file
                byte[] thePictureAsBytes = new byte[theFile.ContentLength];
                using (BinaryReader theReader = new BinaryReader(theFile.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(theFile.ContentLength);
                }

                //convert the bytes of image data to a string using the Base64 encoding
                string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

                //create a new mongo picture model object to insert into the db
                MongoPictureModel thePicture = new MongoPictureModel()
                {
                    FileName = theFileName,
                    PictureDataAsString = thePictureDataAsString
                };

                //insert the picture object
                bool didItInsert = InsertPictureIntoDatabase(thePicture);

                if (didItInsert)
                    ViewBag.Message = "The image was updated successfully";
                else
                    ViewBag.Message = "A database error has occured";
            }
            else
                ViewBag.Message = "You must upload an image";

            return View();
        }

        /// &lt;summary>
        /// This method will insert the picture into the db
        /// &lt;/summary>
        /// &lt;param name="thePicture">&lt;/param>
        /// &lt;returns>&lt;/returns>
        private bool InsertPictureIntoDatabase(MongoPictureModel thePicture)
        {
            var thePictureColleciton = GetPictureCollection();
            var theResult = thePictureColleciton.Insert(thePicture);
            return theResult.Ok;
        }

        /// &lt;summary>
        /// This method will return just the id's and filenames of the pictures to use to retrieve the image from the db
        /// &lt;/summary>
        /// &lt;returns>&lt;/returns>
        private List<MongoPictureModel> GetThePictures()
        {
            var thePictureColleciton = GetPictureCollection();
            var thePictureCursor = thePictureColleciton.FindAll();

            //use SetFields to just return the id and the name of the picture instead of the entire document
            thePictureCursor.SetFields(Fields.Include("_id", "FileName"));

            return thePictureCursor.ToList() ?? new List<MongoPictureModel > ();
        }

        /// &lt;summary>
        /// This action will return an image result to render the data from the picture as a jpeg
        /// &lt;/summary>
        /// &lt;param name="id">id of the picture as a string&lt;/param>
        /// &lt;returns>&lt;/returns>
        public FileContentResult ShowPicture(string id)
        {
            var thePictureColleciton = GetPictureCollection();

            //get pictrue document from db
            var thePicture = thePictureColleciton.FindOneById(new ObjectId(id));

            //transform the picture's data from string to an array of bytes
            var thePictureDataAsBytes = Convert.FromBase64String(thePicture.PictureDataAsString);

            //return array of bytes as the image's data to action's response. We set the image's content mime type to image/jpeg
            return new FileContentResult(thePictureDataAsBytes, "image/jpeg");
        }

        /// &lt;summary>
        /// This will return the mongoDB picture collection object to use dor data related actions
        /// &lt;/summary>
        /// &lt;returns>&lt;/returns>
        private MongoCollection<MongoPictureModel> GetPictureCollection()
        {
            //set this to what ever your connection is or from config
            var theConnectionString = "mongodb://localhost";

            //get the mongo db client object
            var theDBClient = new MongoClient(theConnectionString);

            //get reference to db server
            var theServer = theDBClient.GetServer();

            //gets the database , if it doesn't exist it will create a new one
            string databaseName = "PictureApplication";//replace with whatever name you choose
            var thePictureDB = theServer.GetDatabase(databaseName);

            //finally attempts to get a collection, if not there it will make a new one
            string theCollectionName = "pictures";
            var thePictureColleciton = thePictureDB.GetCollection <MongoPictureModel > (theCollectionName);

            return thePictureColleciton;
        }
    }
    public class MongoPictureModel
    {
        public ObjectId _id { get; set; }
        public string FileName { get; set; }
        public string PictureDataAsString { get; set; }
    }
}