using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Enumerables
{
    public enum TicketType
    {
        [Description("Bugs/Errors")]
        BugsOrErrors,
        [Description("Feature Requests")]
        FeatureRequests,
        [Description("Other Comments")]
        OtherComments,
        [Description("Training/Document Requests")]
        TrainingOrDocumentRequests
    }
}
