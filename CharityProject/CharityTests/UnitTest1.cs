using Charities.Data.Models;
using CharityProject.Contracts;
using CharityProject.Data;
using CharityProject.Models;
using CharityProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CharityTests
{
    public class Tests
    {

        [TestFixture]
        public class CaseServiceTests
        {
            private IEnumerable<Category> categories;
            private IEnumerable<Charity> charities;
            private IEnumerable<User> users;
            private ApplicationDbContext context;
            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "InMemoryDb")
                    .Options;
                this.context = new ApplicationDbContext(options);
                if (this.context.Users.ToList().Count == 0)
                {
                    this.categories = new List<Category>()
                    {
                        new Category(){Id = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548") , Name="Category1"},
                        new Category(){Id = Guid.Parse("429e7889-d7df-4933-bbc3-5544dbbb278a"), Name="Category2" }
                    };

                    this.charities = new List<Charity>()
                {
                    new Charity(){
                        CategoryId=Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                        CreatorId="a",
                        FundsNeeded=200,
                        ImageUrl="https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                        Name = "TestCharity",
                        Location = "Kazanlak",
                        Description = "This is a test charity."
                    }
                };

                    this.users = new List<User>()
                {
                    new User(){Id="a", Email="gogo@abv.bg", UserName = "gosko"},
                    new User(){Id="b", Email="gogo1@abv.bg", UserName = "gosko1"},
                    new User(){Id="c", Email="gogo2@abv.bg", UserName = "gosko2"},
                };
                    this.context.AddRange(categories);
                    this.context.AddRange(users);
                    this.context.AddRange(charities);
                    this.context.SaveChanges();
                }
            }
            [Test]
            public async Task CreateCharityMethodTestedWithCorrectModelAndIncorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "TestName",
                    Description = "SomeDescription",
                    FundsNeeded = 5000,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                    Location = "Kazanlak",
                };

                Assert.That(async () => await caseService.CreateCharity(correctModel, "d"), Throws.ArgumentException);
            }
            [Test]
            public async Task CreateCharityMethodTestedWithCorrectModelAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "TestName",
                    Description = "SomeDescription",
                    FundsNeeded = 5000,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                    Location = "Kazanlak",
                };

                var result = await caseService.CreateCharity(correctModel, "a");

                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public void CreateCharityMethodTestedWithEmptyModelAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel emptyModel = new CreateCaseViewModel();

                Assert.That(async () => await caseService.CreateCharity(emptyModel, "a"), Throws.ArgumentException);
            }
            [Test]
            public async Task CreateCharityMethodTestedWithIncorrectModelNameAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "",
                    Description = "SomeDescription",
                    FundsNeeded = 5000,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                    Location = "Kazanlak",
                };

                Assert.That(async () => await caseService.CreateCharity(correctModel, "b"), Throws.Exception);
            }
            [Test]
            public async Task CreateCharityMethodTestedWithIncorrectModelDescriptionAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "TestName",
                    FundsNeeded = 5000,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                    Location = "Kazanlak",
                };

                Assert.That(async () => await caseService.CreateCharity(correctModel, "b"), Throws.ArgumentException);
            }
            [Test]
            public async Task CreateCharityMethodTestedWithIncorrectModelFundsNeededAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "TestName",
                    Description = "SomeDescription",
                    FundsNeeded = 0,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                    Location = "Kazanlak",
                };

                Assert.That(async () => await caseService.CreateCharity(correctModel, "b"), Throws.ArgumentException);
            }
            [Test]
            public async Task CreateCharityMethodTestedWithIncorrectModelLocationAndCorrectUser()
            {
                CaseService caseService = new CaseService(this.context);
                CreateCaseViewModel correctModel = new CreateCaseViewModel()
                {
                    Name = "TestName",
                    Description = "SomeDescription",
                    FundsNeeded = 5000,
                    ImageUrl = "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    CategoryId = Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                };

                Assert.That(async () => await caseService.CreateCharity(correctModel, "b"), Throws.ArgumentException);
            }

            [Test]
            public async Task GetAllCategoriesTest()
            {
                CaseService caseService = new CaseService(this.context);
                var result = await caseService.GetAllCategories();
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task GetAllCharitiesTest()
            {
                CaseService caseService = new CaseService(this.context);
                var result = await caseService.GetAllCharities();
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task GetCharityTest()
            {
                CaseService caseService = new CaseService(this.context);
                Charity charity = await this.context.Charities.FirstOrDefaultAsync();
                var result = await caseService.GetCharity(charity!.Id);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task ExtractCountDataTest()
            {
                CaseService caseService = new CaseService(this.context);
                var result = await caseService.ExtractCountData();
                Assert.That(result, Is.Not.Null);
            }

            [Test]
            public async Task UpdateCharityMethodTestedWithTrueIsApprovedValue()
            {
                CaseService caseService = new CaseService(this.context);
                Charity charity = await this.context.Charities.FirstOrDefaultAsync();

                charity!.IsApproved = true;

                var result = await caseService.UpdateCharity(charity);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task UpdateCharityMethodTestedWithFalseIsApprovedValue()
            {
                CaseService caseService = new CaseService(this.context);
                Charity charity = await this.context.Charities.FirstOrDefaultAsync();

                charity!.IsApproved = false;

                var result = await caseService.UpdateCharity(charity);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task UpdateCharityMethodTestedWithTrueIsDeletedValue()
            {
                CaseService caseService = new CaseService(this.context);
                Charity charity = await this.context.Charities.FirstOrDefaultAsync();

                charity!.IsApproved = true;

                var result = await caseService.UpdateCharity(charity);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task UpdateCharityMethodTestedWithFalseIsDeletedValue()
            {
                CaseService caseService = new CaseService(this.context);
                Charity charity = await this.context.Charities.FirstOrDefaultAsync();

                charity!.IsApproved = false;

                var result = await caseService.UpdateCharity(charity);
                Assert.That(result, Is.Not.Null);
            }
        }

        [TestFixture]
        public class CommentServiceTests
        {
            private IEnumerable<Charity> charities;
            private IEnumerable<User> users;
            private ApplicationDbContext context;
            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: "InMemoryDb")
                   .Options;
                this.context = new ApplicationDbContext(options);
                if (this.context.Users.ToList().Count == 0)
                {
                    this.charities = new List<Charity>()
                {
                    new Charity(){
                        CategoryId=Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                        CreatorId="a",
                        FundsNeeded=200,
                        ImageUrl="https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                        Name = "TestCharity",
                        Location = "Kazanlak",
                        Description = "This is a test charity."
                    }
                };
                    this.users = new List<User>()
                {
                    new User(){Id="a", Email="gogo@abv.bg", UserName = "gosko"},
                    new User(){Id="b", Email="gogo1@abv.bg", UserName = "gosko1"},
                    new User(){Id="c", Email="gogo2@abv.bg", UserName = "gosko2"},
                };

                    this.context.AddRange(users);
                    this.context.AddRange(charities);
                    this.context.SaveChanges();
                }
            }
            [Test]
            public void AddCommentToCharityWithEmptyCharityAndEmptyComment()
            {
                CommentService commentService = new CommentService(this.context);
                Comment emptyComment = new Comment();
                Assert.That(async()=>await commentService.AddCommentToCharity(null, emptyComment), Throws.ArgumentNullException);
            }
            [Test]
            public async Task AddCommentToCharityWithCorrectCharityAndEmptyComment()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctChar = await this.context.Charities.FirstOrDefaultAsync();
                Comment emptyComment = new Comment();
                Assert.That(async () => await commentService.AddCommentToCharity(correctChar!, emptyComment), Throws.ArgumentException);
            }
            [Test]
            public async Task AddCommentToCharityWithIncorrectCharityAndCorrectComment()
            {
                CommentService commentService = new CommentService(this.context);
                Comment correctComment = new Comment() 
                {
                    UserId = "b",
                    Text = "Test text",
                    CreatedDate = DateTime.Now
                };
                Assert.That(async () => await commentService.AddCommentToCharity(null, correctComment), Throws.ArgumentNullException);
            }
            [Test]
            public async Task AddCommentToCharityWithCorrectCharityAndCorrectComment()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = await this.context.Charities.FirstOrDefaultAsync();
                Comment correctComment = new Comment()
                {
                    UserId = "b",
                    Text = "Test text",
                    CreatedDate = DateTime.Now,
                    CharityId = correctCharity!.Id
                };
                var result = await commentService.AddCommentToCharity(correctCharity, correctComment);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task AddCommentToCharityWithCorrectCharityAndIncorrectCommentText()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = await this.context.Charities.FirstOrDefaultAsync();
                Comment wrongComment = new Comment()
                {
                    UserId = "b",
                    Text = null,
                    CreatedDate = DateTime.Now,
                    CharityId = correctCharity!.Id
                };

                Assert.That(async()=>await commentService.AddCommentToCharity(correctCharity, wrongComment), Throws.ArgumentException);
            }
            [Test]
            public async Task GetCommentTest()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = await this.context.Charities.FirstOrDefaultAsync();
                Comment correctComment = new Comment()
                {
                    UserId = "b",
                    Text = "Example",
                    CreatedDate = DateTime.Now,
                    CharityId = correctCharity!.Id
                };
                await commentService.AddCommentToCharity(correctCharity, correctComment);
                var result = commentService.GetComment(correctCharity!.Id);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task DeleteCommentWithCorrectCharityIdAndCorrectCommentId()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = this.context.Charities.Include(c=>c.Comments).FirstOrDefault();
                Comment correctComment = correctCharity.Comments.FirstOrDefault();

                var result = await commentService.DeleteComment(correctComment!.Id, correctCharity.Id);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task DeleteCommentWithCorrectCharityIdAndIncorrectCommentId()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = this.context.Charities.Include(c => c.Comments).FirstOrDefault();
                Comment correctComment = correctCharity.Comments.FirstOrDefault();

                Assert.That(async()=>await commentService.DeleteComment(Guid.Parse("4e3afc52-a876-47fd-86c2-b5e818f3ef9a"), correctCharity.Id), Throws.ArgumentNullException);
            }
            [Test]
            public async Task DeleteCommentWithIncorrectCharityId()
            {
                CommentService commentService = new CommentService(this.context);
                Charity correctCharity = await this.context.Charities.Include(c => c.Comments).FirstOrDefaultAsync();
                Comment correctComment = new Comment()
                {
                    UserId = "b",
                    Text = "Example",
                    CreatedDate = DateTime.Now,
                    CharityId = correctCharity!.Id
                };
                await commentService.AddCommentToCharity(correctCharity, correctComment);
                correctComment = correctCharity!.Comments!.FirstOrDefault()!;

                Assert.That(async () => await commentService.DeleteComment(correctComment!.Id, Guid.Parse("4e3afc52-a876-47fd-86c2-b5e818f3ef9a")), Throws.ArgumentNullException);
            }
        }

        [TestFixture]
        public class DonationServiceTests
        {
            private IEnumerable<Charity> charities;
            private IEnumerable<User> users;
            private ApplicationDbContext context;
            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: "InMemoryDb")
                   .Options;
                this.context = new ApplicationDbContext(options);
                if (this.context.Users.ToList().Count == 0)
                {
                    this.charities = new List<Charity>()
                {
                    new Charity(){
                        CategoryId=Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                        CreatorId="a",
                        FundsNeeded=200,
                        ImageUrl="https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                        Name = "TestCharity",
                        Location = "Kazanlak",
                        Description = "This is a test charity."
                    }
                };
                    this.users = new List<User>()
                {
                    new User(){Id="a", Email="gogo@abv.bg", UserName = "gosko"},
                    new User(){Id="b", Email="gogo1@abv.bg", UserName = "gosko1"},
                    new User(){Id="c", Email="gogo2@abv.bg", UserName = "gosko2"},
                };

                    this.context.AddRange(users);
                    this.context.AddRange(charities);
                    this.context.SaveChanges();
                }
            }
            [Test]
            public async Task MakeDontationToCharityTestWithCorrectCharityAndIncorrectDonationAmount()
            {
                DonationService donationService = new DonationService(this.context);
                Charity charity = await context.Charities.FirstAsync();
                Donation donation = new Donation()
                {
                    UserId = "a",
                    Amount = 0
                };
                Assert.That(async()=>await donationService.MakeDonationToCharity(donation, charity), Throws.Exception);
            }
            [Test]
            public async Task MakeDontationToCharityTestWithCorrectCharityAndCorrectDonationAmount()
            {
                DonationService donationService = new DonationService(this.context);
                Charity charity = await context.Charities.FirstAsync();
                Donation donation = new Donation()
                {
                    UserId = "a",
                    Amount = 5
                };
                var result = await donationService.MakeDonationToCharity(donation, charity);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task MakeDontationToCharityTestWithIncorrectCharityAndCorrectDonationAmount()
            {
                DonationService donationService = new DonationService(this.context);
                var charity = new Charity()
                {
                    Id = Guid.Parse("388ac69d-d369-493c-b741-f5a2433fca89")
                };
                Donation donation = new Donation()
                {
                    UserId = "a",
                    Amount = 5
                };
                Assert.That(async () => await donationService.MakeDonationToCharity(donation, charity), Throws.Exception);
            }
        }

        [TestFixture]
        public class UpdateServiceTests
        {
            private IEnumerable<Charity> charities;
            private IEnumerable<User> users;
            private ApplicationDbContext context;
            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: "InMemoryDb")
                   .Options;
                this.context = new ApplicationDbContext(options);
                if (this.context.Users.ToList().Count == 0)
                {
                    this.charities = new List<Charity>()
                {
                    new Charity(){
                        CategoryId=Guid.Parse("5570f29f-e44e-40dc-9f7c-f97334329548"),
                        CreatorId="a",
                        FundsNeeded=200,
                        ImageUrl="https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg",
                        Name = "TestCharity",
                        Location = "Kazanlak",
                        Description = "This is a test charity."
                    }
                };
                    this.users = new List<User>()
                {
                    new User(){Id="a", Email="gogo@abv.bg", UserName = "gosko"},
                    new User(){Id="b", Email="gogo1@abv.bg", UserName = "gosko1"},
                    new User(){Id="c", Email="gogo2@abv.bg", UserName = "gosko2"},
                };

                    this.context.AddRange(users);
                    this.context.AddRange(charities);
                    this.context.SaveChanges();
                }
            }
            [Test]
            public async Task PostUpdateToCharityWithCorrectCharityAndCorrectUpdate()
            {
                UpdateService service = new UpdateService(context);
                Charity charity = await context.Charities.Include(c=>c.Updates).FirstAsync();
                Update update = new Update()
                {
                    Text = "Example text",
                    DateCreated = DateTime.Now,
                    CharityId = charity.Id,
                    UserId = "a"
                };
                var result = await service.PostUpdateToCharity(update, charity);
                Assert.That(result, Is.Not.Null);
            }
            [Test]
            public async Task PostUpdateToCharityWithCorrectCharityAndIncorrectUpdate()
            {
                UpdateService service = new UpdateService(context);
                Charity charity = await context.Charities.FirstAsync();
                Update update = new Update()
                {
                    Id = Guid.Parse("c9d0a459-031b-4eca-9c76-a4df644d8da7"),
                    Text = "",
                    DateCreated = DateTime.Now,
                    CharityId = charity.Id,
                    UserId = "a"
                };
                Assert.That(async () => await service.PostUpdateToCharity(update, charity), Throws.ArgumentNullException);
            }
            [Test]
            public async Task PostUpdateToCharityWithNullCharityAndCorrectUpdate()
            {
                UpdateService service = new UpdateService(context);
                Charity charity = await context.Charities.FirstAsync();
                Update update = new Update()
                {
                    Id = Guid.Parse("c9d0a459-031b-4eca-9c76-a4df644d8da7"),
                    Text = "Example text",
                    DateCreated = DateTime.Now,
                    CharityId = charity.Id,
                    UserId = "a"
                };
                Assert.That(async () => await service.PostUpdateToCharity(update, null), Throws.ArgumentNullException);
            }
            [Test]
            public async Task PostUpdateToCharityWithNullCharityAndIncorrectUpdate()
            {
                UpdateService service = new UpdateService(context);
                Charity charity = await context.Charities.FirstAsync();
                Update update = new Update()
                {
                    Id = Guid.Parse("c9d0a459-031b-4eca-9c76-a4df644d8da7"),
                    Text = "",
                    DateCreated = DateTime.Now,
                    CharityId = charity.Id,
                    UserId = "a"
                };
                Assert.That(async () => await service.PostUpdateToCharity(update, null), Throws.ArgumentNullException);
            }
            [Test]
            public async Task GetUpdateTest()
            {
                UpdateService service = new UpdateService(context);
                Charity charity = await context.Charities.Include(c => c.Updates).FirstAsync();
                Update update = new Update()
                {
                    Text = "Example text",
                    DateCreated = DateTime.Now,
                    CharityId = charity.Id,
                    UserId = "a"
                };
                var result = await service.PostUpdateToCharity(update, charity);
                Update extractedUpdate = service.GetUpdate(result.Updates.First().Id, result);
                Assert.That(extractedUpdate, Is.Not.Null);
            }
        }
    }
}