﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using GitHubSample.Model;
using GitHubSample.Data.Infrastructure;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GitHubSample.Data.GitHubAPI;

namespace GitHubSample.Data.Repository
{
    public class GitHubRepoRepository: Infrastructure.RepositoryBase<GitHubRepo>, IGitHubRepoRepository
    {

        public GitHubRepoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        #region EF wrapper

        /// <summary>
        /// Unmark repository as favorite, removing from local database
        /// </summary>
        /// <param name="repository"></param>
        public void UnMarkAsFavorite(GitHubRepo repository)
        {
            var entity = base.GetAll().Where(r => r.GitHubRepoId == repository.GitHubRepoId).FirstOrDefault();

            base.Delete(entity);

        }

        /// <summary>
        ///  Verify if repository is marked as favorite or not. 
        /// If it exists in local database, return true
        /// </summary>
        /// <param name="gitHubRepoId"></param>
        /// <returns></returns>
        public bool IsFavoriteRepo(int gitHubRepoId)
        {
            var entity = base.GetAll().Where(r => r.GitHubRepoId == gitHubRepoId).FirstOrDefault();

            if (entity != null)
                return true;
            else
                return false;

        }

        #endregion

        #region GitHub api wrapper

        /// <summary>
        /// Search github looking for query criteria, return a list of repositories
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<GitHubRepo> SearchRepositories(string query)
        {

            var repoList = new List<GitHubRepo>();

            try
            {
                string jsonString = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/search/repositories?q={0}", query));

                var jsonObjectDto = JsonConvert.DeserializeObject<GitHubRepoJsonDTO>(jsonString);

                foreach (var repo in jsonObjectDto.Items.ToList())
                {
                    repoList.Add(this.MapDtoToModel(repo));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return repoList;
        }

        /// <summary>
        /// Return a github repository throught its owner and repository name
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        public GitHubRepo GetRepoByName(string owner, string repoName)
        {
            var repoModel = new GitHubRepo();
            try
            {
                string json = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/repos/{0}/{1}", owner, repoName));

                var jsonDto = JsonConvert.DeserializeObject<GitHubRepoDTO>(json);

                repoModel = this.MapDtoToModel(jsonDto);
            }
            catch (Exception)
            {
                throw;
            }

            return repoModel;
        }

        /// <summary>
        /// Return all user repositories from github
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IEnumerable<GitHubRepo> GetUserRepositories(string userName)
        {
            var repoList = new List<GitHubRepo>();
            try
            {
                string json = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/users/{0}/repos", userName));

                var jsonDto = JsonConvert.DeserializeObject<List<GitHubRepoDTO>>(json);

                foreach (var repo in jsonDto.ToList())
                {
                    repoList.Add(this.MapDtoToModel(repo));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return repoList;
        }

        /// <summary>
        /// Return a list of repository's contributors
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        public IEnumerable<GitHubUserDTO> GetRepoContributors(string owner, string repoName)
        {
            var jsonObject = new List<GitHubUserDTO>();
            try
            {
                string json = GitHubApiWrapper.CallRestService(string.Format("https://api.github.com/repos/{0}/{1}/contributors", owner, repoName));

                jsonObject = JsonConvert.DeserializeObject<List<GitHubUserDTO>>(json);
            }
            catch (Exception)
            {
                throw;
            }

            return jsonObject;
        }

        #endregion

        #region Helper

        /// <summary>
        /// Map entity from DTO to model
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private GitHubSample.Model.GitHubRepo MapDtoToModel(GitHubRepoDTO dto)
        {
            GitHubSample.Model.GitHubRepo repoModel = new GitHubRepo();

            if (dto == null)
                return null;

            repoModel.Name = dto.name;
            repoModel.Description = dto.description;
            repoModel.GitHubRepoId = dto.id;
            repoModel.Language = dto.language;
            repoModel.OwnerAvatarUrl = dto.owner.AvatarUrl;
            repoModel.OwnerName = dto.owner.Login;
            repoModel.UpdatedAt = ConvertToDateTime(dto.updated_at);

            return repoModel;
        }

        /// <summary>
        /// Format date received from github api
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private System.DateTime? ConvertToDateTime(string value)
        {
            DateTime dt;

            try
            {
                dt = DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", null);
            }
            catch (Exception)
            {
                return null;
            }

            return dt;
        }
        #endregion
    }
}