using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
             _helper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_BlankParameters_ReturnFalse()
        {
            _fileDownloader.Setup((fd) => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws(new WebException());

            var result = _helper.DownloadInstaller(string.Empty, string.Empty);

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnTrue()
        {
            _fileDownloader.Setup((fd) => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()));

            var result = _helper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader.Setup((fd) => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _helper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }
    }
}
