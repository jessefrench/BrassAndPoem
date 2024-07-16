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

string greeting = @"Welcome to Brass & Poem!
The only place for poetry books and brass instruments.";

void DisplayMenu()
{
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
                DisplayAllProducts(products, productTypes);
                break;
            case "2":
                DeleteProduct(products, productTypes);
                break;
            case "3":
                AddProduct(products, productTypes);
                break;
            case "4":
                UpdateProduct(products, productTypes);
                break;
            case "5":
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid choice. Please choose a valid option.");
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

void DisplayAllProducts(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("All products: ");

    for (int i = 0; i < products.Count; i++)
    {
        var productType = productTypes.FirstOrDefault(pt => pt.Id == products[i].ProductTypeId);
        Console.WriteLine($"{i + 1}. {productType.Title}: {products[i].Name} - ${products[i].Price}");
    }
}

void DeleteProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Console.Write("Enter the number of the product you want to delete: ");
    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count)
    {
        products.RemoveAt(productIndex - 1);
        Console.WriteLine("The selected product has been deleted.");
    }
    else
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }
}

void AddProduct(List<Product> products, List<ProductType> productTypes)
{
    Console.WriteLine("Enter product details:");

    Console.Write("Name: ");
    string name = Console.ReadLine();

    Console.Write("Price: ");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
    {
        Console.WriteLine("Invalid input. Price must be a positive number.");
        Console.Write("Price: ");
    }

    Console.WriteLine("Select product type: ");
    for (int i = 0; i < productTypes.Count; i++)
    {
        Console.WriteLine($"{productTypes[i].Id}. {productTypes[i].Title}");
    }

    int productTypeId;
    while (!int.TryParse(Console.ReadLine(), out productTypeId) || !productTypes.Any(pt => pt.Id == productTypeId))
    {
        Console.WriteLine("Invalid input. Please make a valid selection.");
    }

    Product newProduct = new Product()
    {
        Name = name,
        Price = price,
        ProductTypeId = productTypeId,
    };

    products.Add(newProduct);
    Console.WriteLine("Product added successfully.");
}

void UpdateProduct(List<Product> products, List<ProductType> productTypes)
{
    DisplayAllProducts(products, productTypes);

    Console.Write("Enter the number of the product you want to update: ");
    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count)
    {
        var product = products[index - 1];

        Console.Write($"Enter new name for '{product.Name}' (or press enter to leave unchanged): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrEmpty(newName))
        {
            product.Name = newName;
        }

        Console.Write($"Enter new price for '{product.Name}' (or press enter to leave unchanged): ");
        string newPriceInput = Console.ReadLine();
        if (decimal.TryParse(newPriceInput, out decimal newPrice))
        {
            product.Price = newPrice;
        }

        Console.WriteLine("Select new product type: ");
        for (int i = 0; i < productTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {productTypes[i].Title}");
        }
        Console.Write("Enter the number of the new product type (or press enter to leave unchanged): ");
        string newProductTypeInput = Console.ReadLine();
        if (int.TryParse(newProductTypeInput, out int newProductTypeIndex) && newProductTypeIndex > 0 && newProductTypeIndex <= productTypes.Count)
        {
            product.ProductTypeId = productTypes[newProductTypeIndex - 1].Id;
        }

        Console.WriteLine("Product updated successfully.");
    }
    else
    {
        Console.WriteLine("Invalid choice. No product was updated.");
    }
}

// don't move or change this!
public partial class Program { }