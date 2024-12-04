using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocketPlanetarium.Controllers;
using PocketPlanetarium.Data;
using PocketPlanetarium.Models;


namespace PocketPlanetariumTests
{
    [TestClass]
    public class PlanetsControllerTests
    {

        private ApplicationDbContext _context;
        PlanetsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            var date = new DateOnly(2021, 02, 02);
            _context.Planet.Add(new Planet { planetId = 10, Name = "Planet1", Description = "Blah BLah BLAh BLAH", DiscoveryDate = date, Distance = 1.0202, Mass = 92.33, StellarMagnitude = 1, ImagePath = "askjdflasdjgdfg" });
            _context.Planet.Add(new Planet { planetId = 11, Name = "Planet2", Description = "Blah BLah BLAh BLAH", DiscoveryDate = date, Distance = 1.0202, Mass = 92.33, StellarMagnitude = 1, ImagePath = "askjdflasdjgdfg" });
            _context.Planet.Add(new Planet { planetId = 12, Name = "Planet3", Description = "Blah BLah BLAh BLAH", DiscoveryDate = date, Distance = 1.0202, Mass = 92.33, StellarMagnitude = 1, ImagePath = "askjdflasdjgdfg" });
            _context.SaveChanges();

            controller = new PlanetsController(_context);
        }

        //Test Index 
        #region "Index"
        [TestMethod]
        public void IndexReturnsView()
        {
            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexReturnsPlanets()
        {
            var result = (ViewResult)controller.Index().Result;
            var dataModel = (List<Planet>)result.Model;

            CollectionAssert.AreEqual(_context.Planet.ToList(), dataModel);
        }
        #endregion

        //Test Details
        #region "Details"
        [TestMethod]
        public void DetailsReturns404ViewWhenNoId()
        {
            var result = (ViewResult)controller.Details(null).Result;
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DetailsReturns404ViewWhenPlanetNotFound()
        {
            var result = (ViewResult)controller.Details(100).Result;
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void DetailsReturnsPlanetView()
        {
            var result = (ViewResult)controller.Details(10).Result;
            
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void DetailsReturnsPlanetWhenValidId()
        {
            var result = (ViewResult)controller.Details(10).Result;
            var planet = (Planet)result.Model;
            Assert.AreEqual(_context.Planet.Find(10), planet);
        }
        #endregion

        //Test Create
        #region "Create"
        [TestMethod]
        public void CreateReturnsView()
        {
            var result = (ViewResult)controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }
        [TestMethod]
        public void CreateAddsPlanet()
        {
            var newPlanet = new Planet
            {
                planetId = 13,
                Name = "Planet4",
                Description = "New Planet Description",
                DiscoveryDate = new DateOnly(2022, 01, 01),
                Distance = 2.0202,
                Mass = 100.33,
                StellarMagnitude = 2,
                ImagePath = "newimagepath"
            };

            var result = (RedirectToActionResult)controller.Create(newPlanet).Result;

            Assert.IsNotNull(_context.Planet.Find(13));
        }
        #endregion
    }
}
