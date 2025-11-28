using System;
using System.Collections.Generic;
using CategoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Infrastructure.Data.Configurations
{
    public static class CategorySeeding
    {
        // Convert numeric ID to GUID so it stays stable forever
        private static Guid G(int id)
            => Guid.Parse($"00000000-0000-0000-0000-{id.ToString().PadLeft(12, '0')}");

        public static void SeedCategories(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>();

            void AddCategory(int id, string name, int? parentId = null)
            {
                categories.Add(new Category
                {
                    Id = G(id),
                    CategoryName = name,
                    Description = null,
                    ParentCategoryId = parentId == null ? null : G(parentId.Value),
                    Created = DateTime.UtcNow,
                    IsActive = true
                });
            }

            // LEVEL 1 → LEVEL 2 → LEVEL 3
            //----------------------------------
            // Chipset
            AddCategory(1, "Chipset");

            //   |-- Intel
            AddCategory(9, "Intel", 1);
            AddCategory(11, "Intel 9", 9);
            AddCategory(12, "Intel 7", 9);
            AddCategory(13, "Intel 5", 9);
            AddCategory(14, "Intel 3", 9);

            //   |-- AMD
            AddCategory(10, "AMD", 1);
            AddCategory(15, "AMD R3", 10);
            AddCategory(16, "AMD R5", 10);
            AddCategory(17, "AMD R7", 10);
            AddCategory(18, "AMD R9", 10);


            // VGA
            AddCategory(2, "Video Graphic Array (VGA)");

            // NVIDIA
            AddCategory(19, "NVIDIA", 2);
            AddCategory(20, "RTX 4000 Series", 19);
            AddCategory(21, "RTX 3000 Series", 19);
            AddCategory(22, "GTX 1000 Series", 19);

            // AMD (again but under VGA)
            AddCategory(23, "AMD", 2);
            AddCategory(24, "Radeon RX 7000", 23);
            AddCategory(25, "Radeon RX 6000", 23);
            AddCategory(26, "Radeon RX 5000", 23);

            // VGA Brands
            AddCategory(27, "Brands", 2);
            AddCategory(28, "ASUS", 27);
            AddCategory(29, "MSI", 27);
            AddCategory(30, "Gigabyte", 27);
            AddCategory(31, "EVGA", 27);


            // Motherboard
            AddCategory(3, "Motherboard");

            AddCategory(32, "Intel Platform", 3);
            AddCategory(33, "Z790", 32);
            AddCategory(34, "B760", 32);
            AddCategory(35, "H710", 32);

            AddCategory(36, "AMD Platform", 3);
            AddCategory(37, "X670", 36);
            AddCategory(38, "B650", 36);
            AddCategory(39, "A520", 36);

            AddCategory(40, "Form Factor", 3);
            AddCategory(41, "ATX", 40);
            AddCategory(42, "Micro-ATX", 40);
            AddCategory(43, "Mini-ITX", 40);


            // Storage
            AddCategory(4, "Storage");

            AddCategory(44, "SSD", 4);
            AddCategory(45, "NVMe M.2", 44);
            AddCategory(46, "SATA 2.5\"", 44);
            AddCategory(47, "PCIe 4.0", 44);

            AddCategory(48, "HDD", 4);
            AddCategory(49, "3.5\" Desktop", 48);
            AddCategory(50, "2.5\" Laptop", 48);
            AddCategory(51, "Enterprise", 48);

            AddCategory(52, "Brands", 4);
            AddCategory(53, "Samsung", 52);
            AddCategory(54, "Western Digital", 52);
            AddCategory(55, "Seagate", 52);
            AddCategory(56, "Crucial", 52);


            // PSU
            AddCategory(5, "Power Supply Unit (PSU)");

            AddCategory(57, "Wattage", 5);
            AddCategory(58, "500W-650W", 57);
            AddCategory(59, "700W-850W", 57);
            AddCategory(60, "1000W+", 57);

            AddCategory(61, "Efficiency", 5);
            AddCategory(62, "80+ Gold", 61);
            AddCategory(63, "80+ Platinum", 61);
            AddCategory(64, "80+ Titanium", 61);

            AddCategory(65, "Brands", 5);
            AddCategory(66, "Corsair", 65);
            AddCategory(67, "EVGA", 65);
            AddCategory(68, "Seasonic", 65);
            AddCategory(69, "be quiet!", 65);


            // Cooling + Case
            AddCategory(6, "Cooling Fan, Case");

            AddCategory(70, "CPU Cooling", 6);
            AddCategory(71, "Air Coolers", 70);
            AddCategory(72, "AIO Liquid Coolers", 70);
            AddCategory(73, "Custom Loops", 70);

            AddCategory(74, "Case Fans", 6);
            AddCategory(75, "120mm", 74);
            AddCategory(76, "140mm", 74);
            AddCategory(77, "RGB Fans", 74);

            AddCategory(78, "PC Cases", 6);
            AddCategory(79, "Full Tower", 78);
            AddCategory(80, "Mid Tower", 78);
            AddCategory(81, "Mini ITX", 78);


            // KitPraid's PC
            AddCategory(7, "KitPraid's PC");

            AddCategory(82, "Gaming PC", 7);
            AddCategory(83, "Entry Level", 82);
            AddCategory(84, "Mid Range", 82);
            AddCategory(85, "High End", 82);

            AddCategory(86, "Workstation PC", 7);
            AddCategory(87, "Content Creation", 86);
            AddCategory(88, "CAD/3D Modeling", 86);
            AddCategory(89, "Video Editing", 86);

            AddCategory(90, "Office PC", 7);
            AddCategory(91, "Budget", 90);
            AddCategory(92, "Standard", 90);
            AddCategory(93, "Premium", 90);

            // Apply seeding
            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
