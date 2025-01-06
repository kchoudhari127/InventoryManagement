using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryManagement.Tests.Services
{
     public  class ItemServiceTests
    {
        private readonly IItemService _itemService;
        private readonly Mock<IItemRepository> _itemRepositoryMock;
        private readonly Mock<ILogger<ItemService>> _loggerMock;
        public ItemServiceTests()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            _loggerMock = new Mock<ILogger<ItemService>>();
            _itemService = new ItemService(_itemRepositoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task GetItems_ReturnsAllItems()
        { // Arrange
           var items = new List<Item> { new Item { ItemId = 1, Name = "Item1", Description = "Description1", Quantity = 10, ReorderLevel = 2 }, 
               new Item { ItemId = 2, Name = "Item2", Description = "Description2", Quantity = 20, ReorderLevel = 5 } 
           }; 
            _itemRepositoryMock.Setup(repo => repo.GetItems()).ReturnsAsync(items);
            // Act
               var result = await _itemService.GetItems(); 
            // Assert
               Assert.Equal(2, result.Count());
               Assert.Equal("Item1", result.First().Name); 
         }
        [Fact]
        public async Task GetItemById_ExistingId_ReturnsItem()
        {
            // Arrange
               var item = new Item { ItemId = 1, Name = "Item1", Description = "Description1", Quantity = 10, ReorderLevel = 2 }; 
            _itemRepositoryMock.Setup(repo => repo.GetItemById(1)).ReturnsAsync(item);
            // Act
               var result = await _itemService.GetItemById(1); 
            //Assert
               Assert.NotNull(result);
               Assert.Equal("Item1", result.Name);
        }
        [Fact]
        public async Task GetItemById_NonExistingId_ReturnsNull()
        { // Arrange
           _itemRepositoryMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).ReturnsAsync((Item)null);
            // Act
              var result = await _itemService.GetItemById(99);
            // Assert
                Assert.Null(result); 
        }

        [Fact]
        public async Task AddItem_CallsRepositoryAddMethod()
        { 
            // Arrange
             var itemDto = new ItemDto { ItemId = 1, Name = "NewItem", Description = "NewDescription", Quantity = 10, ReorderLevel = 2 }; 
            // Act
              await _itemService.AddItem(itemDto);
            // Assert
               _itemRepositoryMock.Verify(repo => repo.AddItem(It.IsAny<Item>()), Times.Once);
        }
        [Fact]
        public async Task UpdateItem_CallsRepositoryUpdateMethod()
        {
            // Arrange
            var itemDto = new ItemDto { ItemId = 1, Name = "UpdatedItem", Description = "UpdatedDescription", Quantity = 15, ReorderLevel = 3 };

            // Act
            await _itemService.UpdateItem(itemDto);

            // Assert
            _itemRepositoryMock.Verify(repo => repo.UpdateItem(It.IsAny<Item>()), Times.Once);
        }
        [Fact]
        public async Task DeleteItem_CallsRepositoryDeleteMethod()
        {
            // Arrange
            var itemId = 1;

            // Act
            await _itemService.DeleteItem(itemId);

            // Assert
            _itemRepositoryMock.Verify(repo => repo.DeleteItem(itemId), Times.Once);
        }
        [Fact]
       public async Task GetTotalItemCount_ReturnsCorrectCount()
       {
          // Arrange
         var items = new List<Item>
         {
          new Item { ItemId = 1, Name = "Item1", Description = "Description1", Quantity = 10, ReorderLevel = 2 },
          new Item { ItemId = 2, Name = "Item2", Description = "Description2", Quantity = 20, ReorderLevel = 5 }
         };
         _itemRepositoryMock.Setup(repo => repo.GetItems()).ReturnsAsync(items);

        // Act
           var result = await _itemService.GetTotalItemCount();

        // Assert
           Assert.Equal(2, result);
       }



     }
} 
