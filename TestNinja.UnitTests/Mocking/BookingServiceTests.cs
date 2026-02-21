using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingServiceTests
    {
        private Mock<IBookingRepository> _repository;
        private Booking _booking;

        [SetUp]
        public void SetUp()
        {
             _booking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
             };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r=>r.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                _booking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingOverlapButBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.ArrivalDate),
                Status = "Cancelled"
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_StartAndEndBeforeExistingBooking_ReturnEmpty()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate, day: 2),
                DepartureDate = Before(_booking.ArrivalDate),
                Reference = "a"
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_ArrivalDateOverlap_ReturnOverlappingReference()
        {
            var bookingList = new List<Booking>()
            {
                new Booking()
                {
                    ArrivalDate = DateTime.Now,
                    DepartureDate = DateTime.Now.AddDays(1),
                    Reference = "reference"
                },
            }.AsQueryable();

            _repository.Setup((br) => br.GetActiveBookings(1)).Returns(bookingList);

            var booking = new Booking()
            {
                Id = 1,
                ArrivalDate = DateTime.Now,
                DepartureDate = DateTime.Now
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.That(result, Is.EqualTo("reference"));
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int day = 1)
        {
            return dateTime.AddDays(-day);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        [Test]
        public void OverlappingBookingsExist_StartsBeforeAndFinishesInTheMidleOfExistingBooking_ReturnReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.ArrivalDate),
                Reference = "a"
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsBeforeAndFinishesAfterTheExistingBooking_ReturnReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsAndfinishedIntheMidlleOfTheExistingBooking_ReturnReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = Before(_booking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_StartsIntheMidlleOfTheExistingBookingButFinishesAfter_ReturnReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }


        [Test]
        public void OverlappingBookingsExist_StartsAndfinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_booking.DepartureDate),
                DepartureDate = After(_booking.DepartureDate, days: 2),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}
