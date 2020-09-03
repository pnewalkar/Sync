Feature: HighlightAPI Webhook Controller
	In order for the sync service to interact with the highlight api
	As an API developer
	I want to understand the Webhook Controller

@webhook @access
Scenario: Web service access 
	Given I have access to the api
	When I make a "get" request to "isalive" with payload ""
	Then I should receive a status of "OK"

@webhook @add
Scenario Outline: New Webhook
	Given I have access to the api
	When I make a "post" request to "webhook/927t23c4m28934yd12c0mc10v345018y42xp3sm" with payload "{'DateSent': '<DateSent>', 'broadbandDownstreamSpeedKbps':<broadbandDownstreamSpeedKbps>,'broadbandAlertThresholdKbps':<broadbandAlertThresholdKbps>,'deviceAddress':'<deviceAddress>','folder':'<folder>','hasStabilityIssue':<hasStabilityIssue>,'isBroadbandSpeedAlert':<isBroadbandSpeedAlert>, 'isWirelessAccessPoint':<isWirelessAccessPoint>, 'linkUrl':'<linkUrl>', 'locationName':'<locationName>', 'watchName':'<watchName>', 'problem':'<problem>', 'referenceText':'<referenceText>', 'siteLinksUp':<siteLinksUp>, 'stabilityIssueCode':'<stabilityIssueCode>', 'stabilityIssueDescription':'<stabilityIssueDescription>', 'alertSummary':'<alertSummary>', 'timeStampUtc':'<timeStampUtc>', 'wapLocation':'<wapLocation>', 'wapSerialNumber':'<wapSerialNumber>', 'watchTypeName':'<watchTypeName>'}"
	Then I should receive a status of "NoContent"
Examples:
	| broadbandDownstreamSpeedKbps 	| broadbandAlertThresholdKbps 	| deviceAddress | folder 						| hasStabilityIssue | isBroadbandSpeedAlert	| isWirelessAccessPoint	| linkUrl																													| locationName	| watchName		| problem								| referenceText		| siteLinksUp	| stabilityIssueCode	| stabilityIssueDescription			| alertSummary														| timeStampUtc						| wapLocation	| wapSerialNumber	| watchTypeName	| DateSent 							|
	| 5272 							| 2048 							| 10.0.0.1		| KFV Services >> DataCentre 	| true				| false					| false					| https://kfv-services.highlighter.net/portal/index.html#/details/landing/19739/23298/%257B%2522today%2522%253Atrue%257D	| Head Office	| HO-Secondary	| Link-Availability - Red alert raised	| 1067504-1718672	| 1				| #DRP					| Device restarted powered off/on	| KFV Services - Red Alert Raised - LinkHealthADSL (HO-Secondary)	| 2020-02-14T10:23:26.4304015+00:00	| WapLocation	| wapSerialNumber	| watchTypeName	| 2020-02-14T10:23:26.4304015+00:00 |

@webhook @get
Scenario Outline: Confirm a webhook entry
	Given I have access to the portal database
	When I query the portal database with "SELECT TOP 1 * FROM [Highlight].[WebhookAlerts] WHERE [Folder]='<folder>' AND [HasStabilityIssue] = '<hasStabilityIssue>' AND [IsBroadbandSpeedAlert] = '<isBroadbandSpeedAlert>' AND [IsWirelessAccessPoint] = '<isWirelessAccessPoint>' AND [LinkUrl] = '<linkUrl>' AND [Location] = '<locationName>' AND [WatchName] ='<watchName>' AND [Problem] = '<problem>' AND [ReferenceText] = '<referenceText>' AND [SiteLinksUp] = '<siteLinksUp>' AND [AlertSummary] = '<alertSummary>' AND [WatchTypeName] = '<watchTypeName>'"
	Then I should receive a status of "OK"
Examples:
	| broadbandDownstreamSpeedKbps 	| broadbandAlertThresholdKbps 	| deviceAddress | folder 						| hasStabilityIssue | isBroadbandSpeedAlert	| isWirelessAccessPoint	| linkUrl																													| locationName	| watchName		| problem								| referenceText		| siteLinksUp	| stabilityIssueCode	| stabilityIssueDescription			| alertSummary														| timeStampUtc						| wapLocation	| wapSerialNumber	| watchTypeName	|
	| 5272 							| 2048 							| 10.0.0.1		| KFV Services >> DataCentre 	| true				| false					| false					| https://kfv-services.highlighter.net/portal/index.html#/details/landing/19739/23298/%257B%2522today%2522%253Atrue%257D	| Head Office	| HO-Secondary	| Link-Availability - Red alert raised	| 1067504-1718672	| 1				| #DRP					| Device restarted powered off/on	| KFV Services - Red Alert Raised - LinkHealthADSL (HO-Secondary)	| 2020-02-14T10:23:26.4304015+00:00	| WapLocation	| wapSerialNumber	| watchTypeName	|


@webhook @add @bad
Scenario Outline: New Webhook with bad key
	Given I have access to the api
	When I make a "post" request to "webhook/badkey" with payload "{'DateSent': '<DateSent>', 'broadbandDownstreamSpeedKbps':<broadbandDownstreamSpeedKbps>,'broadbandAlertThresholdKbps':<broadbandAlertThresholdKbps>,'deviceAddress':'<deviceAddress>','folder':'<folder>','hasStabilityIssue':<hasStabilityIssue>,'isBroadbandSpeedAlert':<isBroadbandSpeedAlert>, 'isWirelessAccessPoint':<isWirelessAccessPoint>, 'linkUrl':'<linkUrl>', 'locationName':'<locationName>', 'watchName':'<watchName>', 'problem':'<problem>', 'referenceText':'<referenceText>', 'siteLinksUp':<siteLinksUp>, 'stabilityIssueCode':'<stabilityIssueCode>', 'stabilityIssueDescription':'<stabilityIssueDescription>', 'alertSummary':'<alertSummary>', 'timeStampUtc':'<timeStampUtc>', 'wapLocation':'<wapLocation>', 'wapSerialNumber':'<wapSerialNumber>', 'watchTypeName':'<watchTypeName>'}"
	Then I should receive a status of "BadRequest"
Examples:
	| broadbandDownstreamSpeedKbps 	| broadbandAlertThresholdKbps 	| deviceAddress | folder 						| hasStabilityIssue | isBroadbandSpeedAlert	| isWirelessAccessPoint	| linkUrl																													| locationName	| watchName		| problem								| referenceText		| siteLinksUp	| stabilityIssueCode	| stabilityIssueDescription			| alertSummary														| timeStampUtc						| wapLocation	| wapSerialNumber	| watchTypeName	| DateSent 							|
	| 5272 							| 2048 							| 10.0.0.1		| KFV Services >> DataCentre 	| true				| false					| false					| https://kfv-services.highlighter.net/portal/index.html#/details/landing/19739/23298/%257B%2522today%2522%253Atrue%257D	| Head Office	| HO-Secondary	| Link-Availability - Red alert raised	| 1067504-1718672	| 1				| #DRP					| Device restarted powered off/on	| KFV Services - Red Alert Raised - LinkHealthADSL (HO-Secondary)	| 2020-02-14T10:23:26.4304015+00:00	| WapLocation	| wapSerialNumber	| watchTypeName	| 2020-02-14T10:23:26.4304015+00:00 |

