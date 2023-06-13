using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server;

public static class ApplicationDbContextSeed
{ 
    public static async Task SeedQuestionsAsync(ApplicationDbContext context)
    {
        if (await context.Questions.CountAsync() < 20)
            await context.Questions.AddRangeAsync(new Question
                {
                    Text = "Национальное дерево Японии", Answer1 = "Сакура", Answer2 = "Дуб", Answer3 = "Секвойя",
                    Answer4 = "Берёза", CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Из какого зерна делается японский спирт саке?", Answer1 = "Гречка", Answer2 = "Манка",
                    Answer3 = "Рис", Answer4 = "Чечевица", CorrectAnswer = 3
                }, new Question
                {
                    Text = "Какое животное можно увидеть на логотипе Porsche", Answer1 = "Баран", Answer2 = "Конь",
                    Answer3 = "Муха", Answer4 = "Щука", CorrectAnswer = 2
                }, new Question
                {
                    Text = "Какая из планет Солнечной системы самая маленькая?", Answer1 = "Земля", Answer2 = "Юпитер",
                    Answer3 = "Уран", Answer4 = "Меркурий", CorrectAnswer = 4
                },
                new Question
                {
                    Text = "Сколько часовых поясов в России", Answer1 = "5", Answer2 = "11", Answer3 = "12", Answer4 = "7",
                    CorrectAnswer = 2
                },
                new Question
                {
                    Text = "Какая самая маленькая страна в мире?", Answer1 = "Россия", Answer2 = "Ватикан",
                    Answer3 = "Лихтенштейн", Answer4 = "Австралия", CorrectAnswer = 2
                },
                new Question
                {
                    Text = "Сколько людей находится на льду во время хоккейного матча, включая игроков и судей?",
                    Answer1 = "11", Answer2 = "12", Answer3 = "15", Answer4 = "16", CorrectAnswer = 4
                },
                new Question
                {
                    Text = "Какой знак зодиака проходит с 23 августа по 22 сентября?", Answer1 = "Дева", Answer2 = "Весы",
                    Answer3 = "Водолей", Answer4 = "Рыбы", CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Сколько сердец у осьминога?", Answer1 = "1", Answer2 = "2", Answer3 = "3", Answer4 = "4",
                    CorrectAnswer = 3
                },
                new Question
                {
                    Text = "Откуда Том Ям?", Answer1 = "Вьетнам", Answer2 = "Камбоджа", Answer3 = "Сирия",
                    Answer4 = "Таиланд", CorrectAnswer = 4
                },
                new Question
                {
                    Text = "Откуда хачапури?", Answer1 = "Узбекистан", Answer2 = "Грузия", Answer3 = "Таджикистан",
                    Answer4 = "Хорватия", CorrectAnswer = 2
                },
                new Question
                {
                    Text = "В каком знаменитом балете представлены герои принца Зигфрида, Одетты и Одилии?",
                    Answer1 = "Щелкунчик", Answer2 = "Асель", Answer3 = "Двенадцать", Answer4 = "Лебединое озеро",
                    CorrectAnswer = 4
                },
                new Question
                {
                    Text = "Какая единственная планета в солнечной системе на названа " +
                           "в честь греческого бога или богини?",
                    Answer1 = "Земля", Answer2 = "Меркурий", Answer3 = "Венера", Answer4 = "Уран", CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Как звали первого царя из династии Романовых?", Answer1 = "Андрей", Answer2 = "Михаил",
                    Answer3 = "Иван", Answer4 = "Пётр", CorrectAnswer = 2
                },
                new Question
                {
                    Text = "Популярный транспорт, который в песне называют зеленоглазым.", Answer1 = "Такси",
                    Answer2 = "Рикша", Answer3 = "Паровоз", Answer4 = "Самолёт", CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Сколько цветов в радуге", Answer1 = "5", Answer2 = "6", Answer3 = "7", Answer4 = "8",
                    CorrectAnswer = 3
                },
                new Question
                {
                    Text = "Какой герой стал мужем принцессы Фионы?", Answer1 = "Принц Чаминг", Answer2 = "Мэрлин",
                    Answer3 = "Король Артур", Answer4 = "Шрэк", CorrectAnswer = 4
                },
                new Question
                {
                    Text = "Цвет очков, которые носят люди, слишком позитивно воспринимающие мир.",
                    Answer1 = "Фиолетовый", Answer2 = "Красный", Answer3 = "Розовый", Answer4 = "Зелёный",
                    CorrectAnswer = 3
                },
                new Question
                {
                    Text = "Единица наследственного материала, ответственная за формирование какого-либо наследственного" +
                           "признака.", Answer1 = "Ген", Answer2 = "ДНК", Answer3 = "РНК", Answer4 = "Вакуоль",
                    CorrectAnswer = 1
                },
                new Question
                {
                    Text = "Сколько педалей ножного управления в автомобиле с автоматической коробкой передач?",
                    Answer1 = "0", Answer2 = "1", Answer3 = "2", Answer4 = "3", CorrectAnswer = 3
                });
        await context.SaveChangesAsync();
    }
}