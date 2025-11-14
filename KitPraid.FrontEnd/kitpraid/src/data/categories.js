// Default categories data for seeding
const defaultCategories = [
  {
    Id: 1,
    Name: "Chipset",
    categoriesSub1: [
      {
        Id: 9,
        Name: "Intel",
        categoriesSub2: [
          {
            Id: 11,
            Name: "Intel 9",
          },
          {
            Id: 12,
            Name: "Intel 7",
          },
          {
            Id: 13,
            Name: "Intel 5",
          },
          {
            Id: 14,
            Name: "Intel 3",
          },
        ],
      },
      {
        Id: 10,
        Name: "AMD",
        categoriesSub2: [
          // Fixed property name to match Intel structure
          {
            Id: 15,
            Name: "AMD R3",
          },
          {
            Id: 16,
            Name: "AMD R5",
          },
          {
            Id: 17,
            Name: "AMD R7",
          },
          {
            Id: 18,
            Name: "AMD R9",
          },
        ],
      },
    ],
  },
  {
    Id: 2,
    Name: "Video Graphic Array (VGA)",
    categoriesSub1: [
      {
        Id: 19,
        Name: "NVIDIA",
        categoriesSub2: [
          {
            Id: 20,
            Name: "RTX 4000 Series",
          },
          {
            Id: 21,
            Name: "RTX 3000 Series",
          },
          {
            Id: 22,
            Name: "GTX 1000 Series",
          },
        ],
      },
      {
        Id: 23,
        Name: "AMD",
        categoriesSub2: [
          {
            Id: 24,
            Name: "Radeon RX 7000",
          },
          {
            Id: 25,
            Name: "Radeon RX 6000",
          },
          {
            Id: 26,
            Name: "Radeon RX 5000",
          },
        ],
      },
      {
        Id: 27,
        Name: "Brands",
        categoriesSub2: [
          {
            Id: 28,
            Name: "ASUS",
          },
          {
            Id: 29,
            Name: "MSI",
          },
          {
            Id: 30,
            Name: "Gigabyte",
          },
          {
            Id: 31,
            Name: "EVGA",
          },
        ],
      },
    ],
  },
  {
    Id: 3,
    Name: "Motherboard",
    categoriesSub1: [
      {
        Id: 32,
        Name: "Intel Platform",
        categoriesSub2: [
          {
            Id: 33,
            Name: "Z790",
          },
          {
            Id: 34,
            Name: "B760",
          },
          {
            Id: 35,
            Name: "H710",
          },
        ],
      },
      {
        Id: 36,
        Name: "AMD Platform",
        categoriesSub2: [
          {
            Id: 37,
            Name: "X670",
          },
          {
            Id: 38,
            Name: "B650",
          },
          {
            Id: 39,
            Name: "A520",
          },
        ],
      },
      {
        Id: 40,
        Name: "Form Factor",
        categoriesSub2: [
          {
            Id: 41,
            Name: "ATX",
          },
          {
            Id: 42,
            Name: "Micro-ATX",
          },
          {
            Id: 43,
            Name: "Mini-ITX",
          },
        ],
      },
    ],
  },
  {
    Id: 4,
    Name: "Storage",
    categoriesSub1: [
      {
        Id: 44,
        Name: "SSD",
        categoriesSub2: [
          {
            Id: 45,
            Name: "NVMe M.2",
          },
          {
            Id: 46,
            Name: 'SATA 2.5"',
          },
          {
            Id: 47,
            Name: "PCIe 4.0",
          },
        ],
      },
      {
        Id: 48,
        Name: "HDD",
        categoriesSub2: [
          {
            Id: 49,
            Name: '3.5" Desktop',
          },
          {
            Id: 50,
            Name: '2.5" Laptop',
          },
          {
            Id: 51,
            Name: "Enterprise",
          },
        ],
      },
      {
        Id: 52,
        Name: "Brands",
        categoriesSub2: [
          {
            Id: 53,
            Name: "Samsung",
          },
          {
            Id: 54,
            Name: "Western Digital",
          },
          {
            Id: 55,
            Name: "Seagate",
          },
          {
            Id: 56,
            Name: "Crucial",
          },
        ],
      },
    ],
  },
  {
    Id: 5,
    Name: "Power Supply Unit (PSU)",
    categoriesSub1: [
      {
        Id: 57,
        Name: "Wattage",
        categoriesSub2: [
          {
            Id: 58,
            Name: "500W-650W",
          },
          {
            Id: 59,
            Name: "700W-850W",
          },
          {
            Id: 60,
            Name: "1000W+",
          },
        ],
      },
      {
        Id: 61,
        Name: "Efficiency",
        categoriesSub2: [
          {
            Id: 62,
            Name: "80+ Gold",
          },
          {
            Id: 63,
            Name: "80+ Platinum",
          },
          {
            Id: 64,
            Name: "80+ Titanium",
          },
        ],
      },
      {
        Id: 65,
        Name: "Brands",
        categoriesSub2: [
          {
            Id: 66,
            Name: "Corsair",
          },
          {
            Id: 67,
            Name: "EVGA",
          },
          {
            Id: 68,
            Name: "Seasonic",
          },
          {
            Id: 69,
            Name: "be quiet!",
          },
        ],
      },
    ],
  },
  {
    Id: 6,
    Name: "Cooling Fan, Case",
    categoriesSub1: [
      {
        Id: 70,
        Name: "CPU Cooling",
        categoriesSub2: [
          {
            Id: 71,
            Name: "Air Coolers",
          },
          {
            Id: 72,
            Name: "AIO Liquid Coolers",
          },
          {
            Id: 73,
            Name: "Custom Loops",
          },
        ],
      },
      {
        Id: 74,
        Name: "Case Fans",
        categoriesSub2: [
          {
            Id: 75,
            Name: "120mm",
          },
          {
            Id: 76,
            Name: "140mm",
          },
          {
            Id: 77,
            Name: "RGB Fans",
          },
        ],
      },
      {
        Id: 78,
        Name: "PC Cases",
        categoriesSub2: [
          {
            Id: 79,
            Name: "Full Tower",
          },
          {
            Id: 80,
            Name: "Mid Tower",
          },
          {
            Id: 81,
            Name: "Mini ITX",
          },
        ],
      },
    ],
  },
  {
    Id: 7,
    Name: "KitPraid's PC",
    categoriesSub1: [
      {
        Id: 82,
        Name: "Gaming PC",
        categoriesSub2: [
          {
            Id: 83,
            Name: "Entry Level",
          },
          {
            Id: 84,
            Name: "Mid Range",
          },
          {
            Id: 85,
            Name: "High End",
          },
        ],
      },
      {
        Id: 86,
        Name: "Workstation PC",
        categoriesSub2: [
          {
            Id: 87,
            Name: "Content Creation",
          },
          {
            Id: 88,
            Name: "CAD/3D Modeling",
          },
          {
            Id: 89,
            Name: "Video Editing",
          },
        ],
      },
      {
        Id: 90,
        Name: "Office PC",
        categoriesSub2: [
          {
            Id: 91,
            Name: "Budget",
          },
          {
            Id: 92,
            Name: "Standard",
          },
          {
            Id: 93,
            Name: "Premium",
          },
        ],
      },
    ],
  },
];

export default defaultCategories;
