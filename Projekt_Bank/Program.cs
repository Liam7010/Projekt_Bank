using System;

namespace Projekt_Bank
{
    class Program
    {
        // methods in total: 6                              2d-arrays in total: 2
        //[ Main() - 7 methods if included ]                string[,] accounts
        // Login()                                          decimal[,] balances
        // MainMenu()
        // ShowAccounts()
        // TransferMoney()
        // WithdrawMoney()
        // WaitForEnter()

        // Names of all five users.
        static string[] usernames =
        {
            "Liam",
            "Erik",
            "Annas",
            "Sara",
            "Alex"
        };

        // PIN codes for all five users.
        // The index matches the username index.
        static string[] pinCodes =
        {
            "1111",
            "2222",
            "3333",
            "4444",
            "5555"
        };

        // 2D array containing the account names.
        // The first dimension is the user index, and the second dimension is the account index.
        // First number = user - Second number = account.
        static string[,] accounts =
        {
            { "Lönekonto", "Sparkonto", "" },
            { "Lönekonto", "", "" },
            { "Lönekonto", "Sparkonto", "Resekonto" },
            { "Lönekonto", "Sparkonto", "" },
            { "Lönekonto", "Sparkonto", "Resekonto" }
        };

        // 2D array containing the account balances.
        // The positions match the accounts array.
        static decimal[,] balances =
        {
            { 12500.50m, 5000.25m, 0m },
            { 8500.75m, 0m, 0m },
            { 15000.00m, 3000.50m, 1200.75m },
            { 9200.25m, 4500.00m, 0m },
            { 11000.50m, 7000.75m, 2500.00m }
        };
        // displays starter menu.
        static void Main()
        {
            while (true)
            {
                Console.Clear(); // used for a nicer experince, clears the console before showing the accounts.

                Console.WriteLine("----------------------------------");
                Console.WriteLine("      VÄLKOMMEN TILL BANKEN");
                Console.WriteLine("----------------------------------");

                int user = Login();

                // -1 means that all login attempts failed.
                if (user == -1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Du har använt alla tre försök.");
                    Console.WriteLine("Programmet avslutas.");
                    Console.ReadLine();
                    return;
                }

                MainMenu(user);
            }
        }

        // Handles the login process.
        // Returns the user index if login is successful.
        // Returns -1 after three failed attempts.
        static int Login()
        {
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                Console.Write("Användarnamn: ");
                string inputUsername = Console.ReadLine() ?? "";

                Console.Write("PIN-kod: ");
                string inputPin = Console.ReadLine() ?? "";

                // Remove accidental spaces before checking.
                inputUsername = inputUsername.Trim();
                inputPin = inputPin.Trim();

                // Search through all users.
                for (int i = 0; i < usernames.Length; i++)
                {
                    if (inputUsername.Equals(usernames[i], StringComparison.OrdinalIgnoreCase)
                        && inputPin == pinCodes[i])
                    {
                        Console.WriteLine();
                        Console.WriteLine("Inloggningen lyckades!");
                        Console.WriteLine("Välkommen " + usernames[i] + "!");

                        Console.WriteLine();
                        Console.WriteLine("Klicka Enter för att fortsätta.");
                        Console.ReadLine();
                        return i;
                    }
                }
                
                Console.WriteLine();
                Console.WriteLine("Fel användarnamn eller PIN-kod.");

                if (attempt < 3) // Only show remaining attempts if there are any left.
                {
                    Console.WriteLine(
                        "Du har " + (3 - attempt) + " försök kvar."
                    );

                    Console.WriteLine();
                }
            }

            return -1;
        }

