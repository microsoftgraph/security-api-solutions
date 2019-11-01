using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSGraphSecurity
{
    public class SecureScoreControlProfile
    {
        //properties of SecureScoreControlProfile
        public string Title { get; set; }
        public string AzureTenantId { get; set; }
        public string ControlName { get; set; }
        public double MaxScore { get; set; }
        public string ActionType { get; set; }
        public string Service { get; set; }
        public string Tier { get; set; }
        public string UserImpact { get; set; }
        public string ImplementationCost { get; set; }
        public int Rank { get; set; }
        public List<string> Threats { get; set; }
        public bool Deprecated { get; set; }
        public string Remediation { get; set; }
        public string RemediationImpact { get; set; }
        public string ActionUrl { get; set; }
        public List<SecureScoreControlStateUpdate> ControlStateUpdates { get; set; }
        public SecurityVendorInformation VendorInformation { get; set; }
        public List<ComplianceInformation> ComplianceInformation { get; set; }
        public string ControlCategory { get; set; }
    }
}
