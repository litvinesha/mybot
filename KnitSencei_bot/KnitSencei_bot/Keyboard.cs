using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace KnitSencei_bot
{
    class Keyboard
    {
        public async void Start(TelegramBotClient Bot, MessageEventArgs e)
        {
            string text =
 @"Список команд :
/start - запуск бота,
/inline - связь с разработчиком,
/keyboard - вывод клавиатуры,
/help - просить о помощи Сенсея,
/calculator - вызвать калькулятор количества пряжи
/ideas- показать идеи для вязания";
            await Bot.SendTextMessageAsync(e.Message.From.Id, text);
        }

        public async void Inline(TelegramBotClient Bot, MessageEventArgs e)
        {
            var inlineKeyboard = new InlineKeyboardMarkup
                    (new[]
                    {new[]
                        {
                            InlineKeyboardButton.WithUrl("VK", "https://vk.com/id10618217"),
                            InlineKeyboardButton.WithUrl("Telegram","https://t.me/litvinesha")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithUrl("Google+", "https://plus.google.com/109645919916102660305"),
                        }
                    });
            await Bot.SendTextMessageAsync(e.Message.From.Id, "Выбери пункт меню", replyMarkup: inlineKeyboard);
        }

        public async void Keyboard_k(TelegramBotClient Bot, MessageEventArgs e)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new []
                        {
                            new KeyboardButton("Ты кто?"),
                            new KeyboardButton("Привет!")
                        },
                        new []
                        {
                            new KeyboardButton("Помоги мне!"),
                            new KeyboardButton("Как дела?")
                        }
                    });
            replyKeyboard.ResizeKeyboard = true;
            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "А вот и Я!", replyMarkup: replyKeyboard);
        }

        public async void Help(TelegramBotClient Bot, MessageEventArgs e)
        {
            var inlineKeyboard2 = new InlineKeyboardMarkup
                  (new[] {
                            InlineKeyboardButton.WithCallbackData("Новичок"),
                            InlineKeyboardButton.WithCallbackData("Профессионал")
                         });
            await Bot.SendTextMessageAsync(e.Message.From.Id, "Выбери уровень своего мастерства", replyMarkup: inlineKeyboard2);
        }

        public int Idea(TelegramBotClient Bot, MessageEventArgs e)
        {
            int lastmes = Bot.SendTextMessageAsync(e.Message.From.Id, "Если ты нажал(а) сюда, значит хочешь что-то связать. Напиши что?").Result.MessageId;
            return lastmes;
        }

        public async void Calculator(TelegramBotClient Bot, MessageEventArgs e)
        {
            var inlineKeyboard4 = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Указать данные")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести расчет")
                        }
                    });
            await Bot.SendTextMessageAsync(e.Message.From.Id, "Выбери, что ты хочешь сделать", replyMarkup: inlineKeyboard4);
        }

        public async void Send_Ideas(TelegramBotClient Bot, MessageEventArgs e)
        {
            var inlineKeyboard2 = new InlineKeyboardMarkup
                  (new[] {
                            InlineKeyboardButton.WithCallbackData("Еще"),
                            InlineKeyboardButton.WithCallbackData("Хватит")
                         });
            await Bot.SendTextMessageAsync(e.Message.From.Id, "Хочешь еще идей!?", replyMarkup: inlineKeyboard2);
        }
    }
}
