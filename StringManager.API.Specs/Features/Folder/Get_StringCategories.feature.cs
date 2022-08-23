﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace StringManager.API.Specs.Features.Folder
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Get_StringCategoriesFeature : object, Xunit.IClassFixture<Get_StringCategoriesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Get_StringCategories.feature"
#line hidden
        
        public Get_StringCategoriesFeature(Get_StringCategoriesFeature.FixtureData fixtureData, StringManager_API_Specs_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/Folder", "Get_StringCategories", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
    #line hidden
            TechTalk.SpecFlow.Table table35 = new TechTalk.SpecFlow.Table(new string[] {
                        "AccessGroup",
                        "Id",
                        "Name",
                        "Description",
                        "AmountOfItemsInCategory"});
            table35.AddRow(new string[] {
                        "basic-list",
                        "1",
                        "BasicList",
                        "A basic list",
                        "100"});
            table35.AddRow(new string[] {
                        "special-list",
                        "2",
                        "SpecialList",
                        "A special list",
                        "5"});
#line 4
        testRunner.Given("that we have the following categories", ((string)(null)), table35, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table36 = new TechTalk.SpecFlow.Table(new string[] {
                        "CategoryId",
                        "Tag"});
            table36.AddRow(new string[] {
                        "1",
                        "Basic"});
            table36.AddRow(new string[] {
                        "1",
                        "CustomTag"});
            table36.AddRow(new string[] {
                        "2",
                        "Special"});
#line 8
        testRunner.And("that the categories have the following tags", ((string)(null)), table36, "And ");
#line hidden
            TechTalk.SpecFlow.Table table37 = new TechTalk.SpecFlow.Table(new string[] {
                        "Username",
                        "AccessGroup"});
            table37.AddRow(new string[] {
                        "basicUser",
                        "basic-list-r"});
            table37.AddRow(new string[] {
                        "adminUser",
                        "basic-list-crud"});
            table37.AddRow(new string[] {
                        "newUser",
                        "new-list-r"});
#line 13
        testRunner.And("that the users have the following access groups", ((string)(null)), table37, "And ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Get all available categories for a user with read access to the category")]
        [Xunit.TraitAttribute("FeatureTitle", "Get_StringCategories")]
        [Xunit.TraitAttribute("Description", "Get all available categories for a user with read access to the category")]
        [Xunit.InlineDataAttribute("basicUser", "basic-list-r", new string[0])]
        [Xunit.InlineDataAttribute("adminUser", "basic-list-crud", new string[0])]
        public void GetAllAvailableCategoriesForAUserWithReadAccessToTheCategory(string currentUser, string accessGroup, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("currentUser", currentUser);
            argumentsOfScenario.Add("accessGroup", accessGroup);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get all available categories for a user with read access to the category", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 19
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
    this.FeatureBackground();
#line hidden
#line 20
        testRunner.Given(string.Format("that the user \"{0}\" want to get all available categories", currentUser), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 21
        testRunner.When("the api gets the request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 22
        testRunner.Then(string.Format("all information about categories with acceess group \"{0}\" is returned", accessGroup), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 23
        testRunner.And("the reponse has the http status code \"200 Ok\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Get all categories when there is no categories available to the user")]
        [Xunit.TraitAttribute("FeatureTitle", "Get_StringCategories")]
        [Xunit.TraitAttribute("Description", "Get all categories when there is no categories available to the user")]
        public void GetAllCategoriesWhenThereIsNoCategoriesAvailableToTheUser()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get all categories when there is no categories available to the user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 30
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
    this.FeatureBackground();
#line hidden
#line 31
        testRunner.Given("that the user \"newUser\" want to get all available categories", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 32
        testRunner.When("the api gets the request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 33
        testRunner.Then("no categories are returned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 34
        testRunner.And("the response has the http status code \"200 Ok\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Get all categories for invalid user")]
        [Xunit.TraitAttribute("FeatureTitle", "Get_StringCategories")]
        [Xunit.TraitAttribute("Description", "Get all categories for invalid user")]
        public void GetAllCategoriesForInvalidUser()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get all categories for invalid user", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 36
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
    this.FeatureBackground();
#line hidden
#line 37
        testRunner.Given("that the user \"invalidUsername\" want to get all available categories", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 38
        testRunner.When("the api gets the request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table38 = new TechTalk.SpecFlow.Table(new string[] {
                            "ProblemType",
                            "Title",
                            "Detail"});
                table38.AddRow(new string[] {
                            "InvalidUserName",
                            "Invalid user",
                            "The user specified in the request is invalid"});
#line 39
        testRunner.Then("the following problem detail is returned", ((string)(null)), table38, "Then ");
#line hidden
#line 42
        testRunner.And("the response has the http status code \"400 Bad Request\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                Get_StringCategoriesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                Get_StringCategoriesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion