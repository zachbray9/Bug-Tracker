using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Enumerables.Enum_Converters
{
    public class ProjectRoleOptionsRetriever
    {
        public Dictionary<ProjectRole, string> ProjectRoleOptionsDictionary;

        public ProjectRoleOptionsRetriever()
        {
            ProjectRoleOptionsDictionary = new Dictionary<ProjectRole, string>()
            {
                { ProjectRole.Administrator, "Administrator" },
                { ProjectRole.Developer, "Developer" }
            };
        }

        public string ConvertProjectRoleEnumToString(ProjectRole role)
        {
            if (ProjectRoleOptionsDictionary.TryGetValue(role, out var value))
            {
                return value.ToString();
            }

            return String.Empty;
        }

        public ProjectRole ConvertProjectRoleStringToEnum(string roleString)
        {
            ProjectRole role = ProjectRoleOptionsDictionary.FirstOrDefault(d => d.Value == roleString).Key;
            return role;
        }
    }
}
