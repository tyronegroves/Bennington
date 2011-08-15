// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.6.1.0
//      SpecFlow Generator Version:1.6.0.0
//      Runtime Version:4.0.30319.235
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Bennington.AdminAccounts.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Admin Accounts")]
    public partial class AdminAccountsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AdminAccounts.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Admin Accounts", "In order to manage administrator accounts\r\nAs an administrator\r\nI want to be add," +
                    " edit, delete admin accounts using a nice interface", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Admin visits the admin account list page")]
        public virtual void AdminVisitsTheAdminAccountListPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Admin visits the admin account list page", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "FirstName",
                        "LastName"});
            table1.AddRow(new string[] {
                        "20C492E8-B610-43F8-B97A-BDD50C9C864E",
                        "John",
                        "Galt"});
#line 7
 testRunner.Given("the following admin accounts exist in the database", ((string)(null)), table1);
#line 10
 testRunner.When("the administrator visits the Admin Account list page");
#line 11
 testRunner.Then("he should see the Admin Account list page");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "FirstName",
                        "LastName",
                        "Id"});
            table2.AddRow(new string[] {
                        "John",
                        "Galt",
                        "20c492e8-b610-43f8-b97a-bdd50c9c864e"});
#line 12
 testRunner.And("he should see the following accounts on the list page", ((string)(null)), table2);
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Admin goes to the edit page for an admin account")]
        public virtual void AdminGoesToTheEditPageForAnAdminAccount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Admin goes to the edit page for an admin account", ((string[])(null)));
#line 16
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "FirstName",
                        "LastName",
                        "Username"});
            table3.AddRow(new string[] {
                        "1567DDA0-8FC1-45C5-B0D3-F9396DD9BDB8",
                        "Howard",
                        "Roark",
                        "hroark"});
#line 17
 testRunner.Given("the following admin accounts exist in the database", ((string)(null)), table3);
#line 20
 testRunner.When("the administrator visits the Admin Account edit page for \'1567DDA0-8FC1-45C5-B0D3" +
                    "-F9396DD9BDB8\'");
#line 21
 testRunner.Then("he should see the Admin Account edit page");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table4.AddRow(new string[] {
                        "Id",
                        "1567DDA0-8FC1-45C5-B0D3-F9396DD9BDB8"});
            table4.AddRow(new string[] {
                        "FirstName",
                        "Howard"});
            table4.AddRow(new string[] {
                        "LastName",
                        "Roark"});
            table4.AddRow(new string[] {
                        "Username",
                        "hroark"});
            table4.AddRow(new string[] {
                        "Password",
                        ""});
#line 22
 testRunner.And("he should see an admin account edit form with the following values", ((string)(null)), table4);
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Admin edits an admin account")]
        public virtual void AdminEditsAnAdminAccount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Admin edits an admin account", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "FirstName",
                        "LastName",
                        "Username"});
            table5.AddRow(new string[] {
                        "73977DAE-10FA-4311-95B1-9B3ECCA0023D",
                        "E",
                        "W",
                        "sdf"});
#line 31
 testRunner.Given("the following admin accounts exist in the database", ((string)(null)), table5);
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field",
                        "Value"});
            table6.AddRow(new string[] {
                        "Id",
                        "73977DAE-10FA-4311-95B1-9B3ECCA0023D"});
            table6.AddRow(new string[] {
                        "FirstName",
                        "Ellis"});
            table6.AddRow(new string[] {
                        "LastName",
                        "Wyatt"});
            table6.AddRow(new string[] {
                        "Username",
                        "wyattoil"});
            table6.AddRow(new string[] {
                        "Password",
                        "elliswyattoil"});
#line 34
 testRunner.When("the administrator submits the following Admin Account edit page", ((string)(null)), table6);
#line 41
 testRunner.Then("he should see the Admin Account edit page");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "FirstName",
                        "LastName",
                        "Username",
                        "Password"});
            table7.AddRow(new string[] {
                        "73977DAE-10FA-4311-95B1-9B3ECCA0023D",
                        "Ellis",
                        "Wyatt",
                        "wyattoil",
                        "upsrXq/NBgWdbsiDjl9dto6Dtu1Oba3wjYghQjOrGM0="});
#line 42
 testRunner.And("the following admin accounts should exist in the database", ((string)(null)), table7);
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
