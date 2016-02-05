using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitHubSample;
using GitHubSample.Services.IServices;
using GitHubSample.Model;

namespace GitHubSample.Web.SPA.Controllers
{
    public class GitHubReposController : ApiController
    {

        private readonly IGitHubRepoService gitHubservice;
        private const string REPO_OWNER = "felipecembranelli";

        public GitHubReposController(IGitHubRepoService service)
        {
            this.gitHubservice = service;
        }

        // GET : api/GitHubReposApi
        [HttpGet]
        public IEnumerable<GitHubRepo> GetAllGitHubRepos()
        {
            return this.gitHubservice.GetUserRepositories(REPO_OWNER);
        }

        // GET : api/GitHubReposApi/reponame
        [HttpGet]
        public GitHubRepo GetGitHubRepoById(string id)
        {
            return this.gitHubservice.GetRepoByName(REPO_OWNER, id);
        }
    }
}
