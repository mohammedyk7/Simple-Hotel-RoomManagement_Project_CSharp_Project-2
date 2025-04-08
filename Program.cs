using System;

class HotelReservationSystem
{
    const int MAX_ROOMS = 100;
    static int roomCount = 0;
    static int[] roomNumbers = new int[MAX_ROOMS];
    static double[] roomRates = new double[MAX_ROOMS];
    static bool[] isReserved = new bool[MAX_ROOMS];
    static string[] guestNames = new string[MAX_ROOMS];
    static int[] nights = new int[MAX_ROOMS];
    static DateTime[] bookingDates = new DateTime[MAX_ROOMS];

    static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nHotel Reservation System Menu:");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. View All Rooms");
            Console.WriteLine("3. Reserve a Room");
            Console.WriteLine("4. View All Reservations");
            Console.WriteLine("5. Search Reservation by Guest Name");
            Console.WriteLine("6. Find the Highest-Paying Guest");
            Console.WriteLine("7. Cancel Reservation by Room Number");
            Console.WriteLine("8. Exit");
            Console.Write("Select option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddRoom();
                    break;
                case "2":
                    ViewAllRooms();
                    break;
                case "3":
                    MakeReservation();
                    break;
                case "4":
                    ViewReservations();
                    break;
                case "5":
                    SearchReservationByGuestName();
                    break;
                case "6":
                    FindHighestPayingGuest();
                    break;
                case "7":
                    CancelReservation();
                    break;
                case "8":
                    exit = true;
                    Console.WriteLine("Exiting system...");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    static void AddRoom()
    {
        try
        {
            if (roomCount >= MAX_ROOMS)
            {
                Console.WriteLine("Maximum capacity reached!");
                return;
            }

            Console.Write("Enter room number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            // Check if the room number is unique
            bool exists = false;
            for (int i = 0; i < roomCount; i++)
            {
                if (roomNumbers[i] == roomNumber)
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                Console.WriteLine("Room number already exists.");
                return; //to exit the method
            }

            Console.Write("Enter daily rate: ");
            if (!double.TryParse(Console.ReadLine(), out double dailyRate))
            {
                Console.WriteLine("Invalid daily rate.");
                return;
            }

            // Validate the daily rate
            if (dailyRate < 100)
            {
                Console.WriteLine("Daily rate must be at least 100.");
                return;
            }

            // Storing details in an array
            roomNumbers[roomCount] = roomNumber;
            roomRates[roomCount] = dailyRate;
            isReserved[roomCount] = false;
            guestNames[roomCount] = string.Empty;
            roomCount++;

            Console.WriteLine($"Room {roomNumber} added with a daily rate of {dailyRate:C}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a room: {ex.Message}");
        }
    }

    static void ViewAllRooms()
    {
        try
        {
            Console.WriteLine("\nAll Rooms:");
            for (int i = 0; i < roomCount; i++) //the for loop iterates through the list of rooms (roomCount), checking whether each room is reserved or not using the isReserved[i] array.
            {
                if (isReserved[i]) //condition 
                {
                    double totalCost = roomRates[i] * nights[i]; //f the room is reserved (isReserved[i] is true), the total cost of the room is calculated using the formula
                    Console.WriteLine($"Room {roomNumbers[i]} - Rate: {roomRates[i]:C} - Reserved by: {guestNames[i]} - Total Cost: {totalCost:C}"); //"-" as a separator
                }
                else
                {
                    Console.WriteLine($"Room {roomNumbers[i]} - Rate: {roomRates[i]:C} - Available"); //if the room is not reserved (isReserved[i] is false), the code prints the room number, rate, and a message indicating that the room is available
                }
            }
        }
        catch (Exception ex) //to catch any potential error in the block
        {
            Console.WriteLine($"An error occurred while viewing all rooms: {ex.Message}"); //If an error happens, it will display a message 
        }
    }

    static void MakeReservation()
    {
        try
        {
            Console.Write("Enter room number: "); //room must exist 
            if (!int.TryParse(Console.ReadLine(), out int roomNumber)) //check if the user entered a valid integer for the room number. If it's invalid, the system will display "Invalid room number" and exit the method using return.
                                                                       //! logical not "out"pass the roomNumber

            {
                Console.WriteLine("Invalid room number.");
                return; //exit the methoid after checking 
            }

            int index = Array.IndexOf(roomNumbers, roomNumber, 0, roomCount);//checks if the entered room number exists in the roomNumbers
            if (index == -1) //if not found (0=starting index from where to begin the search)
            {
                Console.WriteLine("Room not found."); //when index == -1 means roomnumbers is not found 
                return;
            }

            if (isReserved[index]) // checks if the room that found index is already reserved.
                {
                Console.WriteLine("Room is already reserved.");
                return;
            }

            Console.Write("Enter guest name: ");
            string guestName = Console.ReadLine();

            Console.Write("Enter number of nights: "); // to validate the input.
            if (!int.TryParse(Console.ReadLine(), out int numberOfNights) || numberOfNights <= 0) // (second condition) If the value is invalid or less than or equal to 0, it prints "Number of nights must be greater than 0" and exits
            { // used the logical or to check if the first is true. to check number <=0
                Console.WriteLine("Number of nights must be greater than 0.");
                return; //if 5 is typed it will return true else false // exit if not greater than 0
            }

            guestNames[index] = guestName; // When a room is successfully reserved, the guest's name is stored at the index
            nights[index] = numberOfNights; //stores the number of nights the guest has reserved for the room
            bookingDates[index] = DateTime.Now;
            isReserved[index] = true; //indicating that the room is now reserve

            Console.WriteLine($"Room {roomNumber} reserved for {guestName} for {numberOfNights} nights."); // example: room 101 reserved for mohammed for 2 nights 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while making a reservation: {ex.Message}");
        }
    }

    static void ViewReservations()
    {
        try
        {
            Console.WriteLine("\nReservations:");
            for (int i = 0; i < roomCount; i++)
            {
                if (isReserved[i]) //if its true 
                {
                    double totalCost = roomRates[i] * nights[i]; //if the room is reserved it will calculate then view 
                    Console.WriteLine($"Room {roomNumbers[i]} - Guest: {guestNames[i]} - Nights: {nights[i]} - Rate: {roomRates[i]:C} - Total Cost: {totalCost:C} - Booking Date: {bookingDates[i]}");
                } // added "-" as a separator to make it more oragnizes 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while viewing reservations: {ex.Message}");
        }
    }

    static void SearchReservationByGuestName()
    {
        try
        {
            Console.Write("Enter guest name: ");
            string guestName = Console.ReadLine();

            bool found = false; //acts as a signal: "Yes! We found a matching reservation."
            for (int i = 0; i < roomCount; i++)
            {
                if (isReserved[i]) //true
                {
                    string storedName = guestNames[i].ToLower();
                    string enteredName = guestName.ToLower();

                    if (storedName == enteredName)
                    {
                        // matched reservation
                    }
                }
                
                {
                    double totalCost = roomRates[i] * nights[i];
                    Console.WriteLine($"Room {roomNumbers[i]} - Guest: {guestNames[i]} - Nights: {nights[i]} - Rate: {roomRates[i]:C} - Total Cost: {totalCost:C} - Booking Date: {bookingDates[i]}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Reservation not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while searching for a reservation: {ex.Message}");
        }
    }

    static void FindHighestPayingGuest()
    {
        try
        {
            double highestAmount = 0;
            string highestPayingGuest = "";
            int highestPayingRoom = -1;

            for (int i = 0; i < roomCount; i++)
            {
                if (isReserved[i])
                {
                    double totalCost = roomRates[i] * nights[i];
                    if (totalCost > highestAmount)
                    {
                        highestAmount = totalCost;
                        highestPayingGuest = guestNames[i];
                        highestPayingRoom = roomNumbers[i];
                    }
                }
            }

            if (highestPayingRoom != -1)
            {
                Console.WriteLine($"Highest paying guest is {highestPayingGuest} in room {highestPayingRoom} with a total cost of {highestAmount:C}.");
            }
            else
            {
                Console.WriteLine("No reservations found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while finding the highest-paying guest: {ex.Message}");
        }
    }

    static void CancelReservation()
    {
        try
        {
            Console.Write("Enter room number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            int index = Array.IndexOf(roomNumbers, roomNumber, 0, roomCount);
            if (index == -1)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            if (!isReserved[index])
            {
                Console.WriteLine("Room is not reserved.");
                return;
            }

            isReserved[index] = false;
            guestNames[index] = string.Empty;
            nights[index] = 0;
            bookingDates[index] = default;

            Console.WriteLine($"Reservation for room {roomNumber} has been canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while canceling the reservation: {ex.Message}");
        }
    }
}
