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

void DisplayAllProducts() // method to display all products with their details
{
    Console.WriteLine("\nAll products: \n"); // print a header for the product list

    var productList = products // start with the collection of products
        .Join(productTypes, // perform an inner join with the product types collection
            product => product.ProductTypeId, // key selector for products
            productType => productType.Id, // key selector for product types
            (product, productType) => new { Product = product, ProductType = productType }) // result selector combining product and productType into a new anonymous object
        .Select((p, index) => new // select each combined object with an index
        {
            Index = index + 1, // add 1 to the index to start from 1 instead of 0
            ProductName = p.Product.Name, // select the product name
            ProductTypeTitle = p.ProductType.Title, // select the product type title
            Price = p.Product.Price // select the product price
        })
        .ToList(); // convert the result to a list

    productList.ForEach(p => // iterate over each item in the product list
        Console.WriteLine($"{p.Index}. {p.ProductTypeTitle}: {p.ProductName} - ${p.Price}")); // print the product details
}

void DeleteProduct() // method to delete a product based on user input
{
    DisplayAllProducts(); // call the method to display all products

    Console.Write("\nEnter the number of the product you want to delete: "); // prompt the user to enter the number of the product to delete
    if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count) // try to parse the user input to an integer and check if it's a valid index
    {
        var product = products.ElementAt(productIndex - 1); // get the product at the specified index (subtract 1 to convert from 1-based to 0-based index)
        products.RemoveAt(productIndex - 1); // remove the product from the list
        Console.WriteLine($"\n{product.Name} has been deleted."); // inform the user that the product has been deleted
    }
    else // if the input is invalid
    {
        Console.WriteLine("\nInvalid input. Please make a valid selection."); // inform the user that the input is invalid
    }
}

void AddProduct() // method to add a new product based on user input
{
    Console.WriteLine("\nEnter product details: "); // prompt the user to enter product details

    Console.Write("\nName: "); // prompt the user to enter the product name
    string name = Console.ReadLine(); // read the product name from user input

    Console.Write("\nPrice: "); // prompt the user to enter the product price
    decimal price; // declare a variable to hold the product price
    while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0) // try to parse the user input to a decimal and check if it's a positive number
    {
        Console.WriteLine("\nInvalid input. Price must be a positive number."); // inform the user that the input is invalid
        Console.Write("\nPrice: "); // prompt the user to enter the price again
    }

    Console.WriteLine("\nSelect product type: "); // prompt the user to select a product type
    foreach (var productType in productTypes) // iterate over each product type
    {
        Console.WriteLine($"{productType.Id}. {productType.Title}"); // print the product type id and title
    }

    int productTypeId; // declare a variable to hold the product type id
    while (!int.TryParse(Console.ReadLine(), out productTypeId) || !productTypes.Any(pt => pt.Id == productTypeId)) // try to parse the user input to an integer and check if it's a valid product type id
    {
        Console.WriteLine("\nInvalid input. Please make a valid selection."); // inform the user that the input is invalid
    }

    products.Add(new Product // add a new product to the products collection
    {
        Name = name, // set the product name
        Price = price, // set the product price
        ProductTypeId = productTypeId // set the product type id
    });

    Console.WriteLine("\nProduct added successfully."); // inform the user that the product has been added successfully
}

void UpdateProduct() // method to update an existing product based on user input
{
    DisplayAllProducts(); // call the method to display all products

    Console.Write("\nEnter the number of the product you want to update: "); // prompt the user to enter the number of the product to update
    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= products.Count) // try to parse the user input to an integer and check if it's a valid index
    {
        var product = products[index - 1]; // get the product at the specified index (subtract 1 to convert from 1-based to 0-based index)

        Console.Write($"\nEnter new name for '{product.Name}' (or press enter to leave unchanged): "); // prompt the user to enter a new name for the product
        string newName = Console.ReadLine(); // read the new name from user input
        if (!string.IsNullOrEmpty(newName)) // check if the new name is not empty
        {
            product.Name = newName; // update the product name
        }

        Console.Write($"\nEnter new price for '{product.Name}' (or press enter to leave unchanged): "); // prompt the user to enter a new price for the product
        string newPriceInput = Console.ReadLine(); // read the new price from user input
        if (decimal.TryParse(newPriceInput, out decimal newPrice)) // try to parse the new price to a decimal
        {
            product.Price = newPrice; // update the product price
        }

        Console.WriteLine("\nSelect new product type: "); // prompt the user to select a new product type
        var productTypeOptions = productTypes // create a list of product type options with index
            .Select((pt, i) => new { Index = i + 1, ProductType = pt }) // select each product type with an index
            .ToList(); // convert the result to a list
        productTypeOptions.ForEach(option => // iterate over each product type option
            Console.WriteLine($"{option.Index}. {option.ProductType.Title}")); // print the product type index and title

        Console.Write("\nEnter the number of the new product type (or press enter to leave unchanged): "); // prompt the user to enter the number of the new product type
        string newProductTypeInput = Console.ReadLine(); // read the new product type input from user
        if (int.TryParse(newProductTypeInput, out int newProductTypeIndex) && // try to parse the new product type input to an integer
            productTypeOptions.Any(option => option.Index == newProductTypeIndex)) // check if the new product type index is valid
        {
            product.ProductTypeId = productTypeOptions // update the product type id
                .First(option => option.Index == newProductTypeIndex) // get the product type by index
                .ProductType.Id; // set the product type id
        }

        Console.WriteLine("\nProduct updated successfully."); // inform the user that the product has been updated successfully
    }
    else // if the input is invalid
    {
        Console.WriteLine("\nInvalid choice. No product was updated."); // inform the user that the input is invalid
    }
}

public partial class Program { }