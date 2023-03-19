using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {

        private ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoServiceSoapType;

        [TestInitialize]
        public async Task Initialize()
        {
            countryInfoServiceSoapType = new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public async Task Validate_ListCountryNamesByCode()
        {
            var countryList = countryInfoServiceSoapType.ListOfCountryNamesByCode();

            var countryListAsc = countryList.OrderBy(x => x.sISOCode);

            Assert.IsTrue(Enumerable.SequenceEqual(countryList, countryListAsc), "Country code is not in ascending order");

        }

        [TestMethod]
        public async Task Validate_CountryName()
        {
            var countryName = countryInfoServiceSoapType.CountryName("??");

            var responseMessage = "Country not found in the database";

            Assert.AreEqual(responseMessage.ToLower(), countryName.ToLower());

            

        }


        [TestMethod]
        public async Task Validate_LastEntryCountry()
        {
            var countryCode = countryInfoServiceSoapType.ListOfCountryNamesByCode().Last();

            var countryName = countryInfoServiceSoapType.CountryName(countryCode.sISOCode);

            Assert.AreEqual(countryCode.sName, countryName);


        }
    }
}