        // Displays main menu.
        static void MainMenu(int user)
        {
            // Variable to track if the user has chosen to log out.
            bool loggedOut = false;
            // Loop until the user chooses to log out.
            while (!loggedOut)
            {
                Console.Clear();// used for a nicer experince.

                Console.WriteLine("----------------------------------");
                Console.WriteLine("            HUVUDMENY");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Inloggad som: " + usernames[user]);

                Console.WriteLine();
                Console.WriteLine("1. Se dina konton och saldo");
                Console.WriteLine("2. Överföring mellan konton");
                Console.WriteLine("3. Ta ut pengar");
                Console.WriteLine("4. Logga ut");
                Console.WriteLine();

                Console.Write("Välj ett alternativ: ");
                // Read the user's input and handle null values.
                string input = Console.ReadLine() ?? "";
                // Check if the input is a valid integer.
                if (int.TryParse(input, out int choice))
                {
                    if (choice == 1) // Show accounts and balances.
                    {
                        ShowAccounts(user);
                    }
                    else if (choice == 2) // Transfer money between accounts.
                    {
                        TransferMoney(user);
                    }
                    else if (choice == 3) // Withdraw money.
                    {
                        WithdrawMoney(user);
                    }
                    else if (choice == 4) // Log out.
                    {
                        loggedOut = true;

                        Console.WriteLine();
                        Console.WriteLine("Du har loggats ut.");
                        Console.WriteLine(
                            "Klicka Enter för att komma till inloggningen."
                        );

                        Console.ReadLine();
                    }
                    else // If the input is a valid integer but not a valid choice.
                    {
                        Console.WriteLine();
                        Console.WriteLine("Ogiltigt val.");
                        WaitForEnter();
                    }
                }
                else // If the input is not a valid integer.
                {
                    Console.WriteLine();
                    Console.WriteLine("Ogiltigt val.");
                    WaitForEnter();
                }
            }
        }


        // Displays all accounts belonging to the logged-in user.
        static void ShowAccounts(int user)
        {
            Console.Clear(); // used for a nicer experince.

            Console.WriteLine("----------------------------------");
            Console.WriteLine("        DINA KONTON OCH SALDO");
            Console.WriteLine("----------------------------------");

            for (int account = 0; account < accounts.GetLength(1); account++)
            {
                if (accounts[user, account] != "")
                {
                    Console.WriteLine((account + 1) + ". " + accounts[user, account] + ": " + balances[user, account].ToString("0.00") + " kr");
                }
            }

            WaitForEnter();
        }


        // Transfers money between two accounts belonging to the same user.
        static void TransferMoney(int user)
        {
            Console.Clear(); // used for a nicer experince.

            Console.WriteLine("----------------------------------");
            Console.WriteLine("          ÖVERFÖRING");
            Console.WriteLine("----------------------------------");

            // Display all accounts with their balances.
            for (int account = 0; account < accounts.GetLength(1); account++)
            {
                if (accounts[user, account] != "")
                {
                    Console.WriteLine((account + 1) + ". " + accounts[user, account] + " - " + balances[user, account].ToString("0.00") + " kr");
                }
            }

            Console.WriteLine();

            Console.Write("Välj konto att ta pengar från: ");
            // Check if the input is a valid integer.
            if (!int.TryParse(Console.ReadLine(), out int fromAccount))
            {
                Console.WriteLine("Ogiltigt val.");
                WaitForEnter();
                return;
            }

            fromAccount--;
            // Check if the fromAccount is valid.
            if (fromAccount < 0 ||
                fromAccount >= accounts.GetLength(1) ||
                accounts[user, fromAccount] == "")
            {
                Console.WriteLine("Ogiltigt konto.");
                WaitForEnter();
                return;
            }

            Console.Write("Välj konto att flytta pengarna till: ");
            // Check if the input is a valid integer.
            if (!int.TryParse(Console.ReadLine(), out int toAccount))
            {
                Console.WriteLine("Ogiltigt val.");
                WaitForEnter();
                return;
            }

            toAccount--;
            // Check if the toAccount is valid.
            if (toAccount < 0 ||
                toAccount >= accounts.GetLength(1) ||
                accounts[user, toAccount] == "")
            {
                Console.WriteLine("Ogiltigt konto.");
                WaitForEnter();
                return;
            }
            // Check if the fromAccount and toAccount are the same.
            if (fromAccount == toAccount)
            {
                Console.WriteLine(
                    "Du kan inte överföra pengar till samma konto."
                );

                WaitForEnter();
                return;
            }

            Console.Write("Hur mycket vill du överföra? ");
            // Check if the input is a valid decimal number.
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Ogiltig summa.");
                WaitForEnter();
                return;
            }
            // Check if the amount is greater than 0.
            if (amount <= 0)
            {
                Console.WriteLine("Summan måste vara större än 0.");
                WaitForEnter();
                return;
            }
            // Check if the user has enough money in the fromAccount.
            if (amount > balances[user, fromAccount])
            {
                Console.WriteLine();
                Console.WriteLine("Du har inte tillräckligt med pengar.");
                WaitForEnter();
                return;
            }

            // Remove money from the first account.
            balances[user, fromAccount] -= amount;

            // Add money to the second account.
            balances[user, toAccount] += amount;

            Console.WriteLine();
            Console.WriteLine("Överföringen lyckades!");
            Console.WriteLine();

            Console.WriteLine(
                accounts[user, fromAccount] +
                " har nu " +
                balances[user, fromAccount].ToString("0.00") +
                " kr."
            );

            Console.WriteLine(accounts[user, toAccount] + " har nu " + balances[user, toAccount].ToString("0.00") + " kr.");
            WaitForEnter();
        }

        // Withdraws money from one of the user's accounts.
        static void WithdrawMoney(int user)
        {
            Console.Clear();// used for a nicer experince.

            Console.WriteLine("----------------------------------");
            Console.WriteLine("          TA UT PENGAR");
            Console.WriteLine("----------------------------------");
            // Display all accounts with their balances.
            for (int account = 0; account < accounts.GetLength(1); account++)
            {
                if (accounts[user, account] != "")
                {
                    Console.WriteLine((account + 1) + ". " + accounts[user, account] + " - " + balances[user, account].ToString("0.00") + " kr");
                }
            }

            Console.WriteLine();

            Console.Write("Välj konto: ");
            // Check if the input is a valid integer.
            if (!int.TryParse(Console.ReadLine(), out int accountChoice))
            {
                Console.WriteLine("Ogiltigt val.");
                WaitForEnter();
                return;
            }
            
            accountChoice--;
            // Check if the account choice is valid.
            if (accountChoice < 0 || accountChoice >= accounts.GetLength(1) || accounts[user, accountChoice] == "")
            {
                Console.WriteLine("Ogiltigt konto.");
                WaitForEnter();
                return;
            }

            Console.Write("Hur mycket vill du ta ut? ");
            // Check if the input is a valid decimal number.
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Ogiltig summa.");
                WaitForEnter();
                return;
            }
            // Check if the amount is greater than 0.
            if (amount <= 0)
            {
                Console.WriteLine("Summan måste vara större än 0.");
                WaitForEnter();
                return;
            }
            // Check if the user has enough money in the account.
            if (amount > balances[user, accountChoice])
            {
                Console.WriteLine();
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot.");

                Console.WriteLine("Ditt saldo är " + balances[user, accountChoice].ToString("0.00") +" kr.");
                WaitForEnter();
                return;
            }

            // Ask for the PIN before allowing the withdrawal.
            Console.WriteLine();
            Console.Write("Skriv din PIN-kod för att bekräfta uttaget: ");

            string confirmationPin = Console.ReadLine() ?? "";
            confirmationPin = confirmationPin.Trim();
            // Check if the entered PIN matches the user's PIN.
            if (confirmationPin != pinCodes[user])
            {
                Console.WriteLine();
                Console.WriteLine("Fel PIN-kod.");
                Console.WriteLine("Uttaget har avbrutits.");
                WaitForEnter();
                return;
            }

            // Remove the money from the account.
            balances[user, accountChoice] -= amount;

            Console.WriteLine();
            Console.WriteLine("Uttaget lyckades!");
            Console.WriteLine("Du tog ut " + amount.ToString("0.00") + " kr.");
            Console.WriteLine();
            Console.WriteLine("Nytt saldo på " + accounts[user, accountChoice] + ": " + balances[user, accountChoice].ToString("0.00") + " kr.");
            WaitForEnter();
        }

        // Waits for the user to press Enter.
        static void WaitForEnter()
        {
            Console.WriteLine();
            Console.WriteLine("Klicka Enter för att komma till huvudmenyn.");
            Console.ReadLine();
        }
    }
}

