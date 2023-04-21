using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using Npgsql;

namespace Program
{
    class Program
    {
        const string connString = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=zone2415;";
        static void Main(string[] args)
        {
            Console.WriteLine("\nЗдраствуйте!");
            Found:
            Aut:
            Console.WriteLine("\nВыберите действие:\n"+"1.Авторизация\n"+"2.Регистрация");
            
            int vod = Convert.ToInt32(Console.ReadLine());
            if (vod == 1)
            {
                
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();
                authenticateUser(login, password);
                Console.ReadKey();
                
            }
            else if (vod == 2)
            {
                Console.WriteLine("Введите свое ФИО:");
                string nameuser = Console.ReadLine();
                Console.WriteLine("Придумайте логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Придумайте пароль:");
                string password = Console.ReadLine();
                registerUser(nameuser,login, password);
                Console.ReadKey();
                goto Aut;
            }
            else
            {
                Console.WriteLine("Такого действия нет! Попробуйте еще раз");
                goto Found;
            }
        }

        public static void authenticateUser(string login, string password)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                Console.WriteLine("Открытие базы данных");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            string query = $"SELECT COUNT(*) FROM public.\"Users\" WHERE \"Login\"='{login}' AND \"Password\"='{password}'";

            using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    Console.WriteLine("\nАвторизация прошла успешно!\n");
                    showActions();
                }
                else
                {
                    Console.WriteLine("Ошибка авторизации: неверный логин или пароль!");
                    
                }
            }
        }

        public static void showActions()
        {
            
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотреть результаты тестирования");
            Console.WriteLine("2. Поиск результатов тестирования по имени");
            Console.WriteLine("3. Создать анализ результатов тестирования");
            Console.WriteLine("4. Выход");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Просмотреть результаты тестирования
                    break;

                case 2:
                    // Поиск результатов тестирования по имени
                    break;

                case 3:
                    // Создать анализ результатов тестированя
                    break;

                case 4:
                    // Выход
                    Console.WriteLine("Выход из программы...");
                    break;

                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
        public static void registerUser(string nameuser, string login, string password)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                Console.WriteLine("Открытие базы данных");
            }
            catch (Exception y)
            {
                Console.WriteLine(y);
                throw;
            }
            
            string query = $"INSERT INTO public.\"Users\" (\"NameUser\", \"Login\", \"Password\", \"RolesID\") VALUES ('{nameuser}', '{login}', '{password}', '{2}')";

            using (NpgsqlCommand command1 = new NpgsqlCommand(query, conn))
            {
                int com = command1.ExecuteNonQuery();
                
                if (com > 0)
                {
                    Console.WriteLine("Регистрация прошла успешно!\n"+"Нажмите любую клавишу клавиатуры.\n");
                }
                else
                {
                    Console.WriteLine("Ошибка регистрации: не удалось создать новый аккаунт!");
                }
            }
            
        }
        
    }

}