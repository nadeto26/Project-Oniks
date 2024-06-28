using IdentityAdvancedDemo.Data;
using IdentityAdvancedDemo.Data.Models;
using IdentityAdvancedDemo.Migrations;
using IdentityAdvancedDemo.Models;
using IdentityAdvancedDemo.Models.Accessories;
using IdentityAdvancedDemo.Models.Delivery;
using IdentityAdvancedDemo.Models.Disscounts;
using IdentityAdvancedDemo.Models.Furnitures;
using IdentityAdvancedDemo.Models.Messages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;
using DeliveryDetails = IdentityAdvancedDemo.Data.Models.DeliveryDetails;
using Messages = IdentityAdvancedDemo.Data.Models.Messages;

namespace IdentityAdvancedDemo.Controllers
{
    public class FurnitureController : Controller
    {

        private readonly FurnitureDbContext dbContext;

        public FurnitureController(FurnitureDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        //Контакти 
        public async Task<IActionResult> AllMessages()
        {
            var toDisplay = await dbContext
                .Messages.Select(d => new MessageViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Message = d.Message,
                    About = d.About,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber

                })
                .ToListAsync();

            return View(toDisplay);
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

            return RedirectToAction(nameof(AllMessages));
        }

        //Мебели - Детайли
        public async Task<bool> Exist(int id)
        {
            return await dbContext.Furnitures.AnyAsync(f => f.Id == id);
        }

        public async Task<AddFurnitureViewModel?> FurnitureDetaildById(int id)
        {
            return await dbContext
                .Furnitures
                .Where(f => f.Id == id)
                .Select(f => new AddFurnitureViewModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Delivery = f.Delivery,
                    Description = f.Description,
                    Price = f.Price,
                    OldPrice = f.OldPrice,
                    NewPrice = f.NewPrice,
                    Category = f.Category.Name,
                    ImageUrl = f.ImageUrl,
                    Importer = f.Importer,
                    Height = f.Height,
                    Weight = f.Weight
                }).FirstOrDefaultAsync();
        }

        public async Task<IActionResult> DetailsFurniture(int id)
        {
            if (await Exist(id) == false)
            {
                return BadRequest();
            }

            var furnitureModel = await FurnitureDetaildById(id);

            return View(furnitureModel);
        }

        //Аксесоари - Детайли
        public async Task<bool> ExistAccesoary(int id)
        {
            return await dbContext.Accessories.AnyAsync(f => f.Id == id);
        }

