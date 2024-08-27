
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Fundipedia.TechnicalInterview.Model.Extensions;

namespace Fundipedia.TechnicalInterview.Tests
{
   
    public class SupplierExtensionTest
    {
        

        [Fact]
        public void Is_Supplier_Active_Success()
        {
            //Fact
            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                Emails = new List<Email> { new Email { EmailAddress = "ggg@gmail.com" } },
                Phones = new List<Phone> { new Phone { PhoneNumber = "2323-23233" } }
            };
            //Action
            var isActive = supplier.IsActive();
            //Assert
            Assert.True(isActive);

            //Note: can't test not active scenario unless the ActivationDate is made nullable type. 
            //Not active is when the activationdate is null and it can't be set to null as it is not nullable type.
            //I did not want change how it was set up.

        }


    }
}