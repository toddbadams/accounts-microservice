using System;
using tba.Core.ViewModels;
using tba.Projects.Models;

namespace tba.Projects.Services
{
    /// <summary>
    /// 
    /// 
    /// Assumptions:
    ///     clientId is valid
    ///     userId is valid
    ///     must validate projectId
    ///     must validate locationId
    ///     must validate contactId
    /// </summary>
    public interface IProjectService : IDisposable
    {
        PagedViewModel<ProjectRm> Fetch(string clientId, int start, int limit);
        ProjectRm Get(string clientId, string id);
        ProjectRm Insert(string userId, string clientId, ProjectUm project);
        ProjectRm Update(string userId, string clientId, string id, ProjectUm project);

        ProjectRm MarkAsActive(string userId, string clientId, string projectId);
        ProjectRm MarkAsFinished(string userId, string clientId, string projectId);

        ProjectRm Delete(string userId, string clientId, string id);
        ProjectRm UnDelete(string userId, string clientId, string id);
        PagedViewModel<ProjectRm> FetchDeleted(string userId, string clientId, int start, int limit);
    }
}