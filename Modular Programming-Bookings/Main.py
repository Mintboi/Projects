# Author: Martin Swift
# Student Number: R00212573
# Purpose: Modular Programming Assessment


class booking:
    def __init__(self, name, number, plot_type, price, people, pool, kids, booking_id, accommodation_cost):
        self.name = name
        self.number = number
        self.type = plot_type
        self.price = price
        self.people = people
        self.pool = pool
        self.kids = kids
        self.booking_id = booking_id
        self.accommodation_cost = accommodation_cost
        self.string = f"Booking Details\n" + "=" * 19 + f" \nName: {self.name}\nBooking id: {self.booking_id:02}" \
                                                        f"\nAccommodation Type: {self.type}" \
                                                        f"\nNo of People: {self.people}\nPool Pass: {self.pool}" \
                                                        f"\nNo. for kids club: {self.kids}" \
                                                        f"\nCost Accommodation: {self.accommodation_cost}" \
                                                        f"\nTotal Cost: {self.price}"

def read_bookings():
    """
    Takes the bookings file and splits the type of plot, the cost and the number of bookings into three separate lists
    :return: lot_type, cost_of, no_of_bookings
    """
    lot_type = []
    cost_of = []
    no_of_bookings = []
    bookings_file = open("Bookings_2022.txt", "r")
    bookings_list = bookings_file.readlines()
    for i in range(len(bookings_list)):
        lots = bookings_list[i].split(",")
        lot_type.append(lots[0])
        cost = bookings_list[i].split(",")
        cost_of.append(cost[1])
        bookings_no = bookings_list[i].split(",")
        no_of_bookings.append(int(bookings_no[2].strip("\n")))
    return lot_type, cost_of, no_of_bookings


def sites_not_booked(no_of_bookings: list, lot_type):
    """
    this function shows the user the number of free bookings
    as well as the most liked plots
    :return: most_pop
    """
    for i in no_of_bookings:
        largest = 0
        if no_of_bookings[i] > largest:
            largest = no_of_bookings[i]
    most_pop = lot_type[no_of_bookings.index(largest)]
    return most_pop


def book_a_site(bookings, most_pop):
    """
    Takes an input from the user and depending on the input will either display bookings information
    or will take more information and create a booking document for the user
    :return:
    """
    with open("Bookings_2022.txt", "r") as bookings_file:
        bookings_list = bookings_file.readlines()
    print("LONG ISLAND HOLIDAYS \n" + "=" * 19)
    selection = input("1. Make a Booking \n2. Review Bookings \n3. Exit \n=> ")
    booking_id = 00
    deluxe_caravans = 0
    caravans = 0
    camp = 0
    for i in range(30):
        if sum(bookings) >= 30:
            print("Sorry we have exceeded our available slots.")
        if selection == "1" and sum(bookings) < 30:
            # if the user selects to create a booking
            max_price = 0
            price = 0
            accommodation_cost = 0
            name = input("Enter your family name => ")
            while len(name) <= 0 or len(name) > 15:
                if len(name) <= 0:
                    print("Sorry! your name needs to be greater than 0 characters!")
                    name = input("Enter your family name => ")
                elif len(name) > 15:
                    print("Sorry! your name needs to be under 15 characters!")
                    name = input("Enter your family name =>  ")
            number = int(input("Enter your phone number => "))
            while len(str(number)) > 12:
                print("Sorry! your number cannot exceed 12 characters!")
                number = int(input("Enter your phone number => "))
            plot_type = int(input("Choose your accommodation type:\n 1. Deluxe Caravan \n 2. Standard Caravan"
                                  " \n 3. Camp Site \n 4. No Booking \n => "))
            if plot_type == 1:
                price += 2000
                plot_type = "Deluxe Caravan"
                accommodation_cost = 2000
                deluxe_caravans += 1
            elif plot_type == 2:
                price += 1600
                plot_type = "Standard Caravan"
                accommodation_cost = 1600
                caravans = 0
            elif plot_type == 3:
                price += 200
                plot_type = "Camp Site"
                accommodation_cost = 200
                camp += 1
            no_of_people = int(input("Please enter how many guests will be in your group: "))
            pool_club = input("Will your family require a pool pass (Y/N)? ")
            if pool_club.upper() == "Y":
                pool_club = "Yes"
                price += 150
            else:
                pool_club = "No"
            kids_at_club = int(input("How many kids will join the kids club? "))
            while kids_at_club >= no_of_people:
                print("Sorry! children must be supervised at all times by an adult!")
                kids_at_club = int(input("How many kids will join the kids club? "))
            if kids_at_club > 0:
                price += (100 * kids_at_club)
            string = booking(name, number, plot_type, price, no_of_people, pool_club, kids_at_club, booking_id,
                             accommodation_cost).string
            max_price += price
            print(string)
            booking_id += 1
            with open("Bookings_2022.txt", "w") as bookings_write:
                bookings_string = f"Deluxe Caravan, 2000, {deluxe_caravans}\n" \
                                  f"Standard Caravan, 1600, {caravans}\n " \
                                  f"Camp, 200, {camp}\n"
                bookings_write.write(bookings_string)
            with open(f"{name}{booking_id:02}.txt", "w") as f:
                f.write(string)
            with open("Extras.txt", "r") as extras:
                extras_list = extras.readlines()
                for r in range(len(extras_list)):
                    kids_attending = int(extras_list[0].split(",")[1])
                    pool_passes = int(extras_list[1].split(",")[1])
            with open("Extras.txt", "w") as extras:
                replacement = f"Kids Camp, {str(kids_attending + kids_at_club)}\nPool Passes, {str(pool_passes + 1)} "
                extras.write(replacement)

        elif selection == "2":
            if booking_id == 1:
                max_price = 0
            with open("Extras.txt", "r") as extras:
                extras_list = extras.readlines()
                for x in range(len(extras_list)):
                    kids_attending = int(extras_list[0].split(",")[1])
                    pool_passes = int(extras_list[1].split(",")[1])
                # If the user selects to view the bookings information
                print(f"No. of bookings: {sum(bookings)} \nNo. of kids attending kids club: {kids_attending}\n"
                      f"No. of pool passes sold: {pool_passes}\nProjected income: {max_price:.2f}\n"
                      f"Average per booking: {(max_price / booking_id):.2f}\nMost Popular: {most_pop}"
                      f"\nPlots remaining: {30 - sum(bookings)}")

        elif selection == "3":
            break
        add_another_booking = input("Would you like to make another selection? (Y/N) ")
        if add_another_booking.upper() == "Y":
            print("LONG ISLAND HOLIDAYS \n" + "=" * 19)
            selection = input("1. Make a Booking \n2. Review Bookings \n3. Exit \n=> ")
            continue
        else:
            break


def main():
    type_of_plot, cost, bookings = read_bookings()
    most_pop = sites_not_booked(bookings, type_of_plot)
    book_a_site(bookings, most_pop)


main()
