using System;
using System.Collections.Generic;

namespace MSGraphSecurity
{
    public class TiIndicator
    {
        //properties of TI Indicator
        public string Action { get; set; }
        public string AdditionalInformation { get; set; }
        public List<string> ActivityGroupNames { get; set; }
        public int? Confidence { get; set; }
        public string Description { get; set; }
        public string DiamondModel { get; set; }
        public string EmailEncoding { get; set; }
        public string EmailLanguage { get; set; }
        public string EmailRecipient { get; set; }
        public string EmailSenderAddress { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSourceDomain { get; set; }
        public string EmailSourceIpAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailXMailer { get; set; }
        public DateTimeOffset? ExpirationDateTime { get; set; }
        public string ExternalId { get; set; }
        public DateTimeOffset? FileCompileDateTime { get; set; }
        public DateTimeOffset? FileCreatedDateTime { get; set; }
        public string FileHashType { get; set; }
        public string FileHashValue { get; set; }
        public string FileMutexName { get; set; }
        public string FileName { get; set; }
        public string FilePacker { get; set; }
        public string FilePath { get; set; }
        public long? FileSize { get; set; }
        public string FileType { get; set; }
        public string DomainName { get; set; }
        public string Id { get; set; }
        public DateTimeOffset? IngestedDateTime { get; set; }
        public bool? IsActive { get; set; }
        public List<string> KillChain { get; set; }
        public string KnownFalsePositives { get; set; }
        public DateTimeOffset? LastReportedDateTime { get; set; }
        public List<string> MalwareFamilyNames { get; set; }
        public string NetworkCidrBlock { get; set; }
        public int? NetworkDestinationAsn { get; set; }
        public string NetworkDestinationCidrBlock { get; set; }
        public string NetworkDestinationIPv4 { get; set; }
        public string NetworkDestinationIPv6 { get; set; }
        public int? NetworkDestinationPort { get; set; }
        public string NetworkIPv4 { get; set; }
        public string NetworkIPv6 { get; set; }
        public int? NetworkPort { get; set; }
        public int? NetworkProtocol { get; set; }
        public int? NetworkSourceAsn { get; set; }
        public string NetworkSourceCidrBlock { get; set; }
        public string NetworkSourceIPv4 { get; set; }
        public string NetworkSourceIPv6 { get; set; }
        public int? NetworkSourcePort { get; set; }
        public bool? PassiveOnly { get; set; }
        public int? Severity { get; set; }
        public List<string> Tags { get; set; }
        public string TargetProduct { get; set; }
        public string ThreatType { get; set; }
        public string TlpLevel { get; set; }
        public string Url { get; set; }
        public string UserAgent { get; set; }
    }
}