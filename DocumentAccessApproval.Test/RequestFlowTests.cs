using DocumentAccessApproval.BusinessLogic.Managers;
using DocumentAccessApproval.DataLayer;
using DocumentAccessApproval.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Test
{
    [TestFixture]
    public class RequestFlowTests
    {
        private AccessRequestManager _accessrequestManager;
        private UserManager _userManager;
        private DocumentManager _documentManager;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDb") // Use a shared name or random one per test
            .Options;

            var context = new DatabaseContext();

            context.Database.EnsureCreated();

            _accessrequestManager = new AccessRequestManager();
            _userManager = new UserManager();
            _documentManager = new DocumentManager();
        }

        [Test]
        public void ReadDocument_Correct()
        {

            var commonUser = _userManager.GetUser("commonUser");
            var approverUser = _userManager.GetUser("approverUser");
            var document = _documentManager.GetDocuments().FirstOrDefault();

            var accessRequest = new AccessRequest()
            {
                Id = Guid.NewGuid(),
                User = new User()
                {
                    Username = commonUser.Username
                },
                DocumentId = document.Id,
                AccessType = AccessType.Read,
                AccessReason = "Access for test purposes"
            };

            var requestId = _accessrequestManager.CreateAccessRequest(accessRequest);

            accessRequest = _accessrequestManager.GetAccessRequest(requestId);

            var decision = new Decision()
            {
                Status = Status.Approved
            };

            _accessrequestManager.UpdateAccessRequestDecision(requestId, approverUser.Username, decision);

            var resultDocument = _documentManager.GetDocument(document.Id, commonUser.Username);

            Assert.That(resultDocument, Is.Not.Null);

        }
    }
}
