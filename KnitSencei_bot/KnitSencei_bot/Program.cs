using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using ApiAiSDK;
using ApiAiSDK.Model;
using System.IO;

namespace KnitSencei_bot
{
    class Program
    {
        static TelegramBotClient Bot;
        static ApiAi apiAi;
        static FillIProduct fill_product = new FillIProduct();
        static List<DataOfProduct> product = new List<DataOfProduct>();
        static DataOfIdea idea = new DataOfIdea();
        static Keyboard keyboard = new Keyboard();
        static List<DataOfIdea> bla = idea.ReadFile("ideas.csv");
        //static List<CalcData> cd = 
        static List<DataOfIdea> data = new List<DataOfIdea>();
        static int last_message;
        //static int count = 0;
        static string select_product;
        static int blaCount = 0;
        static int count_idea = 0;

        static void Main(string[] args)
        {
            //cee1d885245c490a85cf0ce0ef756cc4
            Bot = new TelegramBotClient("596483216:AAFe1hLBlTZDZWNLLIxpzX2LUq-NhqHR2mo");
            AIConfiguration config = new AIConfiguration("cee1d885245c490a85cf0ce0ef756cc4", SupportedLanguage.Russian);
            apiAi = new ApiAi(config);

            Bot.OnMessage += Bot_OnMessageReceived;
            Bot.OnCallbackQuery += Bot_OnCallbackQueryReceived;

            Bot.StartReceiving();
            fill_product.Fill_product(product); //заполнение листа информацией из тхт
            Console.ReadLine();
            Bot.StopReceiving();
        }


        private static async void Bot_OnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName}{e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} нажал(а) кнопку {buttonText}");

            if (buttonText == "Указать данные")
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Укажи вид изделия, размер и данные о пряже");
            else if (buttonText == "Произвести расчет")
            {
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Результат расчета - ");
            }
            await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Ты нажал(а) кнопку {buttonText}");
        }

        private async static void Bot_OnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.TextMessage)
                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Извини, но я могу воспринимать только текст");
            string name = $"{message.From.FirstName}{message.From.LastName}";
            Console.WriteLine($"{name} отправил(a) сообщение '{message.Text}'  ");
            switch (message.Text)
            {
                case "/start": keyboard.Start(Bot, e); break;
                case "/inline": keyboard.Inline(Bot, e); break;
                case "/keyboard": keyboard.Keyboard_k(Bot, e); break;
                case "/ideas": last_message = keyboard.Idea(Bot, e); break;
                case "/calculator": keyboard.Calculator(Bot, e); break;
                default:
                    //Bot.SendTextMessageAsync(e.Message.From.Id, "Прости, я не понимаю тебя").Wait();
                    //var response = apiAi.TextRequest(message.Text);
                    //string answer = response.Result.Fulfillment.Speech;
                    //if (answer == "")
                    //    answer = "Прости, я не понимаю тебя";
                    //await Bot.SendTextMessageAsync(e.Message.From.Id, answer);
                    break;
            }    
            blaCount = bla.Count()-1;

            blaCount = bla.Count() - 1;
            for (int i = 0; i <= product.Count() - 1; i++)
            {
                if ((last_message + 1 == e.Message.MessageId) && (e.Message.Text.ToLower() == product[i].product.ToLower()))
                    select_product = product[i].product;

            }

            if (blaCount != 0)
            {

                if (select_product == bla[count_idea].Product.ToLower())
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, bla[count_idea].Name + "\n" + bla[0].Description).Wait();
                    var FileUrl = string.Format(@"images//{0}", bla[count_idea].Photo);
                    var stream = new FileStream(FileUrl, FileMode.Open);
                    var fileToSend = new FileToSend(bla[count_idea].Photo, stream);
                    await Bot.SendPhotoAsync(e.Message.Chat.Id, fileToSend);
                    count_idea++;
                }
                for (int i = 0; i <= product.Count() - 1; i++)
                {
                    if ((last_message + 1 == e.Message.MessageId) && (e.Message.Text.ToLower() == product[i].product.ToLower()))
                    { select_product = product[i].product; }
                }

                if ((blaCount != 0) & (count_idea == 0))
                {
                    for (int i = 0; i <= blaCount; i++)
                    {
                        if (select_product == bla[i].Product.ToLower())
                        {
                            DataOfIdea tmp = new DataOfIdea();
                            tmp.ID = bla[i].ID;
                            tmp.Name = bla[i].Name;
                            tmp.Product = bla[i].Product;
                            tmp.Photo = bla[i].Photo;
                            tmp.Description = bla[i].Description;
                            data.Add(tmp);
                        }
                    }


                    //if ((last_message + 1 == e.Message.MessageId) && (e.Message.Text.ToLower() != product[count].product.ToLower()))
                    //    Bot.SendTextMessageAsync(e.Message.From.Id, "Otvali Vasya").Wait();
                    if (count_idea == 5)
                    { Bot.SendTextMessageAsync(e.Message.Chat.Id, "К сожалению, в данной категории больше нет идей! Для того, чтобы найти что-то другое нужно выбрать /ideas").Wait(); select_product = ""; data.Clear(); count_idea = 0; }
                    else if (data.Count() != 0)
                    {
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, data[count_idea].Name + "\n" + data[count_idea].Description).Wait();
                        var FileUrl = string.Format(@"images//{0}", data[count_idea].Photo);
                        var stream = new FileStream(FileUrl, FileMode.Open);
                        var fileToSend = new FileToSend(data[count_idea].Photo, stream);
                        await Bot.SendPhotoAsync(e.Message.Chat.Id, fileToSend);
                        count_idea++;
                    }
                }
            }
      }

         
            //if ((last_message + 1 == e.Message.MessageId) && (e.Message.Text.ToLower() != product[count].product.ToLower()))
            //    Bot.SendTextMessageAsync(e.Message.From.Id, "Otvali Vasya").Wait();
        }
    }

