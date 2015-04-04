using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ProjectileProfileSave : ScriptableObject
    {
        public List<ProjectileProfile> ProjectileProfiles = new List<ProjectileProfile>();

        public ProjectileProfile Get(int id)
        {
            return ProjectileProfiles.Find(item => item.ID == id);
        }
    }
}