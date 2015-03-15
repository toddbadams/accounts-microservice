using System;
using tba.Projects.Entities;

namespace tba.Projects.Models
{

    public class ProjectUm 
    {
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Create a new entity
        /// </summary>
        public Project CreateEntity(string clientId, string userId, DateTime timeStamp)
        {
            return Project.Create(Name, Description);
        }

        public Project CreateUpdatedEntity(Project entity)
        {
            return new Project
            {
                Id = entity.Id,
                Status = entity.Status,
                Name = Name,
            };

        }
    }
}