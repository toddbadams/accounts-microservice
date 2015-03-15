using System.ComponentModel.DataAnnotations;
using tba.Core.Entities;

namespace tba.Projects.Entities
{
    public class Project : Entity
    {
        /// <summary>
        /// The projects' title or name
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// A project description
        /// </summary>
        [MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// The current status of the project (active or finished)
        /// </summary>
        public ProjectStatus Status { get; set; }

        /// <summary>
        /// The location of the project 
        /// </summary>
        public string LocationId { get; set; }

        public static string GetProjectStatusString(ProjectStatus status)
        {
            switch ((int)status)
            {
                case 0:
                    return "active";
                case 1:
                    return "finished";
            }
            return "unknown";
        }

        public static ProjectStatus GetProjectStatusEnum(string status)
        {
            switch (status)
            {
                case "active":
                    return ProjectStatus.Active;
                case "finished":
                    return ProjectStatus.Finished;
            }
            return ProjectStatus.Active;
        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Project Create(string name, string description)
        {
            var e = new Project
            {
                Id = NewBase64Id(),
                Name = name,
                Description = description,
                Active = true,
                Status = ProjectStatus.Active
            };
            return e;
        }

        public enum ProjectStatus
        {
            Active = 0,
            Finished = 1
        }
    }
}