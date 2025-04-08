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
            return;
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

    static void ViewAllRooms()
    {
        Console.WriteLine("\nAll Rooms:");
        for (int i = 0; i < roomCount; i++) //loop through all rooms
        {
            // Print room number and its status
            Console.Write($"Room {roomNumbers[i]}: ");
        }
        {
            if (isReserved[i])
            {
                double totalCost = roomRates[i] * nights[i];
                Console.WriteLine($"Room {roomNumbers[i]} - Rate: {roomRates[i]:C} - Reserved by: {guestNames[i]} - Total Cost: {totalCost:C}");
            }
            else
            {
                Console.WriteLine($"Room {roomNumbers[i]} - Rate: {roomRates[i]:C} - Available");
            }
        }
    }

    static void MakeReservation()
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

        if (isReserved[index])
        {
            Console.WriteLine("Room is already reserved.");
            return;
        }

        Console.Write("Enter guest name: ");
        string guestName = Console.ReadLine();

        Console.Write("Enter number of nights: ");
        if (!int.TryParse(Console.ReadLine(), out int numberOfNights) || numberOfNights <= 0)
        {
            Console.WriteLine("Number of nights must be greater than 0.");
            return;
        }

        guestNames[index] = guestName;
        nights[index] = numberOfNights;
        bookingDates[index] = DateTime.Now;
        isReserved[index] = true;

        Console.WriteLine($"Room {roomNumber} reserved for {guestName} for {numberOfNights} nights.");
    }

    static void ViewReservations()
    {
        Console.WriteLine("\nReservations:");
        for (int i = 0; i < roomCount; i++)
        {
            if (isReserved[i])
            {
                double totalCost = roomRates[i] * nights[i];
                Console.WriteLine($"Room {roomNumbers[i]} - Guest: {guestNames[i]} - Nights: {nights[i]} - Rate: {roomRates[i]:C} - Total Cost: {totalCost:C} - Booking Date: {bookingDates[i]}");
            }
        }
    }

    static void SearchReservationByGuestName()
    {
        Console.Write("Enter guest name: ");
        string guestName = Console.ReadLine();

        bool found = false;
        for (int i = 0; i < roomCount; i++)
        {
            if (isReserved[i] && guestNames[i].Equals(guestName, StringComparison.OrdinalIgnoreCase))
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

    static void FindHighestPayingGuest()
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

    static void CancelReservation()
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
}
