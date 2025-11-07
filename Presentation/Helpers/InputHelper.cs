using System;

namespace ECommerce.Presentation.Helpers
{
    public static class InputHelper
    {
        public static string ReadNonEmpty(string message)
        {
            Console.Write(message);
            var input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? ReadNonEmpty(message) : input.Trim();
        }

        public static int ReadInt(string message)
        {
            Console.Write(message);
            return int.TryParse(Console.ReadLine(), out var value) ? value : ReadInt(message);
        }

        public static decimal ReadDecimal(string message)
        {
            Console.Write(message);
            return decimal.TryParse(Console.ReadLine(), out var value) ? value : ReadDecimal(message);
        }
    }
}