        public async Task<AllViewModelAccesoaries?> AccesoaryDetaildById(int id)
        {
            return await dbContext
                .Accessories
                .Where(f => f.Id == id)
                .Select(f => new AllViewModelAccesoaries()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Delivery = f.Delivery,
                    Description = f.Description,
                    Price = f.Price,
                    NewPrice = f.NewPrice,
                    OldPrice = f.OldPrice,
                    ImageUrl = f.ImageUrl,
                    Importer = f.Importer,

                }).FirstOrDefaultAsync();
        }

        public async Task<IActionResult> DetailsAccesoary(int id)
        {
            if (await ExistAccesoary(id) == false)
            {
                return BadRequest();
            }

            var accesoaryModel = await AccesoaryDetaildById(id);

            return View(accesoaryModel);
        }

        //Детайли за отстъпки
        public async Task<bool> ExistDiscounts(int id)
        {
            return await dbContext.Discounts.AnyAsync(f => f.Id == id);
        }

        public async Task<AllDiscountViewModel?> DiscountDetailsById(int id)
        {
            return await dbContext
                .Discounts
                .Where(f => f.Id == id)
                .Select(f => new AllDiscountViewModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Delivery = f.Delivery,
                    NewPrice = f.NewPrice,
                    OldPrice = f.OldPrice,
                    ImageUrl = f.ImageUrl,
                    Importer = f.Importer,

                }).FirstOrDefaultAsync();
        }

        public async Task<IActionResult> DetailsDiscount(int id)
        {
            if (await ExistDiscounts(id) == false)
            {
                return BadRequest();
            }

            var dicsountModel = await DiscountDetailsById(id);

            return View(dicsountModel);
        }

        //За контактите 
        [HttpGet]
        public async Task<IActionResult> AddMessege() //мебел 
        {
            MessageViewModel admodel = new MessageViewModel()
            {

            };

            return View(admodel);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessege(MessageViewModel model)
        {
            var adMessege = new Messages()
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                About = model.About,
                Message = model.Message,
            };
            await dbContext.Messages.AddAsync(adMessege);
            dbContext.SaveChanges();

            return RedirectToAction("AddMessege");
        }

        [HttpGet]
        public async Task<IActionResult> DetailsDelivery()
        {
            DeliveryDetailsViewModel admodel = new DeliveryDetailsViewModel()
            {

            };

            return View(admodel);
        }

        [HttpPost]
        public async Task<IActionResult> DetailsDelivery(DeliveryDetailsViewModel admode)
        {
            var adDelivery = new DeliveryDetails()
            {
                FullName = admode.FullName,
                Address = admode.Address,
                City = admode.City,
                PostCode = admode.PostalCode,
                Email = admode.Email,
                PhoneNumber = admode.PhoneNumber
            };
            await dbContext.DeliveryDetails.AddAsync(adDelivery);
            dbContext.SaveChanges();

            return RedirectToAction("Confirmation");
        }

        public async Task<IActionResult> Confirmation()
        {
            Guid currentUserId = GetUserId();

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var deliveryDetails = await dbContext.DeliveryDetails.FirstOrDefaultAsync(dd => dd.Email == user.Email);
            if (deliveryDetails == null)
            {
                return BadRequest("Моля, въведете имейла, с когото сте се регистрирали");
            }

            var cartItems = await dbContext.FurnitureBuyers
                .Include(fb => fb.Furnitures)
                .Where(fb => fb.BuyerId == currentUserId)
                .ToListAsync();

            var accessoriesInCart = await dbContext.AccesoaryBuyers
                .Include(ab => ab.Accessories)
                .Where(ab => ab.BuyerId == currentUserId)
                .ToListAsync();

            var discountsInCart = await dbContext.DiscountBuyers
                .Include(db => db.Discounts)
                .Where(db => db.BuyerId == currentUserId)
                .ToListAsync();

            foreach (var furnitureBuyer in cartItems)
            {
                var order = new Orders
                {
                    FullName = deliveryDetails.FullName,
                    Address = deliveryDetails.Address,
                    City = deliveryDetails.City,
                    PostCode = deliveryDetails.PostCode,
                    Phonenumber = deliveryDetails.PhoneNumber,
                    BuyerId = currentUserId,
                    DiscountId = discountsInCart.FirstOrDefault()?.DiscountId,
                    QuentityDiscount = discountsInCart.Sum(db => db.Quentity),
                    FurnitureId = furnitureBuyer.Furnitures.Id,
                    FurnitureName = furnitureBuyer.Furnitures.Name,
                    QuentityFurniture = furnitureBuyer.Quentity,
                    AccessoryId = null,
                    AccessoriesName = null,
                    QuentityAccessory = 0,
                };

                dbContext.Orders.Add(order);
            }
            foreach (var accessoryBuyer in accessoriesInCart)
            {
                var order = new Orders
                {
                    FullName = deliveryDetails.FullName,
                    Address = deliveryDetails.Address,
                    City = deliveryDetails.City,
                    PostCode = deliveryDetails.PostCode,
                    Phonenumber = deliveryDetails.PhoneNumber,
                    BuyerId = currentUserId,
                    DiscountId = discountsInCart.FirstOrDefault()?.DiscountId,
                    QuentityDiscount = discountsInCart.Sum(db => db.Quentity),
                    FurnitureId = null,
                    FurnitureName = null,
                    QuentityFurniture = 0,
                    AccessoryId = accessoryBuyer.Accessories.Id,
                    AccessoriesName = accessoryBuyer.Accessories.Name,
                    QuentityAccessory = accessoryBuyer.Quentity,
                };

                dbContext.Orders.Add(order);
            }

            foreach (var discountBuyer in discountsInCart)
            {
                var order = new Orders
                {
                    FullName = deliveryDetails.FullName,
                    Address = deliveryDetails.Address,
                    City = deliveryDetails.City,
                    PostCode = deliveryDetails.PostCode,
                    Phonenumber = deliveryDetails.PhoneNumber,
                    BuyerId = currentUserId,
                    DiscountId = discountBuyer.Discounts.Id,
                    DiscountName = discountBuyer.Discounts.Name,
                    QuentityDiscount = discountBuyer.Quentity,
                    FurnitureId = null,
                    FurnitureName = null,
                    QuentityFurniture = 0,
                    AccessoryId = null,
                    AccessoriesName = null,
                    QuentityAccessory = 0,
                };

                dbContext.Orders.Add(order);
            }

            dbContext.FurnitureBuyers.RemoveRange(cartItems);
            dbContext.AccesoaryBuyers.RemoveRange(accessoriesInCart);
            dbContext.DiscountBuyers.RemoveRange(discountsInCart);

            await dbContext.SaveChangesAsync();

            return View();
        }

        //Мебели - всимки 
        public async Task<IActionResult> All([FromQuery] AllFurnitureQuaryModel model)
        {
            var queryResult = All
                (
                model.Category,
                model.SearchItem,
                model.Sorting,
                model.CurrentPage,
                AllFurnitureQuaryModel.FurniturePerPage
                );

            model.TotalFurnitureCount = queryResult.ToTalFurnitureCount;
            model.Furnitures = queryResult.Furnitures;

            var furnitureCategories = await AllCategoriesNames();
            model.Categories = (IEnumerable<string>)furnitureCategories;

            return View(model);
        }

        FurnitureQuaryModel All(string category = null,
            string searchItem = null, FurnitureSort sort = FurnitureSort.HighestPrice
            , int currentPage = 1, int furniturePerPage = 1)
        {
            var furnitureQuery = dbContext.Furnitures.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                furnitureQuery = dbContext.Furnitures
                    .Where(f => f.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(searchItem))
            {
                furnitureQuery = furnitureQuery
                    .Where(f =>
                    f.Description.ToLower().Contains(searchItem.ToLower()) ||
                    f.Name.ToLower().Contains(searchItem.ToLower()) ||
                    f.Importer.ToLower().Contains(searchItem.ToLower()));
            }

            furnitureQuery = sort switch
            {
                FurnitureSort.LowestPrice => furnitureQuery.OrderByDescending(f => f.Price),
                FurnitureSort.HighestPrice => furnitureQuery.OrderBy(f => f.Price),
            };

            var furniture = furnitureQuery
                //.Skip((currentPage-1) * furniturePerPage)
                //.Take(furniturePerPage)
                .Select(f => new AllFurnitureViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    Price = f.Price,
                    ImageUrl = f.ImageUrl,
                    Category = f.Category.Name,
                    NewPrice = f.NewPrice,
                    OldPrice = f.OldPrice
                }).ToList();

            var totalFurniture = furnitureQuery.Count();

            return new FurnitureQuaryModel()
            {
                ToTalFurnitureCount = totalFurniture,
                Furnitures = furniture
            };
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var adToAdd = await dbContext.Furnitures.FindAsync(id);

            Guid currentUserId = GetUserId();
            try
            {

                var existingCartItem = await dbContext.FurnitureBuyers
                    .FirstOrDefaultAsync(ci => ci.BuyerId == currentUserId && ci.FurnitureId == id);

                if (existingCartItem != null)
                {

                    existingCartItem.Quentity++;
                }
                else
                {

                    var newCartItem = new FurnitureBuier()
                    {
                        BuyerId = currentUserId,
                        FurnitureId = id,
                        Quentity = 1
                    };
                    dbContext.FurnitureBuyers.Add(newCartItem);
                }

                await dbContext.SaveChangesAsync();
                return RedirectToAction("CartFurniture", "Furniture");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Грешка при добавяне на артикул в кошницата: {ex.Message}");
                return RedirectToAction("CartFurniture", "Furniture");
            }



           

        }

        public async Task<FurnitureBuier> GetCartItemByIdAsync(Guid buyerId, int furnitureId)
        {
            return await dbContext.FurnitureBuyers.FirstOrDefaultAsync(w => w.BuyerId == buyerId && w.FurnitureId == furnitureId);
        }

        public async Task<AccessoryBuyer> GetAccesoaryItemByIdAsync(Guid buyerId, int acceasoaryId)
        {
            return await dbContext.AccesoaryBuyers.FirstOrDefaultAsync(w => w.BuyerId == buyerId && w.AccesoaryId == acceasoaryId);
        }

        public async Task<DiscountBuyer> GetDiscuountItemByIdAsync(Guid buyerId, int discountId)
        {
            return await dbContext.DiscountBuyers.FirstOrDefaultAsync(w => w.BuyerId == buyerId && w.DiscountId == discountId);
        }

        public async Task<bool> UpdateCartItemAsync(FurnitureBuier cartItem)
        {
            try
            {
                dbContext.Update(cartItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при актуализиране на артикул в кошницата: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAccesoaryCartItemAsync(AccessoryBuyer cartItem)
        {
            try
            {
                dbContext.Update(cartItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при актуализиране на артикул в кошницата: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateDiscountCartItemAsync(DiscountBuyer cartItem)
        {
            try
            {
                dbContext.Update(cartItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при актуализиране на артикул в кошницата: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> RemoveWineFromCartAsync(Guid buyerId, int furnitureId)
        {
            try
            {
                var cartItem = await dbContext.FurnitureBuyers.FirstOrDefaultAsync(ci => ci.BuyerId == buyerId && ci.FurnitureId == furnitureId);
                if (cartItem != null)
                {
                    dbContext.FurnitureBuyers.Remove(cartItem);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при премахване на артикул от кошницата: {ex.Message}");
                return false;
            }

            
        }

        public async Task<bool> RemoveAccesoaryFromCartAsync(Guid buyerId, int acceosaryId)
        {
            try
            {
                var cartItem = await dbContext.AccesoaryBuyers.FirstOrDefaultAsync(ci => ci.BuyerId == buyerId && ci.AccesoaryId == acceosaryId);
                if (cartItem != null)
                {
                    dbContext.AccesoaryBuyers.Remove(cartItem);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при премахване на артикул от кошницата: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveDiscountFromCartAsync(Guid buyerId, int discountId)
        {
            try
            {
                var cartItem = await dbContext.DiscountBuyers.FirstOrDefaultAsync(ci => ci.BuyerId == buyerId && ci.DiscountId == discountId);
                if (cartItem != null)
                {
                    dbContext.DiscountBuyers.Remove(cartItem);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка при премахване на артикул от кошницата: {ex.Message}");
                return false;
            }
        }

        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            Guid currentUserId = GetUserId();
            var wineCartItem = await GetCartItemByIdAsync(currentUserId, id);
            var discountCartItem = await GetDiscuountItemByIdAsync(currentUserId, id);
            var accoaryCartItem = await GetAccesoaryItemByIdAsync(@currentUserId, id);

            if (wineCartItem != null)
            {
                if (wineCartItem.Quentity > 1)
                {
                    wineCartItem.Quentity--;
                    await UpdateCartItemAsync(wineCartItem);
                }
                else
                {
                    await RemoveWineFromCartAsync(currentUserId, id);
                }
            }

            if (discountCartItem != null)
            {
                if (discountCartItem.Quentity > 1)
                {
                    discountCartItem.Quentity--;
                    await UpdateDiscountCartItemAsync(discountCartItem);
                }
                else
                {
                    await RemoveDiscountFromCartAsync(currentUserId, id);
                }
            }

            if (accoaryCartItem != null)
            {
                if (accoaryCartItem.Quentity > 1)
                {
                    accoaryCartItem.Quentity--;
                    await UpdateAccesoaryCartItemAsync(accoaryCartItem);
                }
                else
                {
                    await RemoveAccesoaryFromCartAsync(currentUserId, id);
                }
            }
            return RedirectToAction(nameof(CartFurniture));
        }

        public async Task<IActionResult> IncreaseQuantity(int id)
        {
            Guid currentUserId = GetUserId();
            var wineCartItem = await GetCartItemByIdAsync(currentUserId, id);

            if (wineCartItem != null)
            {
                wineCartItem.Quentity++;
                await UpdateCartItemAsync(wineCartItem);
            }
            var aceasoryCartItem = await GetAccesoaryItemByIdAsync(currentUserId, id);
            if (aceasoryCartItem != null)
            {
                aceasoryCartItem.Quentity++;
                await UpdateAccesoaryCartItemAsync(aceasoryCartItem);
            }

            var discountCartItem = await GetDiscuountItemByIdAsync(currentUserId, id);
            if (discountCartItem!= null)
            {
                discountCartItem.Quentity++;
                await UpdateDiscountCartItemAsync(discountCartItem);
            }
            return RedirectToAction(nameof(CartFurniture));
        }

        [Authorize]
        public async Task<IActionResult> AddAccessoryToCart(int id)
        {
            var adToAdd = await dbContext.Accessories.FindAsync(id);

            Guid currentUserId = GetUserId();
            try
            {

                var existingCartItem = await dbContext.AccesoaryBuyers
                    .FirstOrDefaultAsync(ci => ci.BuyerId == currentUserId && ci.AccesoaryId == id);

                if (existingCartItem != null)
                {

                    existingCartItem.Quentity++;
                }
                else
                {

                    var newCartItem = new AccessoryBuyer()
                    {
                        BuyerId = currentUserId,
                        AccesoaryId = id,
                        Quentity = 1
                    };
                    dbContext.AccesoaryBuyers.Add(newCartItem);
                }

                await dbContext.SaveChangesAsync();
                return RedirectToAction("CartFurniture", "Furniture");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Грешка при добавяне на артикул в кошницата: {ex.Message}");
                return RedirectToAction("CartFurniture", "Furniture");
            }

        }

        [Authorize]
        public async Task<IActionResult> AddDiscountToCart(int id)
        {
            var adToAdd = await dbContext.Discounts.FindAsync(id);

            Guid currentUserId = GetUserId();
            try
            {

                var existingCartItem = await dbContext.DiscountBuyers
                    .FirstOrDefaultAsync(ci => ci.BuyerId == currentUserId && ci.DiscountId == id);

                if (existingCartItem != null)
                {

                    existingCartItem.Quentity++;
                }
                else
                {

                    var newCartItem = new DiscountBuyer()
                    {
                        BuyerId = currentUserId,
                        DiscountId = id,
                        Quentity = 1
                    };
                    dbContext.DiscountBuyers.Add(newCartItem);
                }

                await dbContext.SaveChangesAsync();
                return RedirectToAction("CartFurniture", "Furniture");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Грешка при добавяне на артикул в кошницата: {ex.Message}");
                return RedirectToAction("CartFurniture", "Furniture");
            }

        }

        public async Task<bool> AddFurnitureToCartAsync(int furnitureId, Guid userId)
        {
            var existingCartItem = await dbContext.FurnitureBuyers
                .FirstOrDefaultAsync(ci => ci.BuyerId == userId && ci.FurnitureId == furnitureId);

            if (existingCartItem != null)
            {

                existingCartItem.Quentity++;
            }

            return true;
        }

        [Authorize]
        public async Task<IActionResult> CartFurniture(int furnitureId, Guid userId)
        {
            Guid currentUserId = GetUserId();

            var existingCartItem = await dbContext.FurnitureBuyers
               .FirstOrDefaultAsync(ci => ci.BuyerId == userId && ci.FurnitureId == furnitureId);

               var userFurniture = await dbContext
              .FurnitureBuyers
              .Where(ab => ab.BuyerId == currentUserId)
              .Select(ab => new AddToCartViewModel()
              {
                  Id = ab.Furnitures.Id,
                  Name = ab.Furnitures.Name,
                  Price = ab.Furnitures.Price,
                  ImageUrl = ab.Furnitures.ImageUrl,
                  Quenitity = ab.Quentity,
                  NewPrice = ab.Furnitures.NewPrice,
                  OldPrice = ab.Furnitures.OldPrice
              })
              .ToListAsync();
            
            var userDiscount = await dbContext
            .DiscountBuyers
            .Where(ab => ab.BuyerId == currentUserId)
            .Select(ab => new AddToCartViewModel()
            {
                Id = ab.Discounts.Id,
                Name = ab.Discounts.Name,
                Price = ab.Discounts.NewPrice,
                ImageUrl = ab.Discounts.ImageUrl,
                Quenitity = ab.Quentity
            })
            .ToListAsync();

            var userAccesoaries = await dbContext
            .AccesoaryBuyers
            .Where(ab => ab.BuyerId == currentUserId)
            .Select(ab => new AddToCartViewModel()
            {
                Id = ab.Accessories.Id,
                Name = ab.Accessories.Name,
                Price = ab.Accessories.Price,
                ImageUrl = ab.Accessories.ImageUrl,
                Quenitity = ab.Quentity
            })
            .ToListAsync();

            var userAds = userFurniture.Concat(userAccesoaries).Concat(userDiscount).ToList();

            return View(userAds);
        }

       
        public async Task<IActionResult> AllPromocii()
        {
            var toDisplay = await dbContext
                .Discounts.Select(d=> new AllDiscountViewModel
                {
                    Id =d.Id,
                    Name = d.Name,
                    NewPrice = d.NewPrice,
                    OldPrice = d.OldPrice,
                    ImageUrl = d.ImageUrl,
                    Weight = d.Weight,
                    Delivery = d.Delivery,
                    Height = d.Height,
                    Importer= d.Importer

                })
                .ToListAsync();

            return View(toDisplay);
        }

        //визуализация на аксесоарите AllAccesoaries 

        public async Task<IActionResult> AllAccesoaries()
        {
            var addToDisplay = await dbContext.Accessories
                .Select(a => new AllViewModelAccesoaries
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    ImageUrl = a.ImageUrl,
                    OldPrice= a.OldPrice,
                    NewPrice= a.NewPrice
                })
                .ToListAsync();

            return View(addToDisplay);

        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            Guid currentUser = GetUserId();

            var accessoryToRemove = await dbContext.Accessories.FindAsync(id);
            if (accessoryToRemove != null)
            {
                var accessoryBuyer = await dbContext.AccesoaryBuyers.FirstOrDefaultAsync(ab => ab.BuyerId == currentUser && ab.AccesoaryId == id);
                if (accessoryBuyer != null)
                {
                    dbContext.AccesoaryBuyers.Remove(accessoryBuyer);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("CartFurniture");
                }
            }

            var furnitureToRemove = await dbContext.Furnitures.FindAsync(id);
            if (furnitureToRemove != null)
            {
                var furnitureBuyer = await dbContext.FurnitureBuyers.FirstOrDefaultAsync(fb => fb.BuyerId == currentUser && fb.FurnitureId == id);
                if (furnitureBuyer != null)
                {
                    dbContext.FurnitureBuyers.Remove(furnitureBuyer);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("CartFurniture");
                }
            }

            var discountToRemove = await dbContext.Discounts.FindAsync(id);
            if (discountToRemove != null)
            {
                var dicsountBuyer = await dbContext.DiscountBuyers.FirstOrDefaultAsync(fb => fb.BuyerId == currentUser && fb.DiscountId == id);
                if (dicsountBuyer != null)
                {
                    dbContext.DiscountBuyers.Remove(dicsountBuyer);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("CartFurniture");
                }
            }

            return BadRequest();
        }


        private IEnumerable<Category> GetCategories()
          => dbContext
              .Categories
              .Select(t => new Category()
              {
                  Id = t.Id,
                  Name = t.Name
              });

        public async Task<IEnumerable<string>> AllCategoriesNames()
        => await dbContext
            .Categories
            .Select(t=> t.Name)
            .Distinct() .ToListAsync();
            

        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdString, out Guid userIdGuid))
            {
                return userIdGuid;
            }
            else
            {
                // Обработка на грешка или връщане на стойност по подразбиране
                // В случай на грешка може да се върне Guid.Empty или null, в зависимост от нуждите на приложението
                throw new InvalidOperationException("Invalid user id format");
            }
        }
    }
}
