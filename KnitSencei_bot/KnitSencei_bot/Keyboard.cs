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
 @" Привет! Я- бот-помощник в твоих вязальных начинаниях! Ты всегда можешь обратиться ко мне за порцией идей, мотивации или просто поговорить! 
А еще я могу помочь тебе расчитать количество пряжи для твоих проектов!
Список команд, которые доступны мне :
/start - запуск бота,
/keyboard - вывод клавиатуры с командами,
/calculator - вызвать калькулятор количества пряжи,
/ideas- показать идеи для вязания,
/inline - связь с разработчиком";
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
            await Bot.SendTextMessageAsync(e.Message.From.Id, "Выбери, как именно ты хочешь связаться с моим разработчиком.", replyMarkup: inlineKeyboard);
        }

        public async void Keyboard_k(TelegramBotClient Bot, MessageEventArgs e)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new []
                        {
                            new KeyboardButton("/calculator"),
                            
                        },
                        new []
                        {
                            new KeyboardButton("/ideas"),
                            new KeyboardButton("/inline")
                        }
                    });
            replyKeyboard.ResizeKeyboard = true;
            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Теперь у тебя под рукой все команды, доступные мне!", replyMarkup: replyKeyboard);
        }

        public int Idea(TelegramBotClient Bot, MessageEventArgs e)
        {
            int lastmes = Bot.SendTextMessageAsync(e.Message.From.Id, "Если ты нажал(а) сюда, значит хочешь что-то связать. Напиши что? А если захочешь больше идей, пиши 'еще'! ").Result.MessageId;
            return lastmes;
        }

        public int Calculator(TelegramBotClient Bot, MessageEventArgs e)
        {
            int lastmesdata = Bot.SendTextMessageAsync(e.Message.From.Id, "Укажи вид изделия, которое ты хочешь связать.").Result.MessageId;
            return lastmesdata;
           
        }

        //public async void Send_Ideas(TelegramBotClient Bot, MessageEventArgs e)
        //{
        //    var inlineKeyboard2 = new InlineKeyboardMarkup
        //          (new[] {
        //                    InlineKeyboardButton.WithCallbackData("Еще"),
        //                    InlineKeyboardButton.WithCallbackData("Хватит")
        //                 });
        //    await Bot.SendTextMessageAsync(e.Message.From.Id, "Хочешь еще идей!?", replyMarkup: inlineKeyboard2);
        //}
    }
}
