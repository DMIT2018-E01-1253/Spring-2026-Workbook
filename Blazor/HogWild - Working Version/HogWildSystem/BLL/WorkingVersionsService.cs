#nullable disable
using HogWildSystem.DAL;
using HogWildSystem.ViewModels;
using System;
using System.Linq;

namespace HogWildSystem.BLL
{
    public class WorkingVersionsService
    {
        private readonly HogWildContext _hogWildContext;

        //  Constructor for the WorkingVersionsService class.
        internal WorkingVersionsService(HogWildContext hogWildContext)
        {
            _hogWildContext = hogWildContext;
        }

        public WorkingVersionsView GetWorkingVersion()
        {
            return _hogWildContext.WorkingVersions
                .Select(x => new WorkingVersionsView
                {
                    VersionID = x.VersionId,
                    Major = x.Major,
                    Minor = x.Minor,
                    Build = x.Build,
                    Revision = x.Revision,
                    AsOfDate = x.AsOfDate,
                    Comments = x.Comments
                }).FirstOrDefault();
        }
    }
}
