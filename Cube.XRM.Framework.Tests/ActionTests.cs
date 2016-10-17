using Cube.XRM.Framework.Core;
using Cube.XRM.Framework.Interfaces;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cube.XRM.Framework.Tests
{
    public class ActionTests
    {

        [Fact]
        public void Should_create_a_new_entity_when_calling_create()
        {
            var ctx = new XrmFakedContext();
            var logSystem = A.Fake<IDetailedLog>();
            var service = ctx.GetOrganizationService();

            //Arrange
            var actions = new Actions(logSystem, service);
            var contact = new Entity("contact");
            contact["fullname"] = "Lionel Messi";

            //Act
            actions.Create(contact);

            //Assert
            var contactCreated = ctx.CreateQuery("contact").FirstOrDefault();
            Assert.NotNull(contactCreated);
            Assert.Equal(contactCreated["fullname"], "Lionel Messi");
        }

        [Fact]
        public void Should_update_an_entity_when_calling_update()
        {
            var ctx = new XrmFakedContext();
            var logSystem = A.Fake<IDetailedLog>();
            var service = ctx.GetOrganizationService();

            //Arrange
            var contact = new Entity("contact") { Id = Guid.NewGuid() };
            contact["fullname"] = "Lionel Messi";

            ctx.Initialize(new Entity[]
            {
                contact
            });

            //Act
            var contactToUpdate = new Entity("contact") { Id = contact.Id };
            contactToUpdate["fullname"] = "Luis Suárez";

            var actions = new Actions(logSystem, service);
            actions.Update(contactToUpdate);

            //Assert
            var contacts = ctx.CreateQuery("contact").ToList();
            Assert.Equal(1, contacts.Count);
            Assert.Equal(contacts[0]["fullname"], "Luis Suárez");
        }

        [Fact]
        public void Should_delete_an_entisting_record_when_calling_delete()
        {
            var ctx = new XrmFakedContext();
            var logSystem = A.Fake<IDetailedLog>();
            var service = ctx.GetOrganizationService();

            //Arrange
            var contact = new Entity("contact") { Id = Guid.NewGuid() };
            contact["fullname"] = "Lionel Messi";

            ctx.Initialize(new Entity[]
            {
                contact
            });

            //Act
            var actions = new Actions(logSystem, service);
            actions.Delete(contact.Id, "contact");

            //Assert
            var contacts = ctx.CreateQuery("contact").ToList();
            Assert.Equal(0, contacts.Count);
        }
    }
}
