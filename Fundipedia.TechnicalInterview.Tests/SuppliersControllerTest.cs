using Fundipedia.TechnicalInterview.Controllers;
using Fundipedia.TechnicalInterview.Data.Context;
using Fundipedia.TechnicalInterview.Domain;
using Fundipedia.TechnicalInterview.Model.Supplier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using System.Security.Cryptography.Xml;

namespace Fundipedia.TechnicalInterview.Tests
{
    /*Just wrote unit test for the controller because the controller just calls the service. The unit test for the service is similar 
     * with the controller one.
     */
    public class SuppliersControllerTest
    {
        SuppliersController supplierController;
        ISupplierService supplierService;
        Guid ValidSupplierId;
        public SuppliersControllerTest()
        {
            var options = new DbContextOptionsBuilder<SupplierContext>()
                .UseInMemoryDatabase(databaseName: "SupplierDatabase")
                .Options;
            
            var context = new SupplierContext(options);
            supplierService = new SupplierService(context);

            //initialize data
            var supplier = new Supplier
            {
                Id = Guid.NewGuid(),
                ActivationDate = DateTime.Today.AddDays(1),
                Emails = new List<Email> { new Email { EmailAddress = "ggg@gmail.com" } },
                Phones = new List<Phone> { new Phone { PhoneNumber = "2323-23233" } }
            };
            supplierService.InsertSupplier(supplier);

            ValidSupplierId = supplier.Id;

            supplierController = new SuppliersController(supplierService);
        }

        [Fact]
        public void Get_All_Supplier_Success()
        {
            //Arrange

            //Act
            var result = supplierController.GetSupplier();
            var resultType = result.Result.Value;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Supplier>>(resultType);
            Assert.NotEmpty(resultType);
        }

        [Fact]
        public void GetById_Supplier_Success()
        {
            //Arrange
            Guid invalid_supplierId = Guid.NewGuid();

            //Act
            var errorResult = supplierController.GetSupplier(invalid_supplierId);
            var successResult = supplierController.GetSupplier(ValidSupplierId);
            var successModel = successResult.Result.Value;

            //Assert
            Assert.IsType<Supplier>(successModel);
            Assert.IsType<NotFoundResult>(errorResult.Result.Result);
            Assert.Equal(ValidSupplierId, successModel.Id);
        }

        [Fact]
        public void Add_Supplier_Success()
        {
            var supplierId = Guid.NewGuid();
            var supplier = new Supplier
            {
                Id = supplierId,
                ActivationDate = DateTime.Today.AddDays(1),
                Emails = new List<Email> { new Email { EmailAddress = "testest1@gmail.com" } },
                Phones = new List<Phone> { new Phone { PhoneNumber = "333-3432" } }
            };

            var response = supplierController.PostSupplier(supplier);
            Assert.IsType<CreatedAtActionResult>(response.Result);

            var createdSupplier = response.Result as CreatedAtActionResult;
            Assert.IsType<Supplier>(createdSupplier.Value);
            var addedValue = createdSupplier.Value as Supplier;
            Assert.Equal(supplierId, addedValue.Id);
        }

        [Fact]
        public void Delete_Employee_Success()
        {
            Guid invalid_suppId = Guid.NewGuid();

            var notFoundResult = supplierController.DeleteSupplier(invalid_suppId);
            var badrequestResult = supplierController.DeleteSupplier(ValidSupplierId);

            //Currently there is no way of deleting supplier because only supplier where activation delete is null can be deleted and
            //activation date never becomes null because it is datetime not nullable datetime so minimum date always goes to the datasource.
            

            //Assert
            Assert.IsType<BadRequestObjectResult>(badrequestResult.Result.Result);
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result.Result);
        }
    }
}