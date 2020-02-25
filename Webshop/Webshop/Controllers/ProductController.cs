using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Webshop.Context;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebshopContext context;
        private IWebHostEnvironment environment;

        //public CreateProductModel createProductModel { get; set; }
        public DatabaseCRUD databaseCRUD;
        public ProductController(WebshopContext context, IWebHostEnvironment env)
        {
            this.environment = env;
            this.context = context;
        }


        //This mtd display the Products based on Passed in CategoryId
        public IActionResult Index(int catid)
        {
           
                var query = (from product in context.Products
                             where product.CategoryId==catid
                             select product).ToList();
                return View(query);           
                
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct()
        {
            CreateProductModel createProductModel = new CreateProductModel();
            return View(createProductModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct([Bind]CreateProductModel model)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    
                    Product newProduct = new Product()
                    {
                        Name = model.Name, 
                        Price = model.Price,
                        Quantity = model.Quantity,
                        CategoryId = model.CategoryId,
                        BrandId = model.BrandId,
                        Description=model.Description,
                        Photo=model.Photo
                    };
                //    DatabaseCRUD databaseCRUD = new DatabaseCRUD(context.Products);
                    
                 // await databaseCRUD.InsertAsync<Product>(newProduct);
                  context.Products.Add(newProduct);
                    context.SaveChanges();
                    
                }
                //this can be used to display summerize the error
                //return View(model);
                // return Content("Successfully added");
                return RedirectToAction("AllProducts", "Product");


            }
            catch
            {
                
                return Content("its Inside catch block, some error in adding product");
            }
        }

        public IActionResult AllProducts()
        {
            var query = context.Products.ToList();
            return View(query);
        }


        public IActionResult TestUploadFile()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadTest(IFormFile file)
        {
            // Get path to wwwroot folder
            var wwwRoot = environment.WebRootPath;

            // Name of folder to store product images
            var productFolder = "products";

            // Create folder for storing product images if it does not exist
            if (!Directory.Exists(wwwRoot + "\\" + productFolder))
                Directory.CreateDirectory(wwwRoot + "\\" + productFolder);

            // Get name of file. Validate file before using it!
            var fileName = System.IO.Path.GetFileName(file.FileName);
            
            // Set the path to point to 
            var filePath = Path.Combine(wwwRoot, productFolder, fileName);


            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return View(nameof(TestUploadFile));
        }
    }
}