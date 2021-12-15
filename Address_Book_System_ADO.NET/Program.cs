using System;

namespace Address_Book_System_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book System ADO.NET \n");

            AddressBookRepo addressBookRepo = new AddressBookRepo();
            AddressBookModel addressBookModel = new AddressBookModel();

            addressBookRepo.DatabaseConnection();
            while (true)
            {
                Console.WriteLine("\nEnter Choice  \n1.AddContact \n2.EditContact \n3.DeleteContact \n4.RetriveStateorCity" +
                                  "\n5.SizeofBook\n6.SortPersonNameByCity\n7.identifyEachAddressbook\n8.CountByBookType\n9.Exit ");
                int choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            //AddressBookModel abmodel = new AddressBookModel();
                            addressBookModel.FirstName = "Mr";
                            addressBookModel.LastName = "Haltung";
                            addressBookModel.Address = "AAAAA";
                            addressBookModel.City = "CCCCC";
                            addressBookModel.State = "MMMMM";
                            addressBookModel.Zipcode = 44444;
                            addressBookModel.PhoneNumber = 999999999;
                            addressBookModel.Email = "mrhaltung@gmail.com";
                            addressBookModel.AddressBookName = "MyBook";
                            addressBookModel.AddressBookType = "family";
                            bool result = addressBookRepo.AddContact(addressBookModel);

                            if (result)
                                Console.WriteLine("Record added successfully...");
                            else
                                Console.WriteLine("Some problem is there...");
                            Console.ReadKey();
                            break;
                        case 2:
                            // Update recods
                            //AddressBookModel abmodel1 = new AddressBookModel();
                            addressBookModel.FirstName = "Mr";
                            addressBookModel.LastName = "Dipesh";
                            addressBookModel.City = "CCCCC";
                            addressBookModel.State = "MMMMM";
                            addressBookModel.Email = "dipesh@gmail.com";
                            addressBookModel.AddressBookName = "MyBook1";
                            addressBookModel.AddressBookType = "Family1";
                            addressBookRepo.EditContactUsingFirstName(addressBookModel);
                            Console.ReadKey();
                            break;
                        case 3:
                            addressBookModel.FirstName = "Dipesh";
                            addressBookRepo.DeleteContactUsingName(addressBookModel);
                            Console.ReadKey();
                            break;
                        case 4:
                            addressBookRepo.RetrieveContactFromPerticularCityOrState();
                            Console.ReadKey();
                            break;
                        case 5:
                            addressBookRepo.AddressBookSizeByCityANDState();
                            Console.ReadKey();
                            break;
                        case 6:
                            addressBookRepo.SortPersonNameByCity();
                            Console.ReadKey();
                            break;
                        case 7:
                            addressBookRepo.identifyEachAddressbooktype();
                            Console.ReadKey();
                            break;
                        case 8:
                            addressBookRepo.GetNumberOfContactsCountByBookType();
                            Console.ReadKey();
                            break;

                        case 9:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please enter the valid choise :: ");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Only enter integer number :: ");
                }
            }
        }
    }
}
