using ApiDotNet_1._0.Application.DTOs;
using ApiDotNet_1._0.Application.DTOs.Validations;
using ApiDotNet_1._0.Application.Services.Interfaces;
using ApiDotNet_1._0.Domain.Entities;
using ApiDotNet_1._0.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet_1._0.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if(productDTO == null) 
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado!");

            var result = new ProductDTOValidator().Validate(productDTO);
            if (!result.IsValid) 
                return ResultService.ResquestError<ProductDTO>("Problemas na validação!", result);
            
            var product = _mapper.Map<Product>(productDTO);
            var data = await _productRepository.CreateAsync(product);
            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return ResultService.Fail("Produto não encontrado!");
            await _productRepository.DeleteAsync(product);
            return ResultService.Ok($"O produto do id: {id} foi deletado!");
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetAsync()
        {
            var product = await _productRepository.GetProductsAsync();
            return ResultService.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(product));

        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return ResultService.Fail<ProductDTO>("Produto não encontrado");
            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
        
        }

        public async Task<ResultService> UpdateAsync(ProductDTO productDTO)
        {
            if (productDTO == null) return ResultService.Fail("Objeto deve ser informado!");
            
            var validation = new ProductDTOValidator().Validate(productDTO);
            if (!validation.IsValid) return ResultService.ResquestError("Problemas com a validação", validation);

            var product = await _productRepository.GetByIdAsync(productDTO.Id);
            if (product == null) return ResultService.Fail("Produto não encontrado");
            product = _mapper.Map<ProductDTO, Product>(productDTO, product);
            await _productRepository.EditAsync(product);
            return ResultService.Ok($"Produto do id {product.Id} editado");

        }
    }
}
