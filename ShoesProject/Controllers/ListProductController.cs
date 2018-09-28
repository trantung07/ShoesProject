using ShoesProject.Models;
using ShoesProjectModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ShoesProject.Controllers
{
    public class ListProductController : Controller
    {
        private Shoes db = new Shoes();
        // GET: ShopGirl
        public ActionResult Index(int id, int? page, int? brandId = 0, int? colorId = 0, int? sizeId = 0, int? orderBy = 0)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            IQueryable<Products> lstProductInCategory;
            switch (orderBy)
            {
                case 1:
                    lstProductInCategory =
                    from p in db.Products
                    from co in p.Colors
                    from s in p.Sizes
                    join c in db.Categories on p.CategoryId equals c.CategoryId
                    join b in db.Brands on p.BrandId equals b.BrandId
                    orderby p.ProductPrice ascending
                    where
                    brandId > 0
                    ? (b.BrandId == brandId)
                    : colorId > 0
                    ? (co.ColorId == colorId)
                    : sizeId > 0
                    ? (s.SizeId == sizeId)
                    : (c.CategoryId == id)
                    select new Products
                    {
                        id = p.ProductId,
                        name = p.ProductName,
                        cateId = p.CategoryId,
                        cateName = c.CategoryName,
                        instock = p.Instock,
                        brandId = p.BrandId,
                        brandName = b.BrandName,
                        price = p.ProductPrice,
                        discount = p.ProductDiscount,
                        imagesFeature = p.ProductFeatureImage,
                        status = p.ProductStatus
                    };
                    break;
                case 2:
                    lstProductInCategory =
                    from p in db.Products
                    from co in p.Colors
                    from s in p.Sizes
                    join c in db.Categories on p.CategoryId equals c.CategoryId
                    join b in db.Brands on p.BrandId equals b.BrandId
                    orderby p.ProductPrice descending
                    where
                    brandId > 0
                    ? (b.BrandId == brandId)
                    : colorId > 0
                    ? (co.ColorId == colorId)
                    : sizeId > 0
                    ? (s.SizeId == sizeId)
                    : (c.CategoryId == id)
                    select new Products
                    {
                        id = p.ProductId,
                        name = p.ProductName,
                        cateId = p.CategoryId,
                        cateName = c.CategoryName,
                        instock = p.Instock,
                        brandId = p.BrandId,
                        brandName = b.BrandName,
                        price = p.ProductPrice,
                        discount = p.ProductDiscount,
                        imagesFeature = p.ProductFeatureImage,
                        status = p.ProductStatus
                    };
                    break;
                case 3:
                    lstProductInCategory =
                    from p in db.Products
                    from co in p.Colors
                    from s in p.Sizes
                    join c in db.Categories on p.CategoryId equals c.CategoryId
                    join b in db.Brands on p.BrandId equals b.BrandId
                    orderby p.ProductName ascending
                    where
                    brandId > 0
                    ? (b.BrandId == brandId)
                    : colorId > 0
                    ? (co.ColorId == colorId)
                    : sizeId > 0
                    ? (s.SizeId == sizeId)
                    : (c.CategoryId == id)
                    select new Products
                    {
                        id = p.ProductId,
                        name = p.ProductName,
                        cateId = p.CategoryId,
                        cateName = c.CategoryName,
                        instock = p.Instock,
                        brandId = p.BrandId,
                        brandName = b.BrandName,
                        price = p.ProductPrice,
                        discount = p.ProductDiscount,
                        imagesFeature = p.ProductFeatureImage,
                        status = p.ProductStatus
                    };
                    break;
                case 4:
                    lstProductInCategory =
                    from p in db.Products
                    from co in p.Colors
                    from s in p.Sizes
                    join c in db.Categories on p.CategoryId equals c.CategoryId
                    join b in db.Brands on p.BrandId equals b.BrandId
                    orderby p.ProductName descending
                    where
                    brandId > 0
                    ? (b.BrandId == brandId)
                    : colorId > 0
                    ? (co.ColorId == colorId)
                    : sizeId > 0
                    ? (s.SizeId == sizeId)
                    : (c.CategoryId == id)
                    select new Products
                    {
                        id = p.ProductId,
                        name = p.ProductName,
                        cateId = p.CategoryId,
                        cateName = c.CategoryName,
                        instock = p.Instock,
                        brandId = p.BrandId,
                        brandName = b.BrandName,
                        price = p.ProductPrice,
                        discount = p.ProductDiscount,
                        imagesFeature = p.ProductFeatureImage,
                        status = p.ProductStatus
                    };
                    break;
                default:
                    lstProductInCategory =
                    from p in db.Products
                    from co in p.Colors
                    from s in p.Sizes
                    join c in db.Categories on p.CategoryId equals c.CategoryId
                    join b in db.Brands on p.BrandId equals b.BrandId
                    orderby p.ProductId ascending
                    where
                    brandId > 0
                    ? (b.BrandId == brandId)
                    : colorId > 0
                    ? (co.ColorId == colorId)
                    : sizeId > 0
                    ? (s.SizeId == sizeId)
                    : (c.CategoryId == id)
                    select new Products
                    {
                        id = p.ProductId,
                        name = p.ProductName,
                        cateId = p.CategoryId,
                        cateName = c.CategoryName,
                        instock = p.Instock,
                        brandId = p.BrandId,
                        brandName = b.BrandName,
                        price = p.ProductPrice,
                        discount = p.ProductDiscount,
                        imagesFeature = p.ProductFeatureImage,
                        status = p.ProductStatus
                    };
                    break;


            }

            var Brand = from b in db.Brands
                        select new Brands
                        {
                            id = b.BrandId,
                            image = b.BrandImage,
                            name = b.BrandName,
                            product = (from p in db.Products
                                      where p.BrandId == b.BrandId
                                      select p).Count()
                        };
            ViewBag.Brand = Brand;
            ViewBag.Var = id;

            var Color = from c in db.Colors
                        where c.ColorStatus == true
                        select c;

            ViewBag.Color = Color;

            var Size = from s in db.Sizes
                        where s.SizeStatus == true
                        select s;

            ViewBag.Size = Size;
            

            return View(lstProductInCategory.ToPagedList(pageNumber, pageSize));
        }
    }
}