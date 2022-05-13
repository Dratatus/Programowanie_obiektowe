using lab_07.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_07
{
    public class Program
    {
        private static IDataAccess _dataAccess;

        public static void Main(string[] args)
        {
            // ustawienie zmiennej typu interfejsu IDataAccess konkretnej implementacji
            // MSSqlDataAccess;
            // XmlDataAccess;
            _dataAccess = new XmlDataAccess();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("1. Wyświetl listę użytkowników");
                Console.WriteLine("2. Dodaj nowego użytkownika");
                Console.WriteLine("3. Edytuj użytkownika");
                Console.WriteLine("4. Usuń użytkownika");
                Console.WriteLine();

                string option = GetAnswerFromUser("Wybierz opcje:", "1", "2", "3", "4");

                switch (option)
                {
                    case "1":

                        DisplayAllUsersOption(true, true);
                        break;

                    case "2":

                        InsertNewUserOption();
                        break;

                    case "3":

                        EditUserOption();
                        break;

                    case "4":

                        DeleteUserOption();
                        break;
                }
            }
        }

        private static void DisplayAllUsersOption(bool waitForReaction, bool handleAndDisplayException)
        {
            try
            {
                List<UserEntity> users = _dataAccess.GetAll();

                Console.WriteLine();
                Console.WriteLine($"Liczba użytkowników: {users.Count}");
                Console.WriteLine();

                foreach (var user in users)
                {
                    Console.WriteLine($"Id: {user.Id}");
                    Console.WriteLine($"Imię: {user.Name}");
                    Console.WriteLine($"Rola: {user.Role}");
                    Console.WriteLine($"Data dodania: {(user.CreatedAt.HasValue ? user.CreatedAt.Value.ToLongDateString() : "NULL")}");
                    Console.WriteLine($"Data usunięcia: {(user.RemovedAt.HasValue ? user.RemovedAt.Value.ToLongDateString() : "NULL")}");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                if (handleAndDisplayException)
                {
                    DisplayExceptionInfo("Wystąpił nieoczekiwany błąd podczas pobierania danych", e);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if (waitForReaction)
                {
                    WaitForReaction(); 
                }
            }
        }

        private static void InsertNewUserOption()
        {
            try
            {
                UserEntity user = new UserEntity
                {
                    Name = GetStringFromUser("Podaj imię użytkownika: "),
                    Role = GetAnswerFromUser("Podaj rolę użytkownika: ", "ADMIN", "MODERATOR", "TEACHER", "STUDENT")
                };

                _dataAccess.AddNew(user);

                Console.WriteLine($"Poprawnie edytowano użytkownika  {user.Name} | {user.Role}");
            }
            catch (Exception e)
            {
                DisplayExceptionInfo("Wystąpił nieoczekiwany błąd podczas dodawania", e);
            }
            finally
            {
                WaitForReaction();
            }
        }

        private static void EditUserOption()
        {
            try
            {
                DisplayAllUsersOption(false, false);

                List<UserEntity> users = _dataAccess.GetAll();

                bool noUserExists = !users.Any();

                if (noUserExists)
                {
                    Console.WriteLine("Nie ma studentów do edycji");
                    WaitForReaction();
                    return;
                }

                List<string> userIds = users.Select(u => u.Id.ToString()).ToList();

                string userToEditIdString = GetAnswerFromUser("Podaj id studenta do edycji: ", userIds.ToArray());

                long userToEditId = Convert.ToInt64(userToEditIdString);

                UserEntity userToEdit = users.Single(u => u.Id == userToEditId);

                UserEntity newUserData = new UserEntity
                {
                    Name = GetStringFromUser("Podaj imię użytkownika: "),
                    Role = GetAnswerFromUser("Podaj rolę użytkownika: ", "ADMIN", "MODERATOR", "TEACHER", "STUDENT")
                };

                _dataAccess.Edit(userToEditId, newUserData);

                Console.WriteLine();
                Console.WriteLine($"Poprawnie edytowano użytkownika\nZ: {userToEdit.Name} | {userToEdit.Role}\n NA:{newUserData.Name} | {newUserData.Role}");

            }
            catch (Exception e)
            {
                DisplayExceptionInfo("Wystąpił nieoczekiwany błąd podczas edycji", e);
            }
            finally
            {
                WaitForReaction();
            }
        }

        private static void DeleteUserOption()
        {
            try
            {
                DisplayAllUsersOption(false, false);

                List<UserEntity> users = _dataAccess.GetAll();

                bool noUserExists = !users.Any();

                if (noUserExists)
                {
                    Console.WriteLine("Nie ma studentów do usunięcia");
                    WaitForReaction();
                    return;
                }

                List<string> userIds = users.Select(u => u.Id.ToString()).ToList();

                string userToEditIdString = GetAnswerFromUser("Podaj id studenta do usunięcia: ", userIds.ToArray());

                long userToDeleteId = Convert.ToInt64(userToEditIdString);

                UserEntity userToDelete = users.Single(u => u.Id == userToDeleteId);

                _dataAccess.Delete(userToDeleteId);

                Console.WriteLine();
                Console.WriteLine($"Poprawnie usunięto użytkownika  {userToDelete.Id} | {userToDelete.Name} | {userToDelete.Role}");
            }
            catch (Exception e)
            {
                DisplayExceptionInfo("Wystąpił nieoczekiwany błąd podczas usuwania", e);
            }
            finally
            {
                WaitForReaction();
            }
        }

        private static string GetStringFromUser(string message)
        {
            Console.Write(message);
            string stringFromUser = Console.ReadLine();
            return stringFromUser;
        }

        private static string GetAnswerFromUser(string message, params string[] validAnswers)
        {
            while (true)
            {
                string validAnswersString = string.Join("/", validAnswers);

                string messageString = $"{message} {validAnswersString}  :  ";

                Console.Write(messageString);

                string answer = Console.ReadLine();

                bool isAnswerValid = validAnswers.Contains(answer);

                if (isAnswerValid)
                {
                    return answer;
                }
            }
        }

        private static void WaitForReaction()
        {
            Console.WriteLine();
            Console.Write("Kliknij dowolny przycisk...");
            Console.ReadKey();
        }

        private static void DisplayExceptionInfo(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine($"Komunikat błędu:\n{exception.Message}");
            Console.WriteLine();
            Console.WriteLine($"Stos:\n{exception.StackTrace}");
        }
    }
}
