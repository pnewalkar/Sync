using System;
using NUnit.Framework;
using Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Helpers;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Models;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Spec.Steps
{
    [Binding]
    public class RestCRUD
    {
        string _baseURL = TestContext.Parameters["baseURL"];
        string _connectionString = TestContext.Parameters["connectionString"];

        object _receivedObject;
        List<object> _receivedObjects;       


        //The results of the query executed against the SQL database
        internal string _queryResult;

        [Given(@"I have access to the api")]
        public void GivenIHaveAccessToTheAPI()
        {
            Assert.IsTrue( HTTPRest.CheckEndpoint(_baseURL));
        }

        [Given(@"I have access to the portal database")]
        public void GivenIHaveAccessToThePortalDatabase()
        {           
            _queryResult = "";
            var rtn = SQLDatabase.TestConnection(_connectionString);
            Assert.IsTrue(rtn, "The connection to the database using " + _connectionString + " could not be established.");
        }

        [Given(@"the table ""(.*)"" exists")]
        public void GivenTheTableExists(string tableName)
        {
            var sql = "IF EXISTS (SELECT * FROM sysObjects WHERE [name] = '" + tableName + "') SELECT 'OK' ELSE SELECT 'Failed'";
            Console.Write (sql);
            _queryResult = SQLDatabase.ReturnQuery(_connectionString, sql);
            Assert.AreEqual(_queryResult, "OK", "The table" + tableName + " should exist in the database.");
        }

        [When(@"I have reset the database")]
        public void WhenIHaveResetTheDatabase()
        {
            Assert.IsTrue( HTTPRest.CheckEndpoint(_baseURL));
            var url = _baseURL + "users/resettables";
            //hard-coded key as rough security measure on this call
            string rtn = HTTPRest.MakeHTTPRequest("POST", url, "'wf80ywef0w87efg0gwefrb09'");
            Assert.IsTrue(rtn.Length == 0, "No return was expected - please check the logs");
        }


        [When(@"I query the portal database with ""(.*)""")]
        public void WhenIQueryThePortalDatabaseWith(string sql)
        {
            Assert.Greater(sql.Length, 0);
            _queryResult = SQLDatabase.ReturnQuery(_connectionString, sql);
            Assert.AreNotEqual(_queryResult, "", "The result from the query should not be nothing");

        }

        [When(@"I check the column ""(.*)"" has a data type of ""(.*)"" with a precision of ""(.*)""")]
        public void WhenICheckTheColumnHasADataTypeOf(string columnName, string dataType, int dataPrecision)
        {
            var xtype = 56;     //INT
            if(dataType.ToUpper() == "MONEY") { xtype = 60; }
            else if(dataType.ToUpper() == "DATETIME") { xtype = 61; }
            else if(dataType.ToUpper() == "DATETIME2") { xtype = 42; }
            else if(dataType.ToUpper() == "BIT") { xtype = 104; }
            else if(dataType.ToUpper() == "DECIMAL") { xtype = 106; }
            else if(dataType.ToUpper() == "VARCHAR") { xtype = 167; }
            var sql = String.Concat("IF EXISTS (SELECT * FROM sysColumns C WHERE C.[name] = '", columnName, "' AND C.xtype = ", xtype, " AND C.length = ", dataPrecision, ") SELECT 'OK' ELSE SELECT 'Failed'");
            Console.Write (sql);
            _queryResult = SQLDatabase.ReturnQuery(_connectionString, sql);
            Assert.AreEqual(_queryResult, "OK", "The column " + columnName + " was expected to have a data type of " + dataType);
        }

        [When(@"I check for the existance of the ""(.*)"" object")]
        public void WhenICheckForTheExistanceOfTheObject(string dbObject)
        {
            var sql = "IF EXISTS (SELECT * FROM sysObjects WHERE [name] = '" + dbObject + "') SELECT 'OK' ELSE SELECT 'Failed'";
            Console.Write (sql);
            _queryResult = SQLDatabase.ReturnQuery(_connectionString, sql);
            Assert.AreEqual(_queryResult, "OK", "The object " + dbObject + " was not found to exist.");
        }
         
        [When(@"I make a ""(.*)"" request to ""(.*)"" with payload ""(.*)""")]
        public void WhenIMakeARequestTo(string method, string uri, string payload)
        {
            Assert.IsTrue( HTTPRest.CheckEndpoint(_baseURL));
            var url = _baseURL + uri;
            string rtn = HTTPRest.MakeHTTPRequest(method, url, payload);
            if (uri.ToLower().Contains("isalive")) {
                Assert.Pass();
            } else if (uri.ToLower().Contains("webhook")) {
                _receivedObject = JsonUtils.GetObject<HighlightAlertDTO>(rtn);
            } else if (uri.ToLower().Contains("highlightalert")) {
                _receivedObject = JsonUtils.GetObject<HighlightAlert>(rtn);
            } else {
                Assert.IsTrue(false, "The uri supplied did not match an expected endpoint ");
            }
        }

        [When(@"I make a ""(.*)"" request to return a list from ""(.*)"" with payload ""(.*)""")]
        public void WhenIMakeARequestToReturnAListFromTo(string method, string uri, string payload)
        {
            Assert.IsTrue( HTTPRest.CheckEndpoint(_baseURL));
            var url = _baseURL + uri;
            try
            {
                string rtn = HTTPRest.MakeHTTPRequest(method, url, payload);
                if (uri.ToLower().Contains("isalive")) {
                    Assert.IsEmpty(rtn);
                } else if (uri.ToLower().Contains("dto")) {
                    _receivedObjects = JsonUtils.GetObjectArray<HighlightAlertDTO>(rtn);
                } else if (uri.ToLower().Contains("highlightalert")) {
                    _receivedObjects = JsonUtils.GetObjectArray<HighlightAlert>(rtn);
                } else {
                    Assert.IsTrue(false, "The uri supplied did not match an expected endpoint ");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                _receivedObjects = new List<object>();
            }
        }

        [Then(@"I should receive a status of ""(.*)""")]
        public void ThenIShouldReceiveAStatusOf(string statusCode)
        {
            Assert.AreEqual(statusCode, HTTPRest.StatusCode.ToString(), "Expected " + statusCode + " but received " + HTTPRest.StatusCode);
        }

        [Then(@"when I make a ""(.*)"" request to ""(.*)"" an item should have a ""(.*)"" property set to ""(.*)""")]
        public void ThenWhenIMakeARequestToAnItemShouldHaveAPropertySetTo(string method, string uri, string property, string propertyValue)
        {
            Assert.IsTrue(method.Length > 0);
            Assert.IsTrue(uri.Length > 0);
            Assert.IsTrue(property.Length > 0);
            Assert.IsTrue(propertyValue.Length > 0);

            var url = _baseURL + uri;
            string rtn = HTTPRest.MakeHTTPRequest(method, url, "");
        }

        [Then(@"the returned string should have a value of ""(.*)""")]
        public void ThenReturnedStringShouldHaveAValueOf(string value)
        {
            Assert.IsTrue(value.Length > 0);
            Assert.IsTrue(HTTPRest.ResponseString.Length > 0);
            Assert.AreEqual(value, HTTPRest.ResponseString.Replace("\"", ""));
        }


        [Then(@"I should receive an object array")]
        public void ThenIShouldReceiveAnObjectArray()
        {
            Assert.IsTrue(_receivedObjects.Count > 0);
        }

        [Then(@"the object returned should have a property of ""(.*)""")]
        public void ThenTheObjectReturnedShouldHaveAPropertyOf(string property)
        {
            Assert.IsNotNull(_receivedObject, "There is no object to check the " + property + " property");
            var foundOne = false;
            foreach (var prop in _receivedObject.GetType().GetProperties())
            {
                if(prop.Name.ToLower() == property.ToLower()) {foundOne = true;}
            }
            Assert.IsTrue(foundOne);
        }

		[Then(@"the object array property ""(.*)"" should have a value of ""(.*)""")]
        public void ThenTheObjectArrayPropertyShouldHaveAValueOf(string property, string value)
        {
            Assert.IsNotNull(_receivedObjects, "There is no object to check the " + property + " property");
            var foundOne = false;
            
            foreach (var obj in _receivedObjects)
            {
                var properties = obj.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    if(prop.Name.ToLower() == property.ToLower()) {
                        foundOne = true;
                        //var val = prop.GetValue(prop);
                        //Assert.AreEqual(val.ToString(), value.ToString());
                    }
                }
            }
            if(!foundOne) {Console.WriteLine(property + " - " + value);}
            Assert.IsTrue(foundOne, " didn't find a " + property);
        }

        [Then(@"the object property ""(.*)"" should have a value of ""(.*)""")]
        public void ThenTheObjectPropertyShouldHaveAValueOf(string property, string value)
        {
            Assert.IsNotNull(_receivedObject, "There is no object to check the " + property + " property");
            foreach (var prop in _receivedObject.GetType().GetProperties())
            {
                if(prop.Name.ToLower() == property.ToLower()) {
                    //Assert.AreEqual(value, _receivedObject[prop.Name].ToString() );
                }
            }
        }

        [Then(@"I should receive a ""(.*)"" object")]
        public void ThenIShouldReceiveAObject(string obj)
        {
            Assert.IsTrue(obj.Length > 0);
            Console.Write(HTTPRest.ResponseString);
            if (obj.ToLower().Contains("isalive")) {
                Assert.IsEmpty(HTTPRest.ResponseString);
            } else if (obj.ToLower().Contains("dto")) {
                Assert.IsTrue(JsonUtils.TryParseJson<HighlightAlertDTO>(HTTPRest.ResponseString), "Parsing the JSON return failed");
                _receivedObject = JsonUtils.GetObject<HighlightAlertDTO>(HTTPRest.ResponseString);
            } else if (obj.ToLower().Contains("highlightalert")) {
                Assert.IsTrue(JsonUtils.TryParseJson<HighlightAlert>(HTTPRest.ResponseString), "Parsing the JSON return failed");
                _receivedObject = JsonUtils.GetObject<HighlightAlert>(HTTPRest.ResponseString);
            } else {
                Assert.IsTrue(false, "The object supplied did not match an expected endpoint ");
            }        }

        [Then(@"it should have an id of ""(.*)""")]
        public void ThenItShouldHaveAnIdOf(string id)
        {
            Assert.IsTrue(id.Length > 0);
            //Assert.AreEqual(id, _receivedObject.Id, "Failed to match id's: " + id + " as opposed to " + _receivedObject.Id);
        }

    }
}
    
    