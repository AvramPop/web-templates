using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspMVCex.DataAbstractionLayer;
using AspMVCex.Models;
using Newtonsoft.Json;

namespace AspMVCex.Controllers
{
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index()
        {
            return View("ViewCatalog");
        }

        public ActionResult GetAssets()
        {
            int userId = int.Parse(Request.Params["userId"]);
            DB db = new DB();
            List<Asset> assetsList = db.GetAllAssetsOfUser(userId);
            return Json(new { assets = assetsList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAssets()
        {
            DB db = new DB();


            List<Asset> list = JsonConvert.DeserializeObject<List<Asset>>(Request.Params["newAssetsToAdd"]);
            foreach(Asset asset in list)
            {
                Debug.WriteLine(asset.ToString());
                db.SaveAsset(asset);
            }
            return Json(new { });
        }

    }
}