using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace MSGraphSecurity
{
    public class Alert
    {
        //Properties of Alert
        public string ActivityGroupName { get; set; }
        public string AssignedTo { get; set; }
        public string AzureSubscriptionId { get; set; }
        public string AzureTenantId { get; set; }
        public string Category { get; set; }
        public DateTimeOffset ClosedDateTime { get; set; }
        public List<CloudAppSecurityState> CloudAppStates { get; set; }
        public List<string> Comments { get; set; }
        public int Confidence { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public string Description { get; set; }
        public List<string> DetectionIds { get; set; }
        public DateTimeOffset EventDateTime { get; set; }
        public AlertFeedback Feedback { get; set; }
        public List<FileSecurityState> FileStates { get; set; }
        public List<AlertHistoryState> HistoryStates { get; set; }
        public List<HostSecurityState> HostStates { get; set; }
        public string Id { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public List<MalwareState> MalwareStates { get; set; }
        public List<NetworkConnection> NetworkConnections { get; set; }
        public List<Process> Processes { get; set; }
        public List<string> RecommendedActions { get; set; }
        public List<RegistryKeyState> RegistryKeyStates { get; set; }
        public AlertSeverity Severity { get; set; }
        public List<string> SourceMaterials { get; set; }
        public AlertStatus Status { get; set; }
        public List<string> Tags { get; set; }
        public string Title { get; set; }
        public List<AlertTrigger> Triggers { get; set; }
        public List<UserSecurityState> UserStates { get; set; }
        public SecurityVendorInformation VendorInformation { get; set; }
        public List<VulnerabilityState> VulnerabilityStates { get; set; }
    }
}