using System;
using System.Collections.Generic;
using System.Text;

namespace MSGraphSecurity
{
    public class SecureScore
    {
        //properties of SecureScore
        public string Id { get; }
        public string AzureTenantId { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public int LicensedUserCount { get; set; }
        public int ActiveUserCount { get; set; }
        public int CurrentScore { get; set; }
        public int MaxScore { get; set; }
        public List<string> EnabledServices { get; set; }
        public List<AverageComparativeScores> AverageComparativeScores { get; set; }
        public List<ControlScores> ControlScores { get; set; }
    }
    public class AverageComparativeScores
    {
        public string Basis { get; set; }
        public double AverageScore { get; set; }
        public double DeviceScore { get; set; }
        public double DataScore { get; set; }
        public double IdentityScore { get; set; }
    }
    public class ControlScores
    {
        public string ControlName { get; set; }
        public double Score { get; set; }
        public string ControlCategory { get; set; }
        public string Description { get; set; }
    }
}
