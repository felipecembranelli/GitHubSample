using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GitHubSample.Web.SPA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ListRepos()
        {
            return PartialView("_listRepos");
        }

        public PartialViewResult RepoDetail()
        {
            return PartialView("_repoDetail");
        }

        public PartialViewResult FavoritesRepos()
        {
            return PartialView("_listFavoritesRepos");
        }
    }
}
