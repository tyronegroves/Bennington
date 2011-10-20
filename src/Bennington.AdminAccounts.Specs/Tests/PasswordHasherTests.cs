using AutoMoq;
using Bennington.AdminAccounts.Passwords;
using NUnit.Framework;
using Should;

namespace Bennington.AdminAccounts.Specs.Tests
{
    [TestFixture]
    public class PasswordHasherTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Returns_empty_string_when_passed_null()
        {
            var passwordHasher = mocker.Create<PasswordHasher>();

            passwordHasher.GetHash(null).ShouldEqual("");
        }

        [Test]
        public void Returns_empty_string_when_passed_empty_string()
        {
            var passwordHasher = mocker.Create<PasswordHasher>();

            passwordHasher.GetHash("").ShouldEqual("");
        }
    }
}