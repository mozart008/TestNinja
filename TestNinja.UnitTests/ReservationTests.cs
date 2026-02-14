using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {

        [Test]
        public void CanBeCanceledBy_AdminCancelling_ReturnsTrue()
        {
            //arrange
            var reservation = new Reservation();

            //act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });
            
            //assert
            Assert.IsTrue(result);
            Assert.That(result, Is.True);
        }


        [Test]
        public void CanBeCanceledBy_NotAdminCancelling_ReturnsFalse()
        {
            //arrange
            var reservation = new Reservation();

            //act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanBeCanceledBy_SameUserCancelling_ReturnsTrue()
        {
            //arrange
            var user = new User() { IsAdmin = false };
            var reservation = new Reservation()
            {
                MadeBy = user
            };

            //act
            var result = reservation.CanBeCancelledBy(user);

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCanceledBy_AnotherUserCancelling_ReturnsFalse()
        {
            //arrange
            var reservation = new Reservation()
            {
                MadeBy = new User()
            };

            //act
            var result = reservation.CanBeCancelledBy(new User());

            //assert
            Assert.IsFalse(result);
        }
    }
}
