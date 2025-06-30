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

            _accessrequestManager = new AccessRequestManager(context);
            _userManager = new UserManager(context);
            _documentManager = new DocumentManager(context);
        }

        [Test]
        public async Task ReadDocument_Correct()
        {
            var commonUser = await _userManager.GetUserAsync("commonUser");
            var approverUser = await _userManager.GetUserAsync("approverUser");
            var document = (await _documentManager.GetDocumentsAsync()).FirstOrDefault();

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

            var requestId = await _accessrequestManager.CreateAccessRequestAsync(accessRequest);

            accessRequest = await _accessrequestManager.GetAccessRequestAsync(requestId);

            var decision = new Decision()
            {
                Status = Status.Approved,
                Reason = "Test of getting document"
            };

            await _accessrequestManager.UpdateAccessRequestDecisionAsync(requestId, approverUser.Username, decision);

            var resultDocument = await _documentManager.GetDocumentAsync(document.Id, commonUser.Username);

            Assert.That(resultDocument, Is.Not.Null);

        }
    }
}
