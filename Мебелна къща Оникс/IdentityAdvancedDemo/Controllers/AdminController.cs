
using IdentityAdvancedDemo.Data;
using IdentityAdvancedDemo.Data.IdentityModels;
using IdentityAdvancedDemo.Data.Models;
using IdentityAdvancedDemo.Models.Accessories;
using IdentityAdvancedDemo.Models.Admin;
using IdentityAdvancedDemo.Models.Disscounts;
using IdentityAdvancedDemo.Models.Furnitures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
 
namespace IdentityAdvancedDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly FurnitureDbContext dbContext;
        private readonly IMemoryCache _memorycashe;
        public AdminController(FurnitureDbContext dbContext, IMemoryCache memoryCache)
        {
            this.dbContext = dbContext;
            _memorycashe = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Orders));
        }

        public async Task<IActionResult> Deletemessege(int id)
        {
            var order = await dbContext.Messages.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            dbContext.Messages.Remove(order);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("AllMessages","Furniture");
        }

        [HttpGet]
        public async Task<IActionResult> Add() //мебел 
        {
            AddFurnitureViewModel admodel = new AddFurnitureViewModel()
            {
                Categories = GetCategories()
            };

            return View(admodel);
        }

        public async Task<IActionResult> Add(AddFurnitureViewModel adModel) //мебел 
        { 
            string currentUser = GetUserId();
            var adToAdd = new Furnitures()
            {
                Name = adModel.Name,
                Description = adModel.Description,
                CategoryId = adModel.CategoryId,
                Price = adModel.Price,
                ImageUrl = adModel.ImageUrl,
                Weight = adModel.Weight,
                Height = adModel.Height,
                Delivery = adModel.Delivery,
                Importer = adModel.Importer,
                OldPrice = adModel.OldPrice,
                NewPrice = adModel.NewPrice
            };
            await dbContext.Furnitures.AddAsync(adToAdd);
            dbContext.SaveChanges();

            return RedirectToAction("Add");
        }

        //добавяне на промоции - визуализация

        [HttpGet]
        public async Task<IActionResult> AddDiscount()
        {
            AllDiscountViewModel adModels = new AllDiscountViewModel()
            {

            };
            return View(adModels);
        }

        //добавяне на промоции- метод post

        [HttpPost]
        public async Task<IActionResult> AddDiscount(AllDiscountViewModel model)
        {
            string currentUserId = GetUserId();

            var adToAdd = new Discount()
            {
                Name = model.Name,
                NewPrice = model.NewPrice,
                OldPrice = model.OldPrice,
                Delivery = model.Delivery,
                ImageUrl= model.ImageUrl,
                Height = model.Height,
                Weight = model.Weight,
                Importer = model.Importer
            };

            await dbContext.Discounts.AddAsync(adToAdd);
            dbContext.SaveChanges();

            return RedirectToAction("AddDiscount");
        }

        //Добавяне на аксесоари - визуализация 

        [HttpGet]
        public async Task<IActionResult> AddAcccesoaries()
        {
            AllViewModelAccesoaries admodel = new AllViewModelAccesoaries();

            return View(admodel);
        }

        //Добавяне на аксесоари - Post 
        [HttpPost]
        public async Task<IActionResult> AddAcccesoaries(AllViewModelAccesoaries adModel)
        {
            string currentUser = GetUserId();

            var adToAdd = new Accessories()
            {
                Name = adModel.Name,
                Description = adModel.Description,
                Price = adModel.Price,
                ImageUrl = adModel.ImageUrl,
                Delivery = adModel.Delivery,
                NewPrice= adModel.NewPrice,
                OldPrice = adModel.OldPrice,
                Importer = adModel.Importer,
            };

            await dbContext.Accessories.AddAsync(adToAdd);
            dbContext.SaveChanges();

            return RedirectToAction("AddAcccesoaries");

        }

        public async Task<IActionResult> EditAccesoaries(int id)
        {
            var adToEdit = await dbContext.Accessories.FindAsync(id);

            if (adToEdit == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();


            AllViewModelAccesoaries adModel = new AllViewModelAccesoaries()
            {
                Name = adToEdit.Name,
                Description = adToEdit.Description,
                Price = adToEdit.Price,
                ImageUrl = adToEdit.ImageUrl,
                Delivery = adToEdit.Delivery,
                NewPrice = adToEdit.NewPrice,
                OldPrice = adToEdit.OldPrice,
                Importer = adToEdit.Importer,
            };

            return View(adModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccesoaries(int id, AllViewModelAccesoaries adModel)
        {
            var adToEdit = await dbContext.Accessories.FindAsync(id);

            if (adToEdit == null)
            {
                return BadRequest();
            }

            string currentUser = GetUserId();


            adToEdit.Name = adModel.Name;
            adToEdit.Description = adModel.Description;
            adToEdit.Price = adModel.Price;
            adToEdit.OldPrice = adModel.OldPrice;
            adToEdit.NewPrice = adModel.NewPrice;
            adToEdit.Delivery = adModel.Delivery;
            adToEdit.ImageUrl = adModel.ImageUrl;
            adModel.Importer = adModel.Importer;

            await dbContext.SaveChangesAsync();
            return RedirectToAction("EditAccesoaries");
        }

        public async Task<IActionResult> EditPromocii(int id)
        {
            var adToEdit = await dbContext.Discounts.FindAsync(id);

            if (adToEdit == null)
            {
                return BadRequest();
            }

            AllDiscountViewModel adModel = new AllDiscountViewModel()
            {
                Name = adToEdit.Name,
                NewPrice = adToEdit.NewPrice,
                OldPrice = adToEdit.OldPrice,
                ImageUrl = adToEdit.ImageUrl,
                Weight = adToEdit.Weight,
                Height = adToEdit.Height,
                Importer = adToEdit.Importer,
                Delivery = adToEdit.Delivery
            };
            return View(adModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPromocii(int id, AllDiscountViewModel admodel)
        {
            var adToEdit = await dbContext.Discounts.FindAsync(id);

            if(adToEdit == null)
            {
                return BadRequest();
            }
            string currentUser = GetUserId();

            adToEdit.Name = admodel.Name;
            adToEdit.NewPrice = admodel.NewPrice;
            adToEdit.OldPrice = admodel.OldPrice;
            adToEdit.ImageUrl = admodel.ImageUrl;
            adToEdit.Weight = admodel.Weight;
            adToEdit.Height = admodel.Height;
            adToEdit.Importer = admodel.Importer;
            adToEdit.Delivery = admodel.Delivery;
            

            await dbContext.SaveChangesAsync();
            return RedirectToAction("EditPromocii");
        }

        [HttpGet]

        public async Task<IActionResult> EditFurniture(int id)
        {
            var adToEdit = await dbContext.Furnitures.FindAsync(id);

            if (adToEdit == null)
            {
                return BadRequest();
            }

            AllFurnitureViewModel adModel = new AllFurnitureViewModel()
            {
                Name = adToEdit.Name,
                Description = adToEdit.Description,
                Categories = GetCategories(),
                Price = adToEdit.Price,
                ImageUrl = adToEdit.ImageUrl,
                Weight = adToEdit.Weight,
                Height = adToEdit.Height,
                Delivery = adToEdit.Delivery,
                Importer = adToEdit.Importer,
                OldPrice = adToEdit.OldPrice,
                NewPrice = adToEdit.NewPrice
            };

            return View(adModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditFurniture(int id, AllFurnitureViewModel adModel)
        {
            var adToEdit = await dbContext.Furnitures.FindAsync(id);

            if (adToEdit == null)
            {
                return BadRequest();
            }
            if (!GetCategories().Any(e => e.Id == adModel.CategoryId))
            {
                ModelState.AddModelError(nameof(adModel.CategoryId), "Category does not exist!");
            }
            adToEdit.Name = adModel.Name;
            adToEdit.Description = adModel.Description;
            adToEdit.Price = adModel.Price;
            adToEdit.CategoryId = adModel.CategoryId;
            adToEdit.ImageUrl = adModel.ImageUrl;
            adToEdit.Weight = adModel.Weight;
            adToEdit.Height = adModel.Height;
            adToEdit.Delivery = adModel.Delivery;
            adToEdit.Importer = adModel.Importer;
            adModel.OldPrice = adToEdit.OldPrice;
            adModel.NewPrice = adToEdit.NewPrice;   

            await dbContext.SaveChangesAsync();
            return RedirectToAction("EditFurniture");
        }

        public async Task<IActionResult> Orders()
        {
            var toDisplay = await dbContext.Orders
                .Select(d => new IdentityAdvancedDemo.Models.Admin.Oders
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    PostCode = d.PostCode,
                    Address = d.Address,
                    City = d.City,
                    QuentityFurniture = d.QuentityFurniture,
                    QuentityAccessory = d.QuentityAccessory,
                    QuentityDiscount = d.QuentityDiscount,
                    AccessoriesName = d.Accessories != null ? d.Accessories.Name : null,
                    DiscountName = d.Discounts != null ? d.Discounts.Name : null,
                    FurnitureName = d.Furnitures != null ? d.Furnitures.Name : null,
                    Phonenumber = d.Phonenumber,
                })
                .ToListAsync();

            return View(toDisplay);
        }

        [Route("Admin/AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            var allUsers = await All();
            return View(allUsers);
        }

        public async Task<IEnumerable<UserServiceModel>> All()
        {


            return await dbContext.Users
                .Select(u => new UserServiceModel()
                {
                    Email = u.Email,
                   
                }).ToListAsync();


        }

        public async Task<string> UserFullName(string userId)
        {
            string result = string.Empty;
            var user = await dbContext.Set<ApplicationUser>().FindAsync(userId);
            if (user != null)
            {
                return $"{user.FirstName} {user.LastName}";
            }
            return result;
        }
    


    private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private IEnumerable<Category> GetCategories()
          => dbContext
              .Categories
              .Select(t => new Category()
              {
                  Id = t.Id,
                  Name = t.Name
              });
    }
}
