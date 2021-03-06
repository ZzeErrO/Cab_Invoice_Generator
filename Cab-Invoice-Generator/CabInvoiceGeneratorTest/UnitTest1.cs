using CabInvoiceGenerator;
using NUnit.Framework;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator = null;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 25;
            Assert.AreEqual(expected, fare);
        }

        [Test]
        public void GivenMultipleRideShouldReturnInvoiceSummary()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.0, 5), new Ride(0.1, 5) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 31.0);
            Assert.AreEqual(expectedSummary.totalFare, summary.totalFare);
        }

        [Test]
        public void GivenMultipleRidesAndFareShouldReturnEnhancedInvoice()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.1, 5), new Ride(1, 10) };
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            InvoiceSummary expectedSummary = new InvoiceSummary(2, 46);
            Assert.AreEqual(true, summary.Equals(expectedSummary));
        }

        [Test]
        public void GivenAUserIdShouldReturnInvoice()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            Ride[] rides = { new Ride(2.1, 5), new Ride(1, 10) };
            Ride[] rides1 = { new Ride(2.0, 5), new Ride(0.1, 5), new Ride(1.1, 6) };
            Ride[] rides2 = { new Ride(3.2, 3), new Ride(0.2, 2) };
            invoiceGenerator.rideRepository.AddRide("1", rides);
            invoiceGenerator.rideRepository.AddRide("2", rides1);
            invoiceGenerator.rideRepository.AddRide("3", rides2);
            InvoiceSummary expectedSummary = new InvoiceSummary(3, 48);
            Assert.AreEqual(expectedSummary, invoiceGenerator.GetInvoiceSummary("2"));
        }

        [Test]
        public void GivenDistanceAndTimeShouldReturnTotalByPremuimFare()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            double distance = 2.0;
            int time = 5;
            double fare = invoiceGenerator.CalculateFare(distance, time);
            double expected = 40;

            InvoiceGenerator invoiceGenerator1 = new InvoiceGenerator(RideType.NORMAL);
            double distance1 = 2.0;
            int time1 = 5;
            double fare1 = invoiceGenerator1.CalculateFare(distance1, time1);
            double expected1 = 25;

            Assert.AreEqual(expected, fare);
            Assert.AreEqual(expected1, fare1);
        }

    }

}