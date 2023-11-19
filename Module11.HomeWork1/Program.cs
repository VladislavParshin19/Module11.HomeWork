using System;
using System.Linq;

namespace Module11.HomeWork
{
    class Program
    {
        public interface EmpInfo
        {
            string ToString();
        }

        public struct Employee : EmpInfo
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Position { get; set; }
            public decimal Salary { get; set; }
            public DateTime Date { get; set; }
            public char Gender { get; set; }

            public string ToString()
            {
                return $"{LastName}, {FirstName}, {Position}, {Salary}, {Date}, {Gender}";
            }

        }
        static void Main()
        {
            //Длина массива
            Console.Write("Ведите количество сотрудников: ");
            int arrayLenght = int.Parse(Console.ReadLine());

            Employee[] employees = new Employee[arrayLenght];

            //Заполнение массива
            for (int i = 0; i < arrayLenght; i++)
            {
                Console.WriteLine($"Введите информацию о сотруднике {i + 1}:");
                employees[i] = new Employee
                {
                    LastName = GetTextInput("Фамилия: "),
                    FirstName = GetTextInput("Имя: "),
                    Position = GetTextInput("Должность: "),
                    Salary = GetDecimalInput("Зарплата: "),
                    Date = GetDateInput("Дaта: "),
                    Gender = GetCharInput("Пол(M или F): "),
                };
            }

            //Вывод всей информации оо сотрудниках
            Console.WriteLine("Полная информация о сотрудниках: ");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }


            //Вывод информации о сотрудниках определенной должности
            string selectedPosition = GetTextInput("Введите должность: ");
            Console.WriteLine($"Информация о сотрудниках на должности {selectedPosition}: ");
            foreach (var employee in employees.Where(a => a.Position == selectedPosition))
            {
                Console.WriteLine(employee.ToString());
            }

            // Нахождение менеджеров с зарплатой выше средней зарплаты клерков
            decimal averageClerkSalary = employees.Where(a => a.Position == "Clerk").Average(a => a.Salary);
            Console.WriteLine($"Менеджеры с зарплатой выше средней зарплаты клерков ({averageClerkSalary}): ");
            foreach (var manager in employees.Where(a => a.Position == "Manager" && a.Salary > averageClerkSalary).OrderBy(a => a.LastName))
            {
                Console.WriteLine(manager.ToString());
            }

            // Вывод информации о сотрудниках, принятых на работу позже определенной даты
            DateTime filterDate = GetDateInput("Введите дату для фильтрации сотрудников: ");
            Console.WriteLine($"Информация о сотрудниках, принятых на работу позже {filterDate.ToShortDateString()}, отсортированная по фамилии:");
            foreach (var employee in employees.Where(a => a.Date > filterDate).OrderBy(a => a.LastName))
            {
                Console.WriteLine(employee.ToString());
            }

            // Вывод информации о сотрудниках определенного пола
            char genderFilter = GetCharInput("Выберите пол для фильтрации(M - мужчины, F - женщины): ");
            Console.WriteLine($"Информация о сотрудниках, пол: {genderFilter}: ");
            foreach (var employee in employees.Where(a => a.Gender == genderFilter || genderFilter != 'M' && genderFilter != 'F'))
            {
                Console.WriteLine(employee.ToString());
            }

            Console.ReadLine();
        }

        //Список методов для более удобного заполнения данных
        static string GetTextInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static decimal GetDecimalInput(string prompt)
        {
            decimal result;
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                else
                {
                    Console.Write(prompt);
                }
            }
        }
        static DateTime GetDateInput(string prompt)
        {
            DateTime result;
            while (true)
            {
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", null, System.Globalization.DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    Console.Write(prompt);
                }
            }
        }
        static char GetCharInput(string prompt)
        {
            char result;
            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out result) && (result == 'M' || result == 'F'))
                {
                    return result;
                }
                else
                {
                    Console.Write(prompt);
                }
            }
        }
    }
}
