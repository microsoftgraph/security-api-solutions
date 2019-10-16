using System;
using System.Collections.Generic;
using System.Text;

namespace MSGraphSecurity
{
    public class TiIndicator
    {
        //public string Action  { get; set; }
        public List<string> ActivityGroupNames { get; set; }
        public int Confidence { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ExpirationDateTime { get; set; }
        public string ExternalId { get; set; }
        public string FileHashType { get; set; }
        public string FileHashValue { get; set; }
        public List<string> KillChain { get; set; }
        public List<string> MalwareFamilyNames { get; set; }
        public int Severity { get; set; }
        public List<string> Tags { get; set; }
        public string TargetProduct { get; set; }
        public string ThreatType { get; set; }
        public string TlpLevel { get; set; }
    }
}
