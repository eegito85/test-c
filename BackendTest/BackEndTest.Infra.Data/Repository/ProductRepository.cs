﻿using BackEndTest.Domain.Entities;
using BackEndTest.Domain.Interfaces;
using BackEndTest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BackEndTest.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            if(product != null)
            {
                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    throw new System.ArgumentException("Não foi possível criar o produto");
                }
            }
            return false;
        }

        public List<int> GetAllProductSku()
        {
            var products = _context.Products.ToList();
            List<int> skuList = new List<int>();
            foreach (var product in products)
            {
                skuList.Add(product.Sku);
            }
            return skuList;
        }

        public async Task<Product> GetProductBySkuAsync(int sku)
        {
            return await _context.Products.Where(i => i.Sku == sku).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveProductBySkuAsync(int sku)
        {
            var product = _context.Products.Where(i => i.Sku == sku).FirstOrDefault();
            if (product != null)
            {
                try
                {
                    _context.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    throw new System.ArgumentException("Não foi possível remover o produto");
                }
            }
            return false;
        }

        public async Task<bool> UpdateProductBySkuAsync(Product product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Não foi possível atualizar o produto");
            }

            return false;
        }
    }
}
