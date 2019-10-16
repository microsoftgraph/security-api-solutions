using System;
using System.Collections.Generic;
using System.Text;

namespace MSGraphSecurity
{
    public class Alert
    {
        public string ActivityGroupName { get; set; }
        public string MyProperty { get; set; }
    }
}


  //"activityGroupName": "String",
  //"assignedTo": "String",
  //"azureSubscriptionId": "String",
  //"azureTenantId": "String",
  //"category": "String",
  //"closedDateTime": "String (timestamp)",
  //"cloudAppStates": [{"@odata.type": "microsoft.graph.cloudAppSecurityState"}],
  //"comments": ["String"],
  //"confidence": 1024,
  //"createdDateTime": "String (timestamp)",
  //"description": "String",
  //"detectionIds": ["String"],
  //"eventDateTime": "String (timestamp)",
  //"feedback": "@odata.type: microsoft.graph.alertFeedback",
  //"fileStates": [{"@odata.type": "microsoft.graph.fileSecurityState"}],
  //"historyStates": [{"@odata.type": "microsoft.graph.alertHistoryState"}],
  //"hostStates": [{"@odata.type": "microsoft.graph.hostSecurityState"}],
  //"id": "String (identifier)",
  //"lastModifiedDateTime": "String (timestamp)",
  //"malwareStates": [{"@odata.type": "microsoft.graph.malwareState"}],
  //"networkConnections": [{"@odata.type": "microsoft.graph.networkConnection"}],
  //"processes": [{"@odata.type": "microsoft.graph.process"}],
  //"recommendedActions": ["String"],
  //"registryKeyStates": [{"@odata.type": "microsoft.graph.registryKeyState"}],
  //"severity": "@odata.type: microsoft.graph.alertSeverity",
  //"sourceMaterials": ["String"],
  //"status": "@odata.type: microsoft.graph.alertStatus",
  //"tags": ["String"],
  //"title": "String",
  //"triggers": [{"@odata.type": "microsoft.graph.alertTrigger"}],
  //"userStates": [{"@odata.type": "microsoft.graph.userSecurityState"}],
  //"vendorInformation": {"@odata.type": "microsoft.graph.securityVendorInformation"},
  //"vulnerabilityStates": [{"@odata.type": "microsoft.graph.vulnerabilityState"}]
