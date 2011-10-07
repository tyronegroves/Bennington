using AutoMoq;
using Bennington.AdminAccounts.Models;
using NUnit.Framework;

namespace Bennington.AdminAccounts.Specs.Tests
{
    [TestFixture]
    public class AdminAccountListPageViewModelMapperTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        //[Test]
        //public void Assert_configuration_is_valid()
        //{
        //    var mapper = mocker.Create<AdminAccountListPageViewModelMapper>();
        //    mapper.AssertConfigurationIsValid();
        //}
    }
}