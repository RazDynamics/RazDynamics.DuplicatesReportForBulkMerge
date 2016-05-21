using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMConsultants.DuplicateDetectionReport
{
    public class DuplicateRecords
    {
        public Guid MasterId
        { get; set; }

        public List<Guid> ChildIds
        { get; set; }

        public Entity MasterEntity
        { get; set; }

        public IEnumerable<Entity> ChildEntity
        { get; set; }
    }
}
