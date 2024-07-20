List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "The Complete Poems of Emily Dickinson",
        Price = 14.99m,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "Leaves of Grass by Walt Whitman",
        Price = 12.50m,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "The Norton Anthology of Poetry",
        Price = 24.99m,
        ProductTypeId = 2
    },
    new Product()
    {
        Name = "Yamaha YTR-2330 Standard Bb Trumpet",
        Price = 499.99m,
        ProductTypeId = 1
    },
    new Product()
    {
        Name = "Bach 180S37 Stradivarius Series Bb Trumpet",
        Price = 2999.99m,
        ProductTypeId = 1
    }
};

List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
        Title = "Brass",
        Id = 1
    },
    new ProductType()
    {
        Title = "Poem",
        Id = 2
    }
};

void DisplayMenu()
{
    string greeting = "Welcome to Brass & Poem!";
    string choice = null;
    while (choice != "5")
    {
        Console.Clear();
        Console.WriteLine(greeting);
        Console.WriteLine(@"Please choose an option:
                        1. Display all products
                        2. Delete a product
                        3. Add a product
                        4. Update product properties
                        5. Exit");
        choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                DisplayAllProducts();
                break;
            case "2":
                DeleteProduct();
                break;
            case "3":
                AddProduct();
                break;
            case "4":
                UpdateProduct();
                break;
            case "5":
                Console.WriteLine("\nGoodbye!");
                break;
            default:
                Console.WriteLine("\nInvalid choice. Please choose a valid option.");
                break;
        }
        if (choice != "5")
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
DisplayMenu();

void DisplayAllProducts()
{
    Console.WriteLine("\nAll products: \n");

    var productList = products
        .Join(productTypes,
            product => product.ProductTypeId,
            productType => productType.Id,
            (product, productType) => new { Product = product, ProductType = productType })
        .Select((p, index) => new
        {
            Index = index + 1,
            ProductName = p.Product.Name,
            ProductTypeTitle = p.ProductType.Title,
            Price = p.Product.Price
        })
        .ToList();

    productList.ForEach(p =>
        Console.WriteLine($"{p.Index}. {p.ProductTypeTitle}: {p.ProductName} - ${p.Price}"));
}

void DeleteProduct()
{
    DisplayAllProducts();

    Console.Write("\nEnter the number of the product you want to delete: ");
    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count)
    {
        var product = products.ElementAt(productIndex - 1);
        products.RemoveAt(productIndex - 1);
        Console.WriteLine($"\n{product.Name} has been deleted.");
    }
    else
    {
        Console.WriteLine("\nInvalid input. Please make a valid selection.");
    }
}

void AddProduct()
{
    Console.WriteLine("\nEnter product details: ");

    Console.Write("\nName: ");
    string name = Console.ReadLine();

    Console.Write("\nPrice: ");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
    {
        Console.WriteLine("\nInvalid input. Price must be a positive number.");
        Console.Write("\nPrice: ");
    }

    Console.WriteLine("\nSelect product type: ");
    foreach (var productType in productTypes)
    {
        Console.WriteLine($"{productType.Id}. {productType.Title}");
    }

    int productTypeId;
    while (!int.TryParse(Console.ReadLine(), out productTypeId) || !productTypes.Any(pt => pt.Id == productTypeId))
    {
        Console.WriteLine("\nInvalid input. Please make a valid selection.");
    }

    products.Add(new Product
    {
        Name = name,
        Price = price,
        ProductTypeId = productTypeId
    });

    Console.WriteLine("\nProduct added successfully.");
}

void UpdateProduct()
{
    DisplayAllProducts();

    Console.Write("\nEnter the number of the product you want to update: ");
    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count)
    {
        var product = products[index - 1];

        Console.Write($"\nEnter new name for '{product.Name}' (or press enter to leave unchanged): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName))
        {
            product.Name = newName;
        }

        Console.Write($"\nEnter new price for '{product.Name}' (or press enter to leave unchanged): ");
        string newPriceInput = Console.ReadLine();
        if (decimal.TryParse(newPriceInput, out decimal newPrice))
        {
            product.Price = newPrice;
        }

        Console.WriteLine("\nSelect new product type: ");
        var productTypeOptions = productTypes
            .Select((pt, i) => new { Index = i + 1, ProductType = pt })
            .ToList();
        productTypeOptions.ForEach(option =>
            Console.WriteLine($"{option.Index}. {option.ProductType.Title}"));

        Console.Write("\nEnter the number of the new product type (or press enter to leave unchanged): ");
        string newProductTypeInput = Console.ReadLine();
        if (int.TryParse(newProductTypeInput, out int newProductTypeIndex) &&
            productTypeOptions.Any(option => option.Index == newProductTypeIndex))
        {
            product.ProductTypeId = productTypeOptions
                .First(option => option.Index == newProductTypeIndex)
                .ProductType.Id;
        }

        Console.WriteLine("\nProduct updated successfully.");
    }
    else
    {
        Console.WriteLine("\nInvalid choice. No product was updated.");
    }
}

public partial class Program { }