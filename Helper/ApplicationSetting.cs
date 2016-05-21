using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMConsultants.DuplicateDetectionReport
{
    internal static class ApplicationSetting
    {
        public static EntityData SelectedEntity { get; set; }
        public static List<string> AttributesDisplayList { get; set; }
        public static List<string> AttributesSchemaList { get; set; }
        // public static List<string> ExtraColumns { get; set; }
        public static List<string> AttributesToDisplay { get; set; }
        public static List<string> AttributesToDisplayName { get; set; }
        public static EntityCollection DuplicateCollection { get; set; }
        public static Dictionary<string, string> AttributesSchemaWithTypes { get; set; }
        public static List<Guid> JobId { get; set; }
        public static Dictionary<Guid, string> ExistingJobIds { get; set; }
    }
}
