using AutoMoq;
using Moq;

namespace Bennington.Login.Tests
{
    public class with_automoqer
    {
        public static AutoMoqer mocker;

        public with_automoqer()
        {
            mocker = new AutoMoqer();
        }

        public static T Create<T>()
        {
            return mocker.Create<T>();
        }

        public static Mock<T> GetMock<T>() where T : class
        {
            return mocker.GetMock<T>();
        }

        public static void SetInstance<T>(T instance) where T : class
        {
            mocker.SetInstance(instance);
        }
    }
}