using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tba.Core.Utilities;

namespace tba.CoreUnitTests
{
    [TestClass]
    public class DateTimeExtensionsUnitTests
    {
        private const long UserId = 666;

        [TestMethod]
        public void FromUnixTimestamp_ShouldCreateDate()
        {
            // Arrange
            const int date = 820800; // Jan 10, 1970 12:00:00

            // Act
            var uit = DateTimeExtensions.FromUnixTimestamp(date);

            // Assert
            Assert.AreEqual(1970, uit.Year);
            Assert.AreEqual(1, uit.Month);
            Assert.AreEqual(10, uit.Day);
            Assert.AreEqual(12, uit.Hour);
            Assert.AreEqual(0, uit.Minute);
            Assert.AreEqual(0, uit.Second);
        }

        [TestMethod]
        public void FromUnixTimestamp_ShouldCreateNull_FromNull()
        {
            // Arrange

            // Act
            var uit = DateTimeExtensions.FromUnixTimestamp(null);

            // Assert
            Assert.IsNull(uit);
        }

        [TestMethod]
        public void ToUnixTimestamp_ShouldCreateUnixDate()
        {
            // Arrange
            var date = new DateTime(1970, 1, 10, 12, 0, 0);

            // Act
            var uit = date.ToUnixTimestamp();

            // Assert
            Assert.AreEqual(820800, uit);
        }

    }
}