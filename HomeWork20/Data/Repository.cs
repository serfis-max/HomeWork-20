using System;

namespace HW_201.Data
{
    public class Repository
    {
        Random r = new Random();

        public enum Name
        {
            First,
            Middle,
            Last
        }

        #region Списки имен
        readonly private string[] FirstNames =
            {
                    "Альрик",
                    "Вилмар",
                    "Герхард",
                    "Зигвард",
                    "Зигфрит",
                    "Ивар",
                    "Ингвар",
                    "Ньорд",
                    "Рагнар",
                    "Рэгнволд",
                    "Севверин",
                    "Стейнвульф",
                    "Торгейрд",
                    "Торстейн",
                    "Ульвар",
                    "Ульрих",
                    "Халлстейн",
                    "Хаук",
                    "Эггил",
                };

        readonly private string[] MiddleNames =
            {
                    "Андерссон",
                    "Виртанен",
                    "Густафссон",
                    "Джонсон",
                    "Карлсен",
                    "Карлссон",
                    "Корхонен",
                    "Кристиансен",
                    "Ларссон",
                    "Мякинен",
                    "Нильссон",
                    "Педерсен",
                    "Расмуссен",
                    "Свенссон",
                    "Ульссон",
                    "Хансен",
                    "Хейкинен",
                    "Юханссон",
                    "Ярвинен"
                };

        readonly private string[] LastNames =
            {
                    "Альриксон",
                    "Вилмарсон",
                    "Герхардсон",
                    "Зигфритсон",
                    "Иварсон",
                    "Ингварсон",
                    "Ньордсон",
                    "Ойвиндсон",
                    "Рагнарсон",
                    "Рэгнволдсон",
                    "Севверинсон",
                    "Стейнвульфсон",
                    "Торгейрдсон",
                    "Торстейнсон",
                    "Ульварсон",
                    "Ульрихсон",
                    "Халлстейнсон",
                    "Хауксон",
                    "Эггилсон",
                };
        #endregion

        public User NewUser()
        {
            return new User()
            {
                Surname = $"{GetNotation(Name.Middle)}",
                FirstName = $"{GetNotation(Name.First)}",
                Patronymic = $"{GetNotation(Name.Last)}",
                PhoneNumber = $"+7 {r.Next(9999999):0000000}",
                Adress = (r.Next(2) % 2 == 0) ? "где-то там" : "где-то здесь",
                Description = "строка\nновая строка\n\tновая строка и таб"
            };
        }

        private string GetNotation(Name n)
        {
            var qwe = r.Next(19);
            switch (n)
            {
                case Name.First: return FirstNames[qwe];
                case Name.Middle: return MiddleNames[qwe];
                case Name.Last: return LastNames[qwe];
            }
            return "";
        }
    }
}
