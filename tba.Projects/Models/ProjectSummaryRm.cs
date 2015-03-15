using System.Collections.Generic;
using System.Linq;
using tba.Core.ViewModels;
using tba.Projects.Entities;

namespace tba.Projects.Models
{
    /// <summary>
    /// Used to create a list of projects
    /// </summary>
    public class ProjectRm: ProjectUm, IViewModel
    {
        public string Id { get; set; }
        public string Status { get; set; }

        public static PagedViewModel<ProjectRm> CreatePagedViewModel(ICollection<Project> entities, int start, int total)
        {
            return PagedViewModel<ProjectRm>.Create(start, total, entities.Select(CreateProjectSummaryRm));
        }

        private static ProjectRm CreateProjectSummaryRm(Project entity)
        {
            return entity != null
                ? new ProjectRm
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Status = Project.GetProjectStatusString(entity.Status),
                }
                : null;
        }
    }
}