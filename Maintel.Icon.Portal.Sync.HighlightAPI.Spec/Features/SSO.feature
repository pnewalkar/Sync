Feature: HighlightAPI SSO Controller
	In order for the sync service to interact with the highlight api
	As an API developer
	I want to understand the SSO Controller

@sso @access
Scenario: Web service access 
	Given I have access to the api
	When I make a "get" request to "isalive" with payload ""
	Then I should receive a status of "OK"

@sso @authenticate
Scenario: Get an authentication token for SSO
	Given I have access to the portal database
	When I make a "POST" request to "SSO/authenticate" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}"
	Then the returned string should contain a value of "token"

@sso @decrypt
Scenario: Get a decrypted token for SSO
	Given I have access to the portal database
		And I make a "POST" request to "SSO/authenticate" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}"
	When I make a "POST" request to "SSO/decrypt" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}" enriching "token" with return from last call
	Then the returned string should not be empty

@sso @cookie @current
Scenario: Get a cookie for SSO
	Given I have access to the portal database
		And I make a "POST" request to "SSO/authenticate" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}"
		And I make a "POST" request to "SSO/decrypt" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}" enriching "token" with return from last call
	When I make a "POST" request to "SSO/cookie" with payload "{'partner': 'Maintel Support', 'uid': 'alastair.little@maintel.co.uk', 'token': '', 'cookie': ''}" enriching "token" with return from last call
	Then the returned string should contain a value of "cookie"

@sso @authenticate @bad
Scenario Outline: Get an authentication token for SSO with bad partner and user combinations
	Given I have access to the portal database
	When I make a "POST" request to "SSO/authenticate" with payload "{'partner': '<partner>', 'uid': '<uid>', 'token': '', 'cookie': ''}"
	Then the returned string should contain a value of "ERROR"
	Examples: 
	| partner 			| uid 								|
	| BadPartner 		| alastair.little@maintel.co.uk 	|
	| Maintel Support 	| baduser@baduser.co.uk 			|

@sso @decrypt @bad
Scenario Outline: Get a decrypted authentication token for SSO with a bad token
	Given I have access to the portal database
	When I make a "POST" request to "SSO/decrypt" with payload "{'partner': '', 'uid': '', 'token': '<token>', 'cookie': ''}"
	Then  I should receive a status of "NotFound"
	Examples: 
	| token 			|
	| somerandomtoken 	|

@sso @cookie @bad
Scenario Outline: Get an cookie for SSO with bad token
	Given I have access to the portal database
	When I make a "POST" request to "SSO/cookie" with payload "{'partner': '<partner>', 'uid': '<uid>', 'token': '<token>', 'cookie': ''}"
	Then the returned string should contain a value of "ERROR"
	Examples: 
	| partner 			| uid 								| token 	|
	| BadPartner 		| alastair.little@maintel.co.uk 	| badtoken	|
	| Maintel Support 	| baduser@baduser.co.uk 			| badtoken  |
	| Maintel Support	| alastair.little@maintel.co.uk 	| badtoken  |
	| Maintel Support 	| alastair.little@maintel.co.uk		| badtoken  